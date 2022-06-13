using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Emails
{
    public class SendGridConfiguration
    {
        public string ApiKey { get; set; }
        public bool ProductionMode { get; set; }
        public string AuthorizedSender { get; set; }
        public string AuthorizedRecipient { get; set; }
    }
}
