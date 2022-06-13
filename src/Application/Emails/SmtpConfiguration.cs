using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Emails
{
    public class SmtpConfiguration
    {
        public string Url { get; set; }
        public int Port { get; set; }
        public bool AuthenticationRequired { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
