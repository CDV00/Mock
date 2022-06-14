using Course.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.DTO
{
    public class AttachmentDTO : BaseEntity<Guid>
    {
        public Guid AssignmentId { get; set; }
        public IFormFile FileUrl { get; set; }
    }
}
