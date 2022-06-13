using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.DTO;
using Course.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Course.BLL.Responses;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;

namespace Course.BLL.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepositoty _sectionRepositoty;
        private readonly ILectureRepository _lessonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILectureService _lectureService;
        private readonly ILectureCompletionService _lectureCompletionService;
        public SectionService(ISectionRepositoty sectionRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork, ILectureRepository lessonRepository, ILectureService lectureService, ILectureCompletionService lectureCompletionService)
        {
            _sectionRepositoty = sectionRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _lessonRepository = lessonRepository;
            _lectureService = lectureService;
            _lectureCompletionService = lectureCompletionService;
        }

        public async Task<Response<int>> GetTotal(Guid courseId)
        {
            try
            {

                var count = await _sectionRepositoty.BuildQuery()
                                                    .FilterByCourseId(courseId)
                                                    .CountAsync();

                return new Response<int>(
                    true,
                    _mapper.Map<int>(count)
                );
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }
        public async Task<Response<SectionDTO>> Add(SectionCreateRequest sectionRequest)
        {
            try
            {
                var section = _mapper.Map<Section>(sectionRequest);
                section.CreatedAt = DateTime.Now;

                await _sectionRepositoty.CreateAsync(section);
                await _unitOfWork.SaveChangesAsync();

                return new Response<SectionDTO>(
                    true,
                    _mapper.Map<SectionDTO>(section)
                );
            }
            catch (Exception ex)
            {
                return new Response<SectionDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<SectionDTO>> GetAll(Guid courseId)
        {
            try
            {
                var result = await _sectionRepositoty.BuildQuery()
                                                     .FilterByCourseId(courseId)
                                                     .ToListAsync(s => _mapper.Map<SectionDTO>(s));

                return new Responses<SectionDTO>(true, _mapper.Map<IEnumerable<SectionDTO>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<SectionDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid idSection)
        {
            try
            {
                var section = await _sectionRepositoty.GetByIdAsync(idSection);
                if (section is null)
                {
                    return new BaseResponse(false, null, "can't find lesson");
                }

                //await _lessonRepository.RemoveBySectionId(idSection);
                var lessons = await _lessonRepository.BuildQuery()
                                                     .FilterBySectionId(idSection)
                                                     .ToListAsync(c => c);
                _lessonRepository.RemoveRange(lessons);
                _sectionRepositoty.Remove(section, false);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<SectionDTO>> Update(Guid id, SectionUpdateRequest sectionRequest)
        {
            try
            {

                var section = await _sectionRepositoty.GetByIdAsync(id);

                if (section is null)
                {
                    return new Response<SectionDTO>(false, null, "can't find section");
                }

                _mapper.Map(sectionRequest, section);
                await _unitOfWork.SaveChangesAsync();

                var sectionResponse = _mapper.Map<SectionDTO>(section);

                return new Response<SectionDTO>(
                    true,
                    _mapper.Map<SectionDTO>(sectionResponse)
                );
            }
            catch (Exception ex)
            {
                return new Response<SectionDTO>(false, ex.Message, null);
            }
        }
        //
        public async Task<float> PercentCourseCompletion(Guid courseId, Guid userId)
        {

            int countCourses = GetTotal(courseId).Result.data;
            float PercentCourseCompletion = 5/(countCourses * 100)/100;

            /*var IsExistEnrolls = await __enrollmentRepository.BuildQuery()
                                                            .FilterByCourseId(courseId)
                                                            .FilterByUserId(userId)
                                                            .AnyAsync();
            if (!IsExistEnrolls)
                return new Response<float>(true, 0);

            var courses = await _courseReviewRepository.BuildQuery()
                                                       .FilterByCourseId(courseId)
                                                       .FilterByUserId(userId)
                                                       .GetAvgRate();*/

            return PercentCourseCompletion;

        }
        //
        public async Task<float> PercentSectionCompletion(Guid sectionId)
        {

            int countTotalLecture = await _lectureService.totalLectureBySection(sectionId);
            var ListLecture = await _lectureService.GetAll(sectionId);
            //var countTotalLectureCompletion = await _lectureCompletionService.IsCompletion(ListLecture[1].)
            float CourseCompletion = 5;

            

            return CourseCompletion;

        }
    }
}
