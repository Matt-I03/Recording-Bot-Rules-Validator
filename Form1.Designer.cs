namespace DeserializeV2
{
    partial class Form1
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
            this.JsonLabel = new System.Windows.Forms.Label();
            this.XmlLabel = new System.Windows.Forms.Label();
            this.XmlFileDrop = new System.Windows.Forms.TextBox();
            this.JsonTextBox = new System.Windows.Forms.TextBox();
            this.JsonSubmit = new System.Windows.Forms.Button();
            this.OnlineMeetingButton = new System.Windows.Forms.Button();
            this.RecordingFilterButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.XmlFileDrop2 = new System.Windows.Forms.TextBox();
            this.ClearBttn = new System.Windows.Forms.Button();
            this.DisplayDataBttn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JsonLabel
            // 
            this.JsonLabel.AutoSize = true;
            this.JsonLabel.Location = new System.Drawing.Point(65, 41);
            this.JsonLabel.Name = "JsonLabel";
            this.JsonLabel.Size = new System.Drawing.Size(134, 13);
            this.JsonLabel.TabIndex = 0;
            this.JsonLabel.Text = "BotConfiguration Json Text";
            // 
            // XmlLabel
            // 
            this.XmlLabel.AutoSize = true;
            this.XmlLabel.Location = new System.Drawing.Point(253, 41);
            this.XmlLabel.Name = "XmlLabel";
            this.XmlLabel.Size = new System.Drawing.Size(134, 13);
            this.XmlLabel.TabIndex = 1;
            this.XmlLabel.Text = "BotConfiguration XML drop";
            // 
            // XmlFileDrop
            // 
            this.XmlFileDrop.AllowDrop = true;
            this.XmlFileDrop.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.XmlFileDrop.Location = new System.Drawing.Point(256, 71);
            this.XmlFileDrop.Multiline = true;
            this.XmlFileDrop.Name = "XmlFileDrop";
            this.XmlFileDrop.ReadOnly = true;
            this.XmlFileDrop.Size = new System.Drawing.Size(162, 146);
            this.XmlFileDrop.TabIndex = 2;
            this.XmlFileDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.XmlFileDrop_DragDrop);
            this.XmlFileDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.XmlFileDrop_DragEnter);
            // 
            // JsonTextBox
            // 
            this.JsonTextBox.Location = new System.Drawing.Point(68, 57);
            this.JsonTextBox.Multiline = true;
            this.JsonTextBox.Name = "JsonTextBox";
            this.JsonTextBox.Size = new System.Drawing.Size(168, 171);
            this.JsonTextBox.TabIndex = 3;
            // 
            // JsonSubmit
            // 
            this.JsonSubmit.Location = new System.Drawing.Point(68, 234);
            this.JsonSubmit.Name = "JsonSubmit";
            this.JsonSubmit.Size = new System.Drawing.Size(75, 23);
            this.JsonSubmit.TabIndex = 4;
            this.JsonSubmit.Text = "Submit";
            this.JsonSubmit.UseVisualStyleBackColor = true;
            this.JsonSubmit.Click += new System.EventHandler(this.JsonSubmit_Click);
            // 
            // OnlineMeetingButton
            // 
            this.OnlineMeetingButton.Location = new System.Drawing.Point(233, 354);
            this.OnlineMeetingButton.Name = "OnlineMeetingButton";
            this.OnlineMeetingButton.Size = new System.Drawing.Size(131, 23);
            this.OnlineMeetingButton.TabIndex = 5;
            this.OnlineMeetingButton.Text = "Online Meeting Check";
            this.OnlineMeetingButton.UseVisualStyleBackColor = true;
            this.OnlineMeetingButton.Click += new System.EventHandler(this.OnlineMeetingButton_Click);
            // 
            // RecordingFilterButton
            // 
            this.RecordingFilterButton.Location = new System.Drawing.Point(395, 354);
            this.RecordingFilterButton.Name = "RecordingFilterButton";
            this.RecordingFilterButton.Size = new System.Drawing.Size(141, 23);
            this.RecordingFilterButton.TabIndex = 6;
            this.RecordingFilterButton.Text = "Recording Filter Check";
            this.RecordingFilterButton.UseVisualStyleBackColor = true;
            this.RecordingFilterButton.Click += new System.EventHandler(this.RecordingFilterButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(465, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Xml meeting data box";
            // 
            // XmlFileDrop2
            // 
            this.XmlFileDrop2.AllowDrop = true;
            this.XmlFileDrop2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.XmlFileDrop2.Location = new System.Drawing.Point(468, 71);
            this.XmlFileDrop2.Multiline = true;
            this.XmlFileDrop2.Name = "XmlFileDrop2";
            this.XmlFileDrop2.ReadOnly = true;
            this.XmlFileDrop2.Size = new System.Drawing.Size(158, 146);
            this.XmlFileDrop2.TabIndex = 8;
            this.XmlFileDrop2.DragDrop += new System.Windows.Forms.DragEventHandler(this.XmlFileDrop2_DragDrop);
            this.XmlFileDrop2.DragEnter += new System.Windows.Forms.DragEventHandler(this.XmlFileDrop_DragEnter);
            // 
            // ClearBttn
            // 
            this.ClearBttn.Location = new System.Drawing.Point(312, 262);
            this.ClearBttn.Name = "ClearBttn";
            this.ClearBttn.Size = new System.Drawing.Size(141, 23);
            this.ClearBttn.TabIndex = 9;
            this.ClearBttn.Text = "Clear";
            this.ClearBttn.UseVisualStyleBackColor = true;
            this.ClearBttn.Click += new System.EventHandler(this.ClearBttn_Click);
            // 
            // DisplayDataBttn
            // 
            this.DisplayDataBttn.Location = new System.Drawing.Point(343, 314);
            this.DisplayDataBttn.Name = "DisplayDataBttn";
            this.DisplayDataBttn.Size = new System.Drawing.Size(75, 23);
            this.DisplayDataBttn.TabIndex = 10;
            this.DisplayDataBttn.Text = "Display Data";
            this.DisplayDataBttn.UseVisualStyleBackColor = true;
            this.DisplayDataBttn.Click += new System.EventHandler(this.DisplayDataBttn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DisplayDataBttn);
            this.Controls.Add(this.ClearBttn);
            this.Controls.Add(this.XmlFileDrop2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RecordingFilterButton);
            this.Controls.Add(this.OnlineMeetingButton);
            this.Controls.Add(this.JsonSubmit);
            this.Controls.Add(this.JsonTextBox);
            this.Controls.Add(this.XmlFileDrop);
            this.Controls.Add(this.XmlLabel);
            this.Controls.Add(this.JsonLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label JsonLabel;
        private System.Windows.Forms.Label XmlLabel;
        private System.Windows.Forms.TextBox XmlFileDrop;
        private System.Windows.Forms.TextBox JsonTextBox;
        private System.Windows.Forms.Button JsonSubmit;
        private System.Windows.Forms.Button OnlineMeetingButton;
        private System.Windows.Forms.Button RecordingFilterButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox XmlFileDrop2;
        private System.Windows.Forms.Button ClearBttn;
        private System.Windows.Forms.Button DisplayDataBttn;
    }
}

