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

namespace Course.BLL.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// contructer CourseReviewService 
        /// </summary>
        /// <param name="courseReviewRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public AssignmentService(IAssignmentRepository assignmentRepository, IAttachmentRepository attachmentRepository,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _attachmentRepository = attachmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // Todo: filter and paging
        /// <summary>
        /// Get all course review
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<AssignmentDTO>> GetAll(Guid sectionId, AssignmentParameters assignmentParameters)
        {
            var assignment = await _assignmentRepository.BuildQuery()
                                                        .FilterByKeyword(assignmentParameters.Keyword)
                                                        .FilterBySectionId(sectionId)
                                                        .IncludeSection()
                                                        .ApplySort(assignmentParameters.Orderby)
                                                        .Skip((assignmentParameters.PageNumber - 1) * assignmentParameters.PageSize)
                                                        .Take(assignmentParameters.PageSize)
                                                        .ToListAsync(c => _mapper.Map<AssignmentDTO>(c));

            var count = await _assignmentRepository.BuildQuery()
                                                   .FilterByKeyword(assignmentParameters.Keyword)
                                                   .FilterBySectionId(sectionId)
                                                   .CountAsync();
            var pageList = new PagedList<AssignmentDTO>(assignment, count, assignmentParameters.PageNumber, assignmentParameters.PageSize);

            return pageList;
        }
        public async Task<Response<AssignmentDTO>> Add(AssignmentForCreateRequest assignmentForCreateRequest)
        {
            try
            {
                var assignment = _mapper.Map<Assignment>(assignmentForCreateRequest);
                assignment.CreatedAt = DateTime.Now;
                await _assignmentRepository.CreateAsync(assignment);

                await _unitOfWork.SaveChangesAsync();
                return new Response<AssignmentDTO>(true, _mapper.Map<AssignmentDTO>(assignment));
            }
            catch (Exception ex)
            {
                return new Response<AssignmentDTO>(false, ex.Message, null);
            }
        }


    }
}
