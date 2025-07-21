using MDSYS.LocalMessageNotifier.Core;

namespace MDSYS.LocalMessageNotifier.UI
{
    public partial class FrmMessageTemplate : Form
    {
        private readonly IMessageTemplateFileService _templateService;

        public string SelectedMessage { get; private set; } = string.Empty;

        public FrmMessageTemplate(IMessageTemplateFileService templateService)
        {
            InitializeComponent();
            this.Text = $"{Constants.ToolCompany}-{Constants.ToolName}- Message Templates";
            _templateService = templateService;
            this.Load += FrmMessageTemplate_Load;
            msgList.SelectedIndexChanged += MsgList_SelectedIndexChanged;
            msgList.DoubleClick += (s, e) => { if (msgList.SelectedItem != null) this.DialogResult = DialogResult.OK; };
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDel_Click;
        }

        private async void FrmMessageTemplate_Load(object? sender, EventArgs e) => await LoadMessageTemplatesAsync();

        private async Task LoadMessageTemplatesAsync()
        {
            try
            {
                msgList.DataSource = await _templateService.LoadAsync();
                msgList.DisplayMember = "MessageKey";
                UpdateButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not load message templates", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Log the exception if logging is set up
                // _logger.LogError(ex, "Failed to load message templates.");
            }
        }

        private void UpdateButtons()
        {
            bool itemSelected = msgList.SelectedItem != null;
            btnEdit.Enabled = itemSelected;
            btnDelete.Enabled = itemSelected;
        }

        private async void BtnAdd_Click(object? sender, EventArgs e)
        {
            using var editor = new FrmMessageTemplateEditor(string.Empty, string.Empty, SavingMode.New, _templateService);
            if (editor.ShowDialog(this) == DialogResult.OK) await LoadMessageTemplatesAsync();
        }

        private async void BtnEdit_Click(object? sender, EventArgs e)
        {
            if (msgList.SelectedItem is MessageTemplate selected)
            {
                using var editor = new FrmMessageTemplateEditor(selected.MessageKey, selected.Message, SavingMode.Edit, _templateService);
                if (editor.ShowDialog(this) == DialogResult.OK) await LoadMessageTemplatesAsync();
            }
        }

        private async void BtnDel_Click(object? sender, EventArgs e)
        {
            if (msgList.SelectedItem is MessageTemplate selected)
            {
                var confirmResult = MessageBox.Show(this, $"Are you sure you want to delete '{selected.MessageKey}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    await _templateService.DeleteAsync(selected.MessageKey);
                    await LoadMessageTemplatesAsync();
                }
            }
        }

        private void MsgList_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (msgList.SelectedItem is MessageTemplate selected) SelectedMessage = selected.Message;
            UpdateButtons();
        }
    }
}
