using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace DeserializeV2
{
    internal class Condition
    {
        public string Operator { get; set; }
        public string LeftSideParameter { get; set; }
        public string LeftSideValue { get; set; }
        public string LogicalOperator { get; set; }
        public string RightSideParameter { get; set; }
        public bool Flag { get; set; } = false;

        public override string ToString()
        {
            string final = $"Operator: {Operator} | Left Param: {LeftSideParameter} | " +
                           $"Left Value: {(String.IsNullOrEmpty(LeftSideValue) ? "N/A" : LeftSideValue)} | " +
                           $"Logical Operator: {LogicalOperator} | Right Param: {RightSideParameter}\n";

            return final;
        }

        public void UpdateCondition(Data data) // Resets flag and gets value for LeftSideValue
        {
            this.Flag = false;
            this.LeftSideValue = data.ConfigSettings.ContainsKey(this.LeftSideParameter)
                                 ? data.ConfigSettings[this.LeftSideParameter]
                                 : string.Empty;
        }

        private static bool EvaluateCondition(Condition condition)
        {
            switch (condition.LogicalOperator)
            {
                case "eq":
                    return condition.LeftSideValue == condition.RightSideParameter;
                case "ne":
                    return condition.LeftSideValue != condition.RightSideParameter;
                default:
                    return false;
            }
        }

        private static bool SelectRecordingFiltersCompare(IEnumerable<Condition> conditions, string comparisonOperator, string boolOperator)
        {
            bool bDecision = boolOperator == "and";

            foreach (Condition condition in conditions)
            {
                if (condition.Operator.ToLower() != boolOperator || string.IsNullOrEmpty(condition.LeftSideValue))
                    continue;

                if (condition.LogicalOperator == comparisonOperator)
                {
                    if (!EvaluateCondition(condition))
                    {
                        condition.Flag = true;
                        if (boolOperator == "and") bDecision = false;
                    }
                    else if (boolOperator == "or") bDecision = true;
                }
            }

            return bDecision;
        }

        private static bool SelectRecordingFiltersContains(IEnumerable<Condition> conditions, string boolOperator)
        {
            bool bDecision = boolOperator == "and";

            foreach (var condition in conditions)
            {
                if (condition.Operator.ToLower() != boolOperator || string.IsNullOrEmpty(condition.LeftSideValue))
                    continue;

                if (condition.LogicalOperator == "Contains")
                {
                    if (condition.LeftSideValue.Contains(condition.RightSideParameter))
                    {
                        if (boolOperator == "or") bDecision = true;
                    }
                    else
                    {
                        condition.Flag = true;
                        if (boolOperator == "and") bDecision = false;
                    }
                }
            }

            return bDecision;
        }

        private static bool SelectRecordingFiltersExcludes(IEnumerable<Condition> conditions, string boolOperator)
        {
            bool bDecision = boolOperator == "and";

            foreach (var condition in conditions)
            {
                if (condition.Operator.ToLower() != boolOperator || string.IsNullOrEmpty(condition.LeftSideValue))
                    continue;

                if (condition.LogicalOperator == "Excludes")
                {
                    if (condition.LeftSideValue.Contains(condition.RightSideParameter))
                    {
                        condition.Flag = true;
                        if (boolOperator == "and") bDecision = false;
                    }
                    else if (boolOperator == "or") bDecision = true;
                }
            }

            return bDecision;
        }

        private static void FlagFailingConditions(IEnumerable<Condition> conditions, string boolOperator) // Flags all failing conditions as ShouldRecord() functions doesn't always get to all of them
        {
            foreach (var condition in conditions)
            {
                if (condition.Operator.ToLower() != boolOperator)
                    continue;

                bool isFailingCondition = false;

                switch (condition.LogicalOperator)
                {
                    case "eq":
                        isFailingCondition = condition.LeftSideValue != condition.RightSideParameter;
                        break;
                    case "ne":
                        isFailingCondition = condition.LeftSideValue == condition.RightSideParameter;
                        break;
                    case "Contains":
                        isFailingCondition = !condition.LeftSideValue.Contains(condition.RightSideParameter);
                        break;
                    case "Excludes":
                        isFailingCondition = condition.LeftSideValue.Contains(condition.RightSideParameter);
                        break;
                    default:
                        continue;
                }

                condition.Flag = isFailingCondition;
            }
        }

        public static bool ShouldRecord(Data data)
        {
            if (!BasicRecordingFiltersCheck(data))
                return false;

            if (data.RecordingConditions == null || data.RecordingConditions.Count() == 0)
                return true;

            if (data.RecordingConditions.Any(m => m.LogicalOperator.ToLower() == "any"))
                return true;
            if (data.RecordingConditions.Any(m => m.LogicalOperator.ToLower() == "all"))
                return true;
            if (data.RecordingConditions.Any(m => m.LogicalOperator.ToLower() == "none"))
                return false;

            // Have to go through all and flag failing conditions or else it won't check them all if condition wrong earlier
            FlagFailingConditions(data.RecordingConditions, "and");
            FlagFailingConditions(data.RecordingConditions, "or");

            if (data.RecordingConditions.Any(m => m.Operator.ToLower() == "and"))
            {
                if (!SelectRecordingFiltersCompare(data.RecordingConditions.Where(m => m.LogicalOperator == "eq"), "eq", "and"))
                    return false;
                if (!SelectRecordingFiltersCompare(data.RecordingConditions.Where(m => m.LogicalOperator == "ne"), "ne", "and"))
                    return false;
                if (!SelectRecordingFiltersContains(data.RecordingConditions.Where(m => m.LogicalOperator == "Contains"), "and"))
                    return false;
                if (!SelectRecordingFiltersExcludes(data.RecordingConditions.Where(m => m.LogicalOperator == "Excludes"), "and"))
                    return false;
            }

            if (data.RecordingConditions.Any(m => m.Operator.ToLower() == "or"))
            {
                if (SelectRecordingFiltersCompare(data.RecordingConditions.Where(m => m.LogicalOperator == "eq"), "eq", "or"))
                    return true;
                if (SelectRecordingFiltersCompare(data.RecordingConditions.Where(m => m.LogicalOperator == "ne"), "ne", "or"))
                    return true;
                if (SelectRecordingFiltersContains(data.RecordingConditions.Where(m => m.LogicalOperator == "Contains"), "or"))
                    return true;
                if (SelectRecordingFiltersExcludes(data.RecordingConditions.Where(m => m.LogicalOperator == "Excludes"), "or"))
                    return true;
                return false;
            }

            return true;
        }

        public static bool ShouldRecordOnlineMeeting(Data data)
        {
            if (!BasicRecordingFiltersCheck(data))
                return false;

            // Call to get IsMeeting in bool
            var strIsMeeting = data.ConfigSettings.ContainsKey("IsMeeting")
                ? data.ConfigSettings["IsMeeting"]
                : string.Empty;
                bool.TryParse(strIsMeeting, out var bIsMeeting);

            if (bIsMeeting)
            {
                if (data.OnlineMeetingConditions == null || data.OnlineMeetingConditions.Count() == 0)
                    return true;

                if (data.OnlineMeetingConditions.Any(m => m.LogicalOperator.ToLower() == "any"))
                    return true;
                if (data.OnlineMeetingConditions.Any(m => m.LogicalOperator.ToLower() == "all"))
                    return true;
                if (data.OnlineMeetingConditions.Any(m => m.LogicalOperator.ToLower() == "none"))
                    return false;

                // Have to go through all and flag failing conditions or else it won't check them all if condition wrong earlier
                FlagFailingConditions(data.OnlineMeetingConditions, "and");
                FlagFailingConditions(data.OnlineMeetingConditions, "or");

                if (data.OnlineMeetingConditions.Any(m => m.Operator.ToLower() == "and"))
                {
                    if (!SelectRecordingFiltersCompare(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "eq"), "eq", "and"))
                        return false;
                    if (!SelectRecordingFiltersCompare(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "ne"), "ne", "and"))
                        return false;
                    if (!SelectRecordingFiltersContains(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "Contains"), "and"))
                        return false;
                    if (!SelectRecordingFiltersExcludes(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "Excludes"), "and"))
                        return false;
                }

                if (data.OnlineMeetingConditions.Any(m => m.Operator.ToLower() == "or"))
                {
                    if (SelectRecordingFiltersCompare(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "eq"), "eq", "or"))
                        return true;
                    if (SelectRecordingFiltersCompare(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "ne"), "ne", "or"))
                        return true;
                    if (SelectRecordingFiltersContains(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "Contains"), "or"))
                        return true;
                    if (SelectRecordingFiltersExcludes(data.OnlineMeetingConditions.Where(m => m.LogicalOperator == "Excludes"), "or"))
                        return true;
                    return false;
                }
            }

            return true;
        }
 
        private static bool BasicRecordingFiltersCheck(Data data)
        {
            bool bIncoming = true, bOutgoing = true, bExternal = true, bInternal = true, bPSTN = true, bIsMeeting = false, bIsACDQueueSource = false,
                bSelectIncomingCall = true, bSelectOutgoingCall = true, bSelectInternalCall = true, bSelectExternalCall = true, bSelectOnlineMeetingOnly = false, bSelectPSTNCallOnly = false;
            // Bunch of calls to get strings into bools
            var strExternal = data.ConfigSettings.ContainsKey("External")
                     ? data.ConfigSettings["External"]
                     : string.Empty;
            bool.TryParse(strExternal, out bExternal);
            bInternal = !bExternal;

            var strCallDirection = data.ConfigSettings.ContainsKey("CallDirection")
                     ? data.ConfigSettings["CallDirection"]
                     : string.Empty;
            bIncoming = strCallDirection == "I";
            bOutgoing = strCallDirection == "O";

            var strPSTN = data.ConfigSettings.ContainsKey("PSTN")
                     ? data.ConfigSettings["PSTN"]
                     : string.Empty;
            bool.TryParse(strPSTN, out bPSTN);

            var strIsMeeting = data.ConfigSettings.ContainsKey("IsMeeting")
                     ? data.ConfigSettings["IsMeeting"]
                     : string.Empty;
            bool.TryParse(strIsMeeting, out bIsMeeting);

            var strIsACDQueueSource = data.ConfigSettings.ContainsKey("IsACDQueueSource")
                     ? data.ConfigSettings["IsACDQueueSource"]
                     : string.Empty;
            bool.TryParse(strIsACDQueueSource, out bIsACDQueueSource);

            var strSelectIncoming = data.ConfigSettings.ContainsKey("IncomingCall")
                    ? data.ConfigSettings["IncomingCall"]
                    : string.Empty;
            bool.TryParse(strSelectIncoming, out bSelectIncomingCall);


            var strSelectOutgoing = data.ConfigSettings.ContainsKey("OutgoingCall")
                    ? data.ConfigSettings["OutgoingCall"]
                    : string.Empty;
            bool.TryParse(strSelectOutgoing, out bSelectOutgoingCall);


            var strSelectInternal = data.ConfigSettings.ContainsKey("InternalCall")
                    ? data.ConfigSettings["InternalCall"]
                    : string.Empty;
            bool.TryParse(strSelectInternal, out bSelectInternalCall);


            var strSelectExternal = data.ConfigSettings.ContainsKey("ExternalCall")
                    ? data.ConfigSettings["ExternalCall"]
                    : string.Empty;
            bool.TryParse(strSelectExternal, out bSelectExternalCall);


            var strSelectOnlineMeeting = data.ConfigSettings.ContainsKey("OnlineMeetingOnly")
                    ? data.ConfigSettings["OnlineMeetingOnly"]
                    : string.Empty;
            bool.TryParse(strSelectOnlineMeeting, out bSelectOnlineMeetingOnly);


            var strSelectPSTNOnly = data.ConfigSettings.ContainsKey("PstnCallOnly")
                    ? data.ConfigSettings["PstnCallOnly"]
                    : string.Empty;
            bool.TryParse(strSelectPSTNOnly, out bSelectPSTNCallOnly);

            if (!bSelectIncomingCall && !bIsMeeting && !bIsACDQueueSource)
                if (bIncoming)
                    return false;

            if (!bSelectOutgoingCall && !bIsMeeting && !bIsACDQueueSource)
                if (bOutgoing)
                    return false;

            if (!bSelectInternalCall && !bIsMeeting && !bIsACDQueueSource)
                if (bInternal)
                    return false;

            if (!bSelectExternalCall && !bIsMeeting && !bIsACDQueueSource)
                if (bExternal)
                    return false;

            if (bSelectPSTNCallOnly)
                if (!bPSTN)
                    return false;

            if (bSelectOnlineMeetingOnly)
                if (!bIsMeeting)
                    return false;

            return true;
        }

        // 2 functions below convert recording filters/online meeting filters strings into condition class
        public static Condition TokenizeRecordingFilters(Data data, string filterString)
        {
            Condition condition = new Condition();

            string pattern = @"<Filter Operator=""(?<Operator>[^""]+)"">\s*<Value>\s*'{(?<Field>[^']+)}'\s*(?<ComparisonOperator>\b(?:eq|ne)\b)\s*'(?<Value>[^']+)'"; 

            var match = Regex.Match(filterString, pattern);

            if (match.Success)
            {
                condition.Operator = match.Groups["Operator"].Value.Trim();
                condition.LeftSideParameter = match.Groups["Field"].Value.Trim();
                condition.LogicalOperator = match.Groups["ComparisonOperator"].Value.Trim();
                condition.RightSideParameter = match.Groups["Value"].Value.Trim();
            }

            condition.UpdateCondition(data);

            return condition;
        }

        public static Condition TokenizeOnlineMeetingFilters(Data data, string filterString)
        {
            Condition condition = new Condition();

            string pattern = @"<Filter Operator=""(?<Operator>[^""]+)"">\s*<Value>\s*'{(?<Field>[^']+)}'\s*(?<ComparisonOperator>\b(?:Contains|Excludes)\b)\s*\('(?<Value>[^)]+)'\)";

            var match = Regex.Match(filterString, pattern);

            if (match.Success)
            {
                condition.Operator = match.Groups["Operator"].Value.Trim();
                condition.LeftSideParameter = match.Groups["Field"].Value.Trim();
                condition.LogicalOperator = match.Groups["ComparisonOperator"].Value.Trim();
                condition.RightSideParameter = match.Groups["Value"].Value.Trim();
            }

            condition.UpdateCondition(data);

            return condition;
        }
    }
}
