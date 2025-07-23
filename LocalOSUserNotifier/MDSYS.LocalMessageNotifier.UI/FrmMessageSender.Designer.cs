using MDSYS.LocalMessageNotifier.UI.Controls;

namespace MDSYS.LocalMessageNotifier.UI
{
    partial class FrmMessageSender
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessageSender));
            sendToGroupBox = new GroupBox();
            rbtSendToSelected = new RadioButton();
            chUsers = new UsersCheckedListBox();
            rbtSendToAll = new RadioButton();
            messageGroupBox = new GroupBox();
            txtMessage = new RichTextBox();
            statusFilterGroupBox = new GroupBox();
            chStatusFilter = new CheckedListBox();
            toolStrip1 = new ToolStrip();
            btnSend_Cancel = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnTemplate = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnRefresh = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnRTL = new ToolStripButton();
            btnAbout = new ToolStripButton();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            statusProgressBar = new ToolStripProgressBar();
            copyRightLable = new Label();
            sendToGroupBox.SuspendLayout();
            messageGroupBox.SuspendLayout();
            statusFilterGroupBox.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // sendToGroupBox
            // 
            sendToGroupBox.Controls.Add(rbtSendToSelected);
            sendToGroupBox.Controls.Add(chUsers);
            sendToGroupBox.Controls.Add(rbtSendToAll);
            sendToGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            sendToGroupBox.Location = new Point(5, 127);
            sendToGroupBox.Name = "sendToGroupBox";
            sendToGroupBox.Size = new Size(783, 225);
            sendToGroupBox.TabIndex = 8;
            sendToGroupBox.TabStop = false;
            sendToGroupBox.Text = "Send To";
            // 
            // rbtSendToSelected
            // 
            rbtSendToSelected.AutoSize = true;
            rbtSendToSelected.Location = new Point(61, 16);
            rbtSendToSelected.Name = "rbtSendToSelected";
            rbtSendToSelected.Size = new Size(88, 24);
            rbtSendToSelected.TabIndex = 2;
            rbtSendToSelected.Text = "Selected";
            rbtSendToSelected.UseVisualStyleBackColor = true;
            // 
            // chUsers
            // 
            chUsers.ColumnWidth = 250;
            chUsers.Dock = DockStyle.Bottom;
            chUsers.Location = new Point(3, 42);
            chUsers.MultiColumn = true;
            chUsers.Name = "chUsers";
            chUsers.Size = new Size(777, 180);
            chUsers.TabIndex = 3;
            // 
            // rbtSendToAll
            // 
            rbtSendToAll.AutoSize = true;
            rbtSendToAll.Location = new Point(6, 16);
            rbtSendToAll.Name = "rbtSendToAll";
            rbtSendToAll.Size = new Size(49, 24);
            rbtSendToAll.TabIndex = 1;
            rbtSendToAll.Text = "All";
            rbtSendToAll.UseVisualStyleBackColor = true;
            // 
            // messageGroupBox
            // 
            messageGroupBox.Controls.Add(txtMessage);
            messageGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            messageGroupBox.Location = new Point(5, 358);
            messageGroupBox.Name = "messageGroupBox";
            messageGroupBox.Size = new Size(780, 213);
            messageGroupBox.TabIndex = 9;
            messageGroupBox.TabStop = false;
            messageGroupBox.Text = "Message Body (0/255)";
            // 
            // txtMessage
            // 
            txtMessage.Dock = DockStyle.Fill;
            txtMessage.Location = new Point(3, 23);
            txtMessage.Name = "txtMessage";
            txtMessage.RightToLeft = RightToLeft.Yes;
            txtMessage.Size = new Size(774, 187);
            txtMessage.TabIndex = 1;
            txtMessage.Text = "";
            // 
            // statusFilterGroupBox
            // 
            statusFilterGroupBox.Controls.Add(chStatusFilter);
            statusFilterGroupBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            statusFilterGroupBox.Location = new Point(5, 30);
            statusFilterGroupBox.Name = "statusFilterGroupBox";
            statusFilterGroupBox.Size = new Size(783, 102);
            statusFilterGroupBox.TabIndex = 15;
            statusFilterGroupBox.TabStop = false;
            statusFilterGroupBox.Text = "Status Filter";
            // 
            // chStatusFilter
            // 
            chStatusFilter.Dock = DockStyle.Fill;
            chStatusFilter.Location = new Point(3, 23);
            chStatusFilter.MultiColumn = true;
            chStatusFilter.Name = "chStatusFilter";
            chStatusFilter.Size = new Size(777, 76);
            chStatusFilter.TabIndex = 3;
            // 
            // toolStrip1
            // 
            toolStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnSend_Cancel, toolStripSeparator3, btnTemplate, toolStripSeparator2, btnRefresh, toolStripSeparator1, btnRTL, btnAbout });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(789, 27);
            toolStrip1.TabIndex = 16;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnSend_Cancel
            // 
            btnSend_Cancel.Image = Properties.Resources.Send;
            btnSend_Cancel.ImageTransparentColor = Color.Magenta;
            btnSend_Cancel.Name = "btnSend_Cancel";
            btnSend_Cancel.Size = new Size(67, 24);
            btnSend_Cancel.Text = "Send";
            btnSend_Cancel.ToolTipText = "Send The Message";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // btnTemplate
            // 
            btnTemplate.Image = Properties.Resources.template;
            btnTemplate.ImageTransparentColor = Color.Magenta;
            btnTemplate.Name = "btnTemplate";
            btnTemplate.Size = new Size(166, 24);
            btnTemplate.Text = "MessageTemplates";
            btnTemplate.ToolTipText = "Save and reuse message templates";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 27);
            // 
            // btnRefresh
            // 
            btnRefresh.Image = Properties.Resources.refresh;
            btnRefresh.ImageTransparentColor = Color.Magenta;
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(87, 24);
            btnRefresh.Text = "Refresh";
            btnRefresh.ToolTipText = "Refresh The User List";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // btnRTL
            // 
            btnRTL.Image = Properties.Resources.righttoleft;
            btnRTL.ImageTransparentColor = Color.Magenta;
            btnRTL.Name = "btnRTL";
            btnRTL.Size = new Size(60, 24);
            btnRTL.Text = "RTL";
            btnRTL.ToolTipText = "Message Body RightToLeft Mode";
            // 
            // btnAbout
            // 
            btnAbout.Image = Properties.Resources.about;
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(77, 24);
            btnAbout.Text = "About";
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, statusProgressBar });
            statusStrip.Location = new Point(0, 615);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(789, 26);
            statusStrip.TabIndex = 17;
            statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            statusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            statusLabel.Image = Properties.Resources.statusReady;
            statusLabel.ImageAlign = ContentAlignment.MiddleLeft;
            statusLabel.Name = "statusLabel";
            statusLabel.Padding = new Padding(230, 0, 0, 0);
            statusLabel.Size = new Size(303, 20);
            statusLabel.Text = "Status";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusProgressBar
            // 
            statusProgressBar.Name = "statusProgressBar";
            statusProgressBar.Size = new Size(100, 18);
            // 
            // copyRightLable
            // 
            copyRightLable.AutoSize = true;
            copyRightLable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            copyRightLable.Location = new Point(5, 582);
            copyRightLable.Name = "copyRightLable";
            copyRightLable.Size = new Size(330, 20);
            copyRightLable.TabIndex = 18;
            copyRightLable.Text = "Copyright © 2023 MDSYS. All rights reserved.";
            copyRightLable.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmMessageSender
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(789, 641);
            Controls.Add(copyRightLable);
            Controls.Add(statusStrip);
            Controls.Add(toolStrip1);
            Controls.Add(statusFilterGroupBox);
            Controls.Add(sendToGroupBox);
            Controls.Add(messageGroupBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FrmMessageSender";
            StartPosition = FormStartPosition.CenterScreen;
            sendToGroupBox.ResumeLayout(false);
            sendToGroupBox.PerformLayout();
            messageGroupBox.ResumeLayout(false);
            statusFilterGroupBox.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox sendToGroupBox;
        private RadioButton rbtSendToSelected;
        private RadioButton rbtSendToAll;
        private UsersCheckedListBox chUsers;
        private GroupBox messageGroupBox;
        private RichTextBox txtMessage;
        private GroupBox statusFilterGroupBox;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private CheckedListBox chStatusFilter;
        private ToolStrip toolStrip1;
        private ToolStripButton btnSend_Cancel;
        private ToolStripButton btnTemplate;
        private ToolStripButton btnRefresh;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnRTL;
        private ToolStripProgressBar statusProgressBar;
        private Label copyRightLable;
        private ToolStripButton btnAbout;
    }
}
