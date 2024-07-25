using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator
{
    internal class Participant
    {
        public string action { get; set; }
        public string displayName { get; set; }
        public string identityType { get; set; }
        public string isObserved { get; set; }
        public string participantId { get; set; }
        public string phoneId { get; set; }
        public string sourceId { get; set; }
        public string tenantId { get; set; }
        public string timeStamp { get; set; }
        public string userId { get; set; }
        public string userPrincipalName { get; set; }
    }
}
