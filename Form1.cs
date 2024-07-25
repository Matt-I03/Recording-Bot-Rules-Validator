using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Windows.Forms.VisualStyles;

namespace DeserializeV2
{
    public partial class Form1 : Form
    {
        private Data data = new Data();

        public Form1()
        {
            InitializeComponent();
        }

        private void BotConfigFileSearch_Click(object sender, EventArgs e)
        {
            data.ConfigSettings.Clear();
            data.RecordingConditions.Clear();
            data.OnlineMeetingConditions.Clear();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All files (*.*)|*.*|JSON files (*.json)|*.json|XML files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);
                    string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                    if (fileExtension == ".json")
                    {
                        data.DeserializeJsonConfig(filePath);
                        BotConfigFileBox.Text = fileName;
                    }
                    else if (fileExtension == ".config" || fileExtension == ".xml")
                    {
                        data.DeserializeXmlConfig(filePath);
                        BotConfigFileBox.Text = fileName;
                    }
                    else
                        MessageBox.Show("Unsupported file type.");
                }
            }

            data.UpdateConditions();
        }

        private void CallXMLFileSearch_Click(object sender, EventArgs e)
        {
            data.CallDetails.Clear();
            data.Participants.Clear();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);
                    string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                    if (fileExtension == ".xml")
                    {
                        data.DeserializeXmlMeetingDetails(filePath);
                        CallDataFileBox.Text = fileName;
                    }
                    else
                        MessageBox.Show("Unsupported file type.");
                }
            }

            data.UpdateConditions();
        }

        private void DisplayBotConfig_Click(object sender, EventArgs e)
        {
            data.DisplayConfigSettings();
        }

        private void DisplayCallXML_Click(object sender, EventArgs e)
        {
            data.DisplayCallDetails();
        }

        private void ValidateBttn_Click(object sender, EventArgs e)
        {
            ResultsDisplay.Clear();

            StringBuilder displayText = new StringBuilder();
            data.UpdateConditions();

            bool recordRes = Condition.ShouldRecord(data);
            bool meetingRes = Condition.ShouldRecordOnlineMeeting(data);

            DisplayRes(recordRes, meetingRes);
        }

        private void ClearBttn_Click(object sender, EventArgs e)
        {
            data.Clear();
            ResultsDisplay.Clear();
            ResultImage.Image = null;
            BotConfigFileBox.Clear();
            CallDataFileBox.Clear();
        }

        public void DisplayRes(bool recordRes, bool meetingRes)
        {
            if (recordRes && meetingRes)
            {
                AppendTextWithFormatting("Recording Filters:", true);
                AppendTextWithFormatting(" Passed\n");
                AppendTextWithFormatting("\nOnline Meeting Filters:", true);
                AppendTextWithFormatting(" Passed");

                ResultImage.Image = Properties.Resources.Check_Mark;
            }
            else if (recordRes)
            {
                AppendTextWithFormatting("Recording Filters:", true);
                AppendTextWithFormatting(" Passed\n");

                AppendTextWithFormatting("\nMeeting Filters:", true);
                AppendTextWithFormatting(" Failed\n");
                AppendTextWithFormatting(data.FailedConditionsToString("OnlineMeetingFilters").ToString(), color: Color.Red);

                ResultImage.Image = Properties.Resources.X_Mark;
            }
            else if (meetingRes)
            {
                AppendTextWithFormatting("Recording Filters:", true);
                AppendTextWithFormatting(" Failed\n");
                AppendTextWithFormatting(data.FailedConditionsToString("RecordingFilters").ToString(), color: Color.Red);

                AppendTextWithFormatting("\nMeeting Filters:", true);
                AppendTextWithFormatting(" Passed\n");

                ResultImage.Image = Properties.Resources.X_Mark;

            }
            else
            {
                AppendTextWithFormatting("Recording Filters:", true);
                AppendTextWithFormatting(" Failed\n");
                AppendTextWithFormatting(data.FailedConditionsToString("RecordingFilters").ToString(), color: Color.Red);

                AppendTextWithFormatting("\nMeeting Filters:", true);
                AppendTextWithFormatting(" Failed\n"); 
                AppendTextWithFormatting(data.FailedConditionsToString("OnlineMeetingFilters").ToString(), color: Color.Red);

                ResultImage.Image = Properties.Resources.X_Mark;
            }
        }

        // Appends text to display box with certain color/bolding with option to choose
        public void AppendTextWithFormatting(string text, bool bold = false, Color? color = null)
        {
            int start = ResultsDisplay.TextLength;
            ResultsDisplay.AppendText(text);
            int end = ResultsDisplay.TextLength;
            ResultsDisplay.Select(start, end - start);
            ResultsDisplay.SelectionFont = new Font(ResultsDisplay.Font, bold ? FontStyle.Bold : FontStyle.Regular);
            ResultsDisplay.SelectionColor = color ?? Color.Black;
            ResultsDisplay.SelectionLength = 0;
        }
    }
}
