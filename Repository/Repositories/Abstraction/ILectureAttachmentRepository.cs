using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Repositories.Abstraction;

namespace Repository.Repositories.Abstraction
{
    public interface ILectureAttachmentRepository : IRepository<LectureAttachment>
    {
        ILectureAttachmentQuery BuildQuery();
    }
}