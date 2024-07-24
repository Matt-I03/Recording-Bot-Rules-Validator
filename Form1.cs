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
                openFileDialog.Filter = "All files (*.*)|*.*|XML files (*.xml)|*.xml";
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

            if (recordRes && meetingRes)
            {
                ResultsDisplay.AppendText("Recording Filters: Passed" + Environment.NewLine);
                ResultsDisplay.AppendText(Environment.NewLine + "Online Meeting Filters: Passed");

                ResultImage.Image = Properties.Resources.Check_Mark;
            }
            else if (recordRes)
            {
                ResultsDisplay.AppendText("Recording Filters: Passed" + Environment.NewLine);

                ResultsDisplay.AppendText(Environment.NewLine + "Failed Meeting Filters: " + Environment.NewLine);
                ResultsDisplay.AppendText(data.FailedConditionsToString("OnlineMeetingFilters").ToString());

                ResultImage.Image = Properties.Resources.X_Mark;
            }
            else if (meetingRes)
            {
                ResultsDisplay.AppendText("Failed Recording Filters: " + Environment.NewLine);
                ResultsDisplay.AppendText(data.FailedConditionsToString("RecordingFilters").ToString());

                ResultsDisplay.AppendText(Environment.NewLine + "Online Meeting Filters: Passed");

                ResultImage.Image = Properties.Resources.X_Mark;

            }
            else
            {
                ResultsDisplay.AppendText("Failed Recording Filters: " + Environment.NewLine);
                ResultsDisplay.AppendText(data.FailedConditionsToString("RecordingFilters").ToString());

                ResultsDisplay.AppendText(Environment.NewLine + "Failed Meeting Filters: " + Environment.NewLine);
                ResultsDisplay.AppendText(data.FailedConditionsToString("OnlineMeetingFilters").ToString());

                ResultImage.Image = Properties.Resources.X_Mark;
            }
        }
    }
}
