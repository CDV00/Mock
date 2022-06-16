using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;

namespace Course.BLL.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUploadFileService _uploadFileService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentService(IAttachmentRepository attachmentRepository, IUploadFileService uploadFileService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _attachmentRepository = attachmentRepository;
            _uploadFileService = uploadFileService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<AttachmentDTO>> GetAll()
        {
            try
            {
                var attachment = await _attachmentRepository.BuildQuery()
                                                            .AsSelectorAsync(x => _mapper.Map<AttachmentDTO>(x));

                return new Response<AttachmentDTO>(true, attachment);
            }
            catch (Exception ex)
            {
                return new Response<AttachmentDTO>(false, ex.Message, null);
            }
        }
        //public async Task<Response<AttachmentDTO>> Add(AttachmentForCreateRequest attachmentForCreateRequest)
        //{
        //    try
        //    {
        //        var attachment = _mapper.Map<Attachment>(attachmentForCreateRequest);
        //        var uploadResponse = await _uploadFileService.UploadFile(attachmentForCreateRequest.FileUrl);
        //        if (uploadResponse.IsSuccess == true)
        //        {
        //            attachment.FileUrl = uploadResponse.url;
        //        }
        //        else
        //        {
        //            attachment.FileUrl = null;
        //        }
        //        //uploadResponse.IsSuccess ? uploadResponse.url : null;

        //        await _attachmentRepository.CreateAsync(attachment);

        //        await _unitOfWork.SaveChangesAsync();
        //        return new Response<AttachmentDTO>(true, _mapper.Map<AttachmentDTO>(attachment));
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<AttachmentDTO>(false, ex.Message, null);
        //    }
        //}
    }
}
