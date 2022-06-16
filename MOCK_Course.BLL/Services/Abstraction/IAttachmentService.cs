using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;

namespace Course.BLL.Services.Abstraction
{
    public interface IAttachmentService
    {
        Task<Response<AttachmentDTO>> GetAll();
        //Task<Response<AttachmentDTO>> Add(AttachmentForCreateRequest attachmentForCreateRequest);
    }
}
