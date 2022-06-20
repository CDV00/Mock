using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class AssignmentRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        //public IList<AttachmentRequest> Attachments { get; set; }
        public Guid SectionId { get; set; }
        public int Index { get; set; }
    }
    public class AssignmentForCreateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IList<AttachmentForCreateRequest> Attachments { get; set; }
        //public Guid SectionId { get; set; }
        public int Index { get; set; }
    }
    public class AssignmentForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IList<AttachmentForUpdateRequest> Attachments { get; set; }
        //public Guid SectionId { get; set; }
        public int Index { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }
    }

}
