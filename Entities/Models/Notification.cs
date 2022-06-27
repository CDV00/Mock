using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.Models
{
    public class Notification : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public string Messenge { get; set; }
        //public string Header { get; set; }
        //public string Body { get; set; }
        //public string Url { get; set; }
        public AppUser User { get; set; }

        /*public Notification(){
            IsRead = false;
            IsReceived = false;
        }*/
     }
    
}
