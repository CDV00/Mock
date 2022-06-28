using Course.DAL.Models;
using Course.DAL.Queries.Abstraction;
using System;

namespace Course.DAL.Queries
{
    public interface ILectureAttachmentQuery : IQuery<LectureAttachment>
    {
        LectureAttachmentQuery FilterById(Guid id);
    }
}