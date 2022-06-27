using Course.BLL.Share.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ParameterRequest
{
    public class DiscountParameters : RequestParameters
    {
        public string OrderBy { get; set; } = "CreateAt desc";
    }
}
