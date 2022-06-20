using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class ExternalLoginResquest
    {
        public string Provider { get; set; }
        public string Token { get; set; }

    }
}
