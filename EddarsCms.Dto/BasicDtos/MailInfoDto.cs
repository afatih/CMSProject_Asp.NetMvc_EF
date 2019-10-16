using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EddarsCms.Dto.BasicDtos
{
    public class MailInfoDto:DtoBase
    {
        public string SenderMail { get; set; }
        public string SenderMailPass { get; set; }
        public string ReceiverMail { get; set; }
        public string Caption { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
    }
}
