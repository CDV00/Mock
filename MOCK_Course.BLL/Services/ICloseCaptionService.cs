using System;
using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;

namespace Course.BLL.Services
{
    public interface ICloseCaptionService
    {
        Task<Response<CloseCaptionDTO>> Add(CloseCaptionForCreateRequest closeCaptionCreateRequest, Guid courseId);
    }
}
