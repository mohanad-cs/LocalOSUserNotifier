using MDSYS.LocalMessageNotifier.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace MDSYS.LocalMessageNotifier.UI
{
    public partial class FrmMessageSender : Form
    {
        private readonly IUserSessionService _usersService;
        private readonly ILocalMessageService _messageService;
        private readonly IServiceProvider _serviceProvider;
        private readonly System.Windows.Forms.Timer _statusClearTimer;
        private const string _rtlTextPrefix = "RTL Mode: ";
        private CancellationTokenSource? _cancellationTokenSource;
        private const string _btnSendText = "Send";
        private const string _btnCancelText = "Cancel";
        private bool _taskRunning = false;
        public FrmMessageSender(
         IUserSessionService usersService,
         ILocalMessageService messageService,
         IServiceProvider serviceProvider
         )
        {
            InitializeComponent();
            this.Text = $"{Constants.ToolCompany}-{Constants.ToolName} - v{Constants.ToolVersion}";

            copyRightLable.Text = Constants.ToolCopyright;
            _usersService = usersService;
            _messageService = messageService;
            _serviceProvider = serviceProvider;
            _statusClearTimer = new System.Windows.Forms.Timer { Interval = 6000 };
            _statusClearTimer.Tick += (s, e) =>
            {
                if (!_taskRunning)
                {
                    statusLabel.Text = "Ready";
                    statusLabel.ForeColor = SystemColors.ControlText;
                    statusProgressBar.Value = 0;
                    statusProgressBar.Visible = false;
                    _statusClearTimer.Stop();
                }

            };
            this.Load += FrmMessageSender_Load;
            this.FormClosed += OnFormClosed;
            this.FormClosing += OnFormClosing; ;
        }



        private void FrmMessageSender_Load(object? sender, EventArgs e)
        {
            // Register this form to receive session change notifications
            WTSRegisterSessionNotification(this.Handle, NOTIFY_FOR_ALL_SESSIONS);
            // Setup status filter checklist
            chStatusFilter.Items.AddRange(Enum.GetNames(typeof(WTSConnectState)));
            chStatusFilter.SetItemChecked(chStatusFilter.Items.IndexOf(WTSConnectState.Active.ToString()), true);
            chStatusFilter.ItemCheck += (s, e) => BeginInvoke(new Action(LoadUsers));

            // Wire up event handlers
            btnSend_Cancel.Click += BtnSend_Click;
            btnAbout.Click += (s, e) =>
            {
                using var frmAbout = _serviceProvider.GetRequiredService<AboutBox>();
                frmAbout.ShowDialog(this);
            };
            btnTemplate.Click += BtnTemplate_Click;
            btnRefresh.Click += (s, e) => LoadUsers();
            btnRTL.Click += BtnRTL_Click;
            rbtSendToAll.CheckedChanged += RbtSendToAll_CheckedChanged;
            rbtSendToSelected.CheckedChanged += (s, e) => RbtSendToAll_CheckedChanged(s, e);
            txtMessage.TextChanged += TxtMessage_TextChanged;
            chUsers.ItemCheck += (s, e) => BeginInvoke(new Action(UpdateSendToGroupText));
            //_usersService.SessionsChanged += OnUserSessionsChanged;

            // Initial state
            txtMessage.MaxLength = Constants.MaxMessageLength;
            rbtSendToAll.Checked = true;
            statusProgressBar.Visible = false;
            UpdateStatus("Ready");
            LoadUsers();
            txtMessage.Focus();
            UpdateRTLButtonText();

        }
        private void UpdateProgressBar(int value)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateProgressBar(value));
                return;
            }
            statusProgressBar.Value = value;
            statusProgressBar.Visible = true;
            if (value >= 100)
            {
                statusProgressBar.Visible = false;
            }
        }
        private void UpdateStatus(string message, bool isError = false)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateStatus(message, isError));
                return;
            }

            statusLabel.Text = message;
            statusLabel.ForeColor = isError ? Color.Red : SystemColors.ControlText;
            statusLabel.Image = isError ? Properties.Resources.statusError : Properties.Resources.statusReady;
            _statusClearTimer.Start();
        }
        private void LoadUsers()
        {
            try
            {
                _taskRunning = true;
                UpdateStatus("Loading users...");
                UpdateProgressBar(3);
                var previouslyCheckedUsers = chUsers.CheckedItems.Cast<WindowsUser>().Select(u => u.UserName).ToHashSet();
                chUsers.Items.Clear();
                var selectedStatuses = chStatusFilter.CheckedItems.Cast<string>()
                    .Select(name => Enum.Parse<WTSConnectState>(name))
                    .ToArray();
                UpdateProgressBar(10);
                var userList = _usersService.GetUsers(selectedStatuses);
                UpdateProgressBar(20);
                chUsers.Items.AddRange(userList.Cast<object>().ToArray());
                UpdateProgressBar(50);
                for (int i = 0; i < chUsers.Items.Count; i++)
                {

                    if (chUsers.Items[i] is WindowsUser user && previouslyCheckedUsers.Contains(user.UserName))
                    {
                        chUsers.SetItemChecked(i, true);
                    }
                }

                if (rbtSendToAll.Checked) RbtSendToAll_CheckedChanged(this, EventArgs.Empty);
                UpdateSendToGroupText();
                UpdateProgressBar(100);
                UpdateStatus("Users loaded successfully.");
            }
            catch (Exception)
            {
                UpdateStatus("Error loading users.", true);

                //  MessageBox.Show(this, "Could not load users. See logs for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _taskRunning = false;
            }
        }
        private async void BtnSend_Click(object? sender, EventArgs e)
        {

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show(this, "Message Text is Required!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedUsers = chUsers.CheckedItems.Cast<WindowsUser>().ToList();
            if (!rbtSendToAll.Checked && selectedUsers.Count == 0)
            {
                MessageBox.Show(this, "One or more users need to be selected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            btnSend_Cancel.Text = _btnCancelText;
            try
            {
                _taskRunning = true;
                DisableEditors();
                var targets = (rbtSendToAll.Checked || selectedUsers.Count == chUsers.Items.Count)
                    ? new List<string> { "*" }
                    : selectedUsers.Select(u => u.SessionId).ToList();
                btnSend_Cancel.Image = Properties.Resources.cancelMessage;
                statusProgressBar.Visible = true;
                UpdateStatus($"Sending to {targets.Count} target(s)...");
                int successCount = 0;
                foreach (var target in targets)
                {
                    var (success, output) = await _messageService.SendMessageAsync(target, txtMessage.Text, _cancellationTokenSource.Token);
                    if (success)
                    {
                        successCount++;
                        UpdateProgressBar((int)((double)successCount / targets.Count * 100));
                    }
                    else
                    {
                        UpdateStatus($"Failed to send to session {target}.", true);
                        await Task.Delay(1000); // Pause to show error before continuing
                        statusProgressBar.Value = 0;
                        statusProgressBar.Visible = false;
                    }
                }

                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    UpdateStatus("Send operation canceled.", true);
                }
                else if (successCount > 0)
                {
                    UpdateStatus($"Message(s) sent successfully to {successCount} target(s).");
                }
            }
            catch (OperationCanceledException)
            {
                UpdateStatus("Send operation canceled", true);
            }
            catch (Exception ex)
            {
                UpdateStatus("An error occurred during send.", true);
            }
            finally
            {
                btnSend_Cancel.Text = _btnSendText;
                btnSend_Cancel.Image = Properties.Resources.Send;
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
                txtMessage.Focus();
                EnableEditors();
                _taskRunning = false;
            }




        }
        private void DisableEditors()
        {
            btnTemplate.Enabled = false;
            btnRTL.Enabled = false;
            btnRefresh.Enabled = false;
            messageGroupBox.Enabled = false;
            statusFilterGroupBox.Enabled = false;
            sendToGroupBox.Enabled = false;
        }
        private void EnableEditors()
        {
            btnTemplate.Enabled = true;
            btnRTL.Enabled = true;
            btnRefresh.Enabled = true;
            messageGroupBox.Enabled = true;
            statusFilterGroupBox.Enabled = true;
            sendToGroupBox.Enabled = true;
        }
        private void BtnTemplate_Click(object? sender, EventArgs e)
        {
            using var frmMessageTemplate = _serviceProvider.GetRequiredService<FrmMessageTemplate>();
            if (frmMessageTemplate.ShowDialog(this) == DialogResult.OK)
            {
                txtMessage.Text = frmMessageTemplate.SelectedMessage;
            }
        }
        private void OnUserSessionsChanged(object? sender, EventArgs e) => this.Invoke(new Action(LoadUsers));
        private void OnFormClosed(object? sender, FormClosedEventArgs e)
        {
           // _usersService.SessionsChanged -= OnUserSessionsChanged;
        }
        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
            // Unregister for notifications when the form is closing
            WTSUnRegisterSessionNotification(this.Handle);
           // base.OnFormClosing(e);
        }
        private void UpdateSendToGroupText() => sendToGroupBox.Text = $"Send To ({chUsers.CheckedItems.Count}/{chUsers.Items.Count})";
        private void TxtMessage_TextChanged(object? sender, EventArgs e) => messageGroupBox.Text = $"Message Body ({txtMessage.Text.Length}/{Constants.MaxMessageLength})";
        private void UpdateRTLButtonText() => btnRTL.Text = txtMessage.RightToLeft == RightToLeft.Yes ? _rtlTextPrefix + "(Yes)" : _rtlTextPrefix + "(No)";
        private void BtnRTL_Click(object? sender, EventArgs e)
        {
            txtMessage.RightToLeft = txtMessage.RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
            UpdateRTLButtonText();
        }
        private void RbtSendToAll_CheckedChanged(object? sender, EventArgs e)
        {
            bool isSendToAll = rbtSendToAll.Checked;
            chUsers.Enabled = !isSendToAll;
            for (int i = 0; i < chUsers.Items.Count; i++)
            {
                chUsers.SetItemChecked(i, isSendToAll);
            }
            UpdateSendToGroupText();
        }


#pragma warning disable IDE1006 // Naming Styles
        private const int WM_WTSSESSION_CHANGE = 0x2B1;
        private const int NOTIFY_FOR_ALL_SESSIONS = 1;
#pragma warning restore IDE1006 // Naming Styles


        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSRegisterSessionNotification(IntPtr hWnd, int dwFlags);

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);

        protected override void WndProc(ref Message m)
        {
            // Check if the message is a session change notification
            if (m.Msg == WM_WTSSESSION_CHANGE)
            {
               // _logger.LogInformation("Received WM_WTSSESSION_CHANGE message. Refreshing users.");
                LoadUsers();
            }

            // Pass all other messages to the base class
            base.WndProc(ref m);
        }
    }
}
