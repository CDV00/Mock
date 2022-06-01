using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public interface ILectureService
    {
        Task<Responses<LectureDTO>> GetAll(Guid courseId);
        Task<Response<LectureDTO>> Add(LectureForCreateRequest LectureRequest);
        Task<BaseResponse> Remove(Guid idLecture);
        Task<Response<LectureDTO>> Update(Guid id, LectureForUpdateRequest LectureRequest);
    }
}
