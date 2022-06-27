using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests
{
    public class NotificationRequest 
    {
        //public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
    }
}
