namespace Validator
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
            this.BotConfigFileBox = new System.Windows.Forms.TextBox();
            this.CallDataFileBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BotConfigFileSearch = new System.Windows.Forms.Button();
            this.CallXMLFileSearch = new System.Windows.Forms.Button();
            this.ValidateBttn = new System.Windows.Forms.Button();
            this.ResultImage = new System.Windows.Forms.PictureBox();
            this.DisplayBotConfig = new System.Windows.Forms.Button();
            this.DisplayCallXML = new System.Windows.Forms.Button();
            this.ResultsDisplay = new System.Windows.Forms.RichTextBox();
            this.ClearBttn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).BeginInit();
            this.SuspendLayout();
            // 
            // BotConfigFileBox
            // 
            this.BotConfigFileBox.Location = new System.Drawing.Point(36, 54);
            this.BotConfigFileBox.Multiline = true;
            this.BotConfigFileBox.Name = "BotConfigFileBox";
            this.BotConfigFileBox.Size = new System.Drawing.Size(465, 32);
            this.BotConfigFileBox.TabIndex = 0;
            // 
            // CallDataFileBox
            // 
            this.CallDataFileBox.Location = new System.Drawing.Point(36, 149);
            this.CallDataFileBox.Multiline = true;
            this.CallDataFileBox.Name = "CallDataFileBox";
            this.CallDataFileBox.Size = new System.Drawing.Size(465, 32);
            this.CallDataFileBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bot Config File (Json/XML)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Call XML File";
            // 
            // BotConfigFileSearch
            // 
            this.BotConfigFileSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.BotConfigFileSearch.Location = new System.Drawing.Point(523, 54);
            this.BotConfigFileSearch.Name = "BotConfigFileSearch";
            this.BotConfigFileSearch.Size = new System.Drawing.Size(83, 32);
            this.BotConfigFileSearch.TabIndex = 4;
            this.BotConfigFileSearch.Text = ". . .";
            this.BotConfigFileSearch.UseVisualStyleBackColor = true;
            this.BotConfigFileSearch.Click += new System.EventHandler(this.BotConfigFileSearch_Click);
            // 
            // CallXMLFileSearch
            // 
            this.CallXMLFileSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.CallXMLFileSearch.Location = new System.Drawing.Point(523, 149);
            this.CallXMLFileSearch.Name = "CallXMLFileSearch";
            this.CallXMLFileSearch.Size = new System.Drawing.Size(83, 32);
            this.CallXMLFileSearch.TabIndex = 5;
            this.CallXMLFileSearch.Text = ". . .";
            this.CallXMLFileSearch.UseVisualStyleBackColor = true;
            this.CallXMLFileSearch.Click += new System.EventHandler(this.CallXMLFileSearch_Click);
            // 
            // ValidateBttn
            // 
            this.ValidateBttn.Location = new System.Drawing.Point(105, 252);
            this.ValidateBttn.Name = "ValidateBttn";
            this.ValidateBttn.Size = new System.Drawing.Size(308, 76);
            this.ValidateBttn.TabIndex = 6;
            this.ValidateBttn.Text = "Validate";
            this.ValidateBttn.UseVisualStyleBackColor = true;
            this.ValidateBttn.Click += new System.EventHandler(this.ValidateBttn_Click);
            // 
            // ResultImage
            // 
            this.ResultImage.Location = new System.Drawing.Point(440, 252);
            this.ResultImage.Name = "ResultImage";
            this.ResultImage.Size = new System.Drawing.Size(88, 76);
            this.ResultImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ResultImage.TabIndex = 7;
            this.ResultImage.TabStop = false;
            // 
            // DisplayBotConfig
            // 
            this.DisplayBotConfig.Location = new System.Drawing.Point(625, 54);
            this.DisplayBotConfig.Name = "DisplayBotConfig";
            this.DisplayBotConfig.Size = new System.Drawing.Size(81, 32);
            this.DisplayBotConfig.TabIndex = 9;
            this.DisplayBotConfig.Text = "View";
            this.DisplayBotConfig.UseVisualStyleBackColor = true;
            this.DisplayBotConfig.Click += new System.EventHandler(this.DisplayBotConfig_Click);
            // 
            // DisplayCallXML
            // 
            this.DisplayCallXML.Location = new System.Drawing.Point(625, 149);
            this.DisplayCallXML.Name = "DisplayCallXML";
            this.DisplayCallXML.Size = new System.Drawing.Size(81, 32);
            this.DisplayCallXML.TabIndex = 10;
            this.DisplayCallXML.Text = "View";
            this.DisplayCallXML.UseVisualStyleBackColor = true;
            this.DisplayCallXML.Click += new System.EventHandler(this.DisplayCallXML_Click);
            // 
            // ResultsDisplay
            // 
            this.ResultsDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ResultsDisplay.Location = new System.Drawing.Point(12, 379);
            this.ResultsDisplay.Name = "ResultsDisplay";
            this.ResultsDisplay.Size = new System.Drawing.Size(795, 223);
            this.ResultsDisplay.TabIndex = 11;
            this.ResultsDisplay.Text = "";
            // 
            // ClearBttn
            // 
            this.ClearBttn.Location = new System.Drawing.Point(726, 342);
            this.ClearBttn.Name = "ClearBttn";
            this.ClearBttn.Size = new System.Drawing.Size(81, 31);
            this.ClearBttn.TabIndex = 12;
            this.ClearBttn.Text = "Clear";
            this.ClearBttn.UseVisualStyleBackColor = true;
            this.ClearBttn.Click += new System.EventHandler(this.ClearBttn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(819, 614);
            this.Controls.Add(this.ClearBttn);
            this.Controls.Add(this.ResultsDisplay);
            this.Controls.Add(this.DisplayCallXML);
            this.Controls.Add(this.DisplayBotConfig);
            this.Controls.Add(this.ResultImage);
            this.Controls.Add(this.ValidateBttn);
            this.Controls.Add(this.CallXMLFileSearch);
            this.Controls.Add(this.BotConfigFileSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CallDataFileBox);
            this.Controls.Add(this.BotConfigFileBox);
            this.Name = "Form1";
            this.Text = "Recorder Bot Rules Validator";
            ((System.ComponentModel.ISupportInitialize)(this.ResultImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BotConfigFileBox;
        private System.Windows.Forms.TextBox CallDataFileBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BotConfigFileSearch;
        private System.Windows.Forms.Button CallXMLFileSearch;
        private System.Windows.Forms.Button ValidateBttn;
        private System.Windows.Forms.PictureBox ResultImage;
        private System.Windows.Forms.Button DisplayBotConfig;
        private System.Windows.Forms.Button DisplayCallXML;
        private System.Windows.Forms.RichTextBox ResultsDisplay;
        private System.Windows.Forms.Button ClearBttn;
    }
}

