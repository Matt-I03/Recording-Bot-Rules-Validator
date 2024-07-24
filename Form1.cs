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

namespace DeserializeV2
{
    public partial class Form1 : Form
    {
        private Data data = new Data();

        public Form1()
        {
            InitializeComponent();
        }

        private void JsonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(JsonTextBox.Text))
                {
                    string contents = JsonTextBox.Text;

                    data.DeserializeJsonConfig(contents);

                    data.UpdateConditions();
                }
            }
            catch (Exception f) { }
        }

        private void XmlFileDrop_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void XmlFileDrop_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);

                string filename = Path.GetFileName(filePath[0]);
                XmlFileDrop.Text = filename;

                data.DeserializeXmlConfig(filePath[0]);
            }

            data.UpdateConditions();
        }

        private void XmlFileDrop2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);

                string filename = Path.GetFileName(filePath[0]);
                XmlFileDrop2.Text = filename;

                data.DeserializeXmlMeetingDetails(filePath[0]);
            }

            data.UpdateConditions();
        }

        private void OnlineMeetingButton_Click(object sender, EventArgs e)
        {
            if (Condition.ShouldRecordOnlineMeeting(data))
            {
                MessageBox.Show("Recording");
            } 
            else
            {
                MessageBox.Show("Could not record");
                data.ShowFailedConditions("OnlineMeetingFilter");
            }
        }

        private void RecordingFilterButton_Click(object sender, EventArgs e)
        {
            if (Condition.ShouldRecord(data))
            {
                MessageBox.Show("Recording");
            }
            else
            {
                MessageBox.Show("Could not record");
                data.ShowFailedConditions("RecordingFilter");
            }
        }

        private void ClearBttn_Click(object sender, EventArgs e)
        {
            data.Clear();
            XmlFileDrop.Text = "";
            XmlFileDrop2.Text = "";
            JsonTextBox.Text = "";
        }

        private void DisplayDataBttn_Click(object sender, EventArgs e)
        {
            data.DisplayData();
        }
    }
}
