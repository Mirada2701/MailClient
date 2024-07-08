using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public class MessInfo
    {
        public InternetAddressList From { get; set; }
        public InternetAddressList To { get; set; }
        public string Theme { get; set; }
        public string Body { get; set; }
        public IEnumerable<MimeEntity> Attach { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
