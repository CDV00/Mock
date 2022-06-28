using Course.BLL.Share.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ParameterRequest
{
    public class DepositParameters : RequestParameters
    {
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string Orderby { get; set; }
    }
}
