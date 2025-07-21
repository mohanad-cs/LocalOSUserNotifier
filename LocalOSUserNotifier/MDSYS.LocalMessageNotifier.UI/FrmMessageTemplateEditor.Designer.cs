namespace MDSYS.LocalMessageNotifier.UI
{
    partial class FrmMessageTemplateEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessageTemplateEditor));
            txtMessage = new RichTextBox();
            txtKey = new TextBox();
            messageKeyGroupBox = new GroupBox();
            messageBodyGroupBox = new GroupBox();
            toolStrip1 = new ToolStrip();
            btnSave = new ToolStripButton();
            messageKeyGroupBox.SuspendLayout();
            messageBodyGroupBox.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtMessage
            // 
            txtMessage.Dock = DockStyle.Fill;
            txtMessage.Location = new Point(3, 23);
            txtMessage.MaxLength = 255;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(523, 338);
            txtMessage.TabIndex = 4;
            txtMessage.Text = "";
            // 
            // txtKey
            // 
            txtKey.Dock = DockStyle.Top;
            txtKey.Location = new Point(3, 23);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(523, 27);
            txtKey.TabIndex = 2;
            // 
            // messageKeyGroupBox
            // 
            messageKeyGroupBox.Controls.Add(txtKey);
            messageKeyGroupBox.Dock = DockStyle.Top;
            messageKeyGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            messageKeyGroupBox.Location = new Point(0, 27);
            messageKeyGroupBox.Name = "messageKeyGroupBox";
            messageKeyGroupBox.Size = new Size(529, 59);
            messageKeyGroupBox.TabIndex = 1;
            messageKeyGroupBox.TabStop = false;
            messageKeyGroupBox.Text = "Message Key";
            // 
            // messageBodyGroupBox
            // 
            messageBodyGroupBox.Controls.Add(txtMessage);
            messageBodyGroupBox.Dock = DockStyle.Bottom;
            messageBodyGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            messageBodyGroupBox.Location = new Point(0, 86);
            messageBodyGroupBox.Name = "messageBodyGroupBox";
            messageBodyGroupBox.Size = new Size(529, 364);
            messageBodyGroupBox.TabIndex = 3;
            messageBodyGroupBox.TabStop = false;
            messageBodyGroupBox.Text = "Message Body (0/255)";
            // 
            // toolStrip1
            // 
            toolStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnSave });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(529, 27);
            toolStrip1.TabIndex = 12;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            btnSave.Image = Properties.Resources.save;
            btnSave.ImageTransparentColor = Color.Magenta;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(65, 24);
            btnSave.Text = "Save";
            // 
            // FrmMessageTemplateEditor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(529, 450);
            Controls.Add(messageBodyGroupBox);
            Controls.Add(messageKeyGroupBox);
            Controls.Add(toolStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMessageTemplateEditor";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            messageKeyGroupBox.ResumeLayout(false);
            messageKeyGroupBox.PerformLayout();
            messageBodyGroupBox.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox txtMessage;
        private TextBox txtKey;
        private GroupBox messageKeyGroupBox;
        private GroupBox messageBodyGroupBox;
        private ToolStrip toolStrip1;
        private ToolStripButton btnSave;
    }
}