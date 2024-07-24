using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DeserializeV2
{
    internal class Data
    {
        public Dictionary<string, string> ConfigSettings { get; set; } = new Dictionary<string, string>();
        public List<Dictionary<string, string>> Participants { get; set; } = new List<Dictionary<string, string>>();
        public List<Condition> RecordingConditions { get; set; } = new List<Condition>();
        public List<Condition> OnlineMeetingConditions { get; set; } = new List<Condition>();

        // Functions to Deserialize different files
        public void DeserializeJsonConfig(string fileContents) //Deserializes Json file containing bot config
        {
            var jsonDoc = JsonDocument.Parse(fileContents);

            foreach (var element in jsonDoc.RootElement.EnumerateObject())
            {
                string key = element.Name;
                string value = element.Value.ToString();

                //Deserializes any json filters into the a list of conditions and puts any of the values in ConfigSettings
                switch (key)
                {
                    case "OnlineMeetingFilter": 
                        this.OnlineMeetingConditions = JsonSerializer.Deserialize<List<Condition>>(value.TrimEnd(';'), 
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        break;
                    case "RecordingFilter":
                        this.RecordingConditions = JsonSerializer.Deserialize<List<Condition>>(value.TrimEnd(';'), 
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        break;
                    default:
                        this.ConfigSettings[key] = value;
                        break;
                }
            }
        }

        public void DeserializeXmlConfig(string filePath) //Deserializes an xml file containing bot config
        {
            XDocument xdoc = XDocument.Load(filePath);

            foreach (var element in xdoc.Descendants())
            {
                if (!element.HasElements)
                {
                    if (element.Name.LocalName == "Value")
                    {
                        if (element.Parent.Parent.Parent.Name.LocalName == "SelectRecordingFilters") // The third parent is the category we need to check to sort conditions
                            this.RecordingConditions.Add(Condition.TokenizeRecordingFilters(this, element.Parent.ToString()));
                        else if (element.Parent.Parent.Parent.Name.LocalName == "OnlineMeetingOptions")
                            this.OnlineMeetingConditions.Add(Condition.TokenizeOnlineMeetingFilters(this, element.Parent.ToString()));
                    }
                    else if (element.Parent.Name.LocalName == "SelectRecordingFilters" && element.Parent.Name.LocalName != "Filters")
                        this.ConfigSettings[element.Name.LocalName] = element.Value;
                    else
                        this.ConfigSettings[element.Name.LocalName] = element.Value;
                }
            }
        }

        public void DeserializeXmlMeetingDetails(string filePath) //Deserializes an xml file containg meeting data
        {
            XDocument xdoc = XDocument.Load(filePath);

            foreach (var element in xdoc.Descendants())
            {
                if (!element.HasElements && element.Parent.Name.LocalName != "RosterChangeItem")
                {
                    this.ConfigSettings[element.Name.LocalName] = element.Value;
                }
                else if (element.Name.LocalName == "RosterChangeItem")
                {
                    var participant = new Dictionary<string, string>();
                    foreach (var subE in element.Descendants())
                    {
                        participant[subE.Name.LocalName] = subE.Value;
                    }
                    this.Participants.Add(participant);
                }
            }
        }

        public void UpdateConditions()
        {
            foreach (Condition m in this.OnlineMeetingConditions)
            {
                m.UpdateCondition(this);
            }

            foreach (Condition m in this.RecordingConditions)
            {
                m.UpdateCondition(this);
            }
        }

        public void Clear()
        {
            this.ConfigSettings.Clear();
            this.Participants.Clear();
            this.RecordingConditions.Clear();
            this.OnlineMeetingConditions.Clear();
        }

        public void DisplayData()
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("Configuration Settings:");
            foreach (var setting in ConfigSettings)
            {
                message.AppendLine($"{setting.Key}: {setting.Value}");
            }

            MessageBox.Show(message.ToString());

            message.Clear();

            message.AppendLine("\nRecording Conditions:");
            foreach (var condition in RecordingConditions)
            {
                message.AppendLine(condition.ToString());
            }

            message.AppendLine("\nOnline Meeting Conditions:");
            foreach (var condition in OnlineMeetingConditions)
            {
                message.AppendLine(condition.ToString());
            }

            MessageBox.Show(message.ToString());
        }

        public void ShowFailedConditions(string choice)
        {
            StringBuilder failedConditions = new StringBuilder();

            failedConditions.AppendLine("Failed Conditions: \n");

            if (choice == "RecordingFilter")
            {
                foreach (var m in this.RecordingConditions.Where(m => m.Flag == true))
                {
                    failedConditions.AppendLine(m.ToString());
                }
            }

            if (choice == "OnlineMeetingFilter")
            {
                foreach (var m in this.OnlineMeetingConditions.Where(m => m.Flag == true))
                {
                    failedConditions.AppendLine(m.ToString());
                }
            }

            MessageBox.Show(failedConditions.ToString());
        }
    }
}

