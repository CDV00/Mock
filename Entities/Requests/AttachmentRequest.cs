using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class AttachmentRequest
    {
        public IFormFile FileUrl { get; set; }
    }
    public class AttachmentForCreateRequest
    {
        public string Name { get; set; }
        public string FileUrl { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
    }
    public class AttachmentForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FileUrl { get; set; } = "";
        public bool IsDeleted { get; set; } = false;
    }
}
