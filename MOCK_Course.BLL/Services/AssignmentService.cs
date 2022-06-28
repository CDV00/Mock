using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Entities.Responses;

namespace Course.BLL.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// construct CourseReviewService 
        /// </summary>
        /// <param name="courseReviewRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public AssignmentService(IAssignmentRepository assignmentRepository, IAttachmentRepository attachmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _attachmentRepository = attachmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //load attacment
        // Todo: filter and paging
        /// <summary>
        /// Get all assignment
        /// </summary>
        /// <returns></returns>
        public async Task<ApiBaseResponse> GetAllAssignment(AssignmentParameters parameter)
        {
            var assignments = await _assignmentRepository.GetAllAssignment(parameter);

            return new ApiOkResponse<PagedList<AssignmentDTO>>(assignments);
        }

    }
}
