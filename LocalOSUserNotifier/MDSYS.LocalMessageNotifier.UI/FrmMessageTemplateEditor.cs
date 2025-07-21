using MDSYS.LocalMessageNotifier.Core;

namespace MDSYS.LocalMessageNotifier.UI
{
    public enum SavingMode { New, Edit }
    public partial class FrmMessageTemplateEditor : Form
    {
        private readonly SavingMode _savingMode;
        private readonly IMessageTemplateFileService _templateService;

        public FrmMessageTemplateEditor(string key, string message, SavingMode savingMode, IMessageTemplateFileService templateService)
        {
            InitializeComponent();
            this.Text = $"{Constants.ToolCompany}-{Constants.ToolName}- Message Template Editor";
            _savingMode = savingMode;
            _templateService = templateService;
            txtKey.Text = key;
            txtMessage.Text = message;
            txtMessage.MaxLength = Constants.MaxMessageLength;
            txtMessage.TextChanged += TxtMessage_TextChanged; ;
            if (_savingMode == SavingMode.Edit) messageKeyGroupBox.Enabled = false;
            UpdateMessageBodyGroupBoxText();
            btnSave.Click += BtnSave_Click;
        }

        private void TxtMessage_TextChanged(object? sender, EventArgs e)
        {
            UpdateMessageBodyGroupBoxText();
        }
        private void UpdateMessageBodyGroupBoxText()
        {
            messageBodyGroupBox.Text = $"Message Body ({txtMessage.Text.Length}/{Constants.MaxMessageLength})";
        }
        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtKey.Text))
                {
                    bool keyExist = await _templateService.ExistAsync(txtKey.Text);
                    if (keyExist)
                    {
                        MessageBox.Show("Message key already exists. Please use a different key.", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                var messageTemplate = new MessageTemplate(txtKey.Text, txtMessage.Text);
                if (_savingMode == SavingMode.New) await _templateService.AddAsync(messageTemplate);
                else await _templateService.UpdateAsync(messageTemplate);
                this.DialogResult = DialogResult.OK;
            }
            catch (ArgumentException aex)
            {
                MessageBox.Show(this, aex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to save template.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
