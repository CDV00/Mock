using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class UpdateUserActiveRequest
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
}
