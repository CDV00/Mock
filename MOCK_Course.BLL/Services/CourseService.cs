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

namespace Course.BLL.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICousesRepository _cousesRepository;
        private readonly IAudioLanguageRepository _audioLanguageRepository;
        private readonly ICloseCaptionRepository _closeCaptionRepository;
        private readonly ILevelRepository _levelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService(ICousesRepository cousesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork, IAudioLanguageRepository audioLanguageRepository,
            ICloseCaptionRepository closeCaptionRepository, ILevelRepository levelRepository)
        {
            _cousesRepository = cousesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _audioLanguageRepository = audioLanguageRepository;
            _closeCaptionRepository = closeCaptionRepository;
            _levelRepository = levelRepository;
        }

        public async Task<PagedList<CourseDTO>> GetCoursesAsync(CourseParameters courseParameter)
        {
            var coursesDTO = await _cousesRepository.BuildQuery()
                                                    .IncludeCategory()
                                                    .IncludeUser()
                                                    .FilterByKeyword(courseParameter.Keyword)
                                                    .FilterByCategoryId(courseParameter.CategoryId)
                                                    .FilterByAudioLanguageIds(courseParameter.AudioLanguageIds)
                                                    .FilterByCloseCaptionIds(courseParameter.CloseCaptionIds)
                                                    .FilterByLevelIds(courseParameter.LevelIds)
                                                    .FilterByDiscount(courseParameter.IsDiscount)
                                                    .ApplySort(courseParameter.Orderby)
                                                    .Skip((courseParameter.PageNumber - 1) * courseParameter.PageSize)
                                                    .Take(courseParameter.PageSize)
                                                    .ToListAsync(c => _mapper.Map<CourseDTO>(c));

            var count = await _cousesRepository.BuildQuery()
                                                    .FilterByKeyword(courseParameter.Keyword)
                                                    .FilterByCategoryId(courseParameter.CategoryId)
                                                    .FilterByAudioLanguageIds(courseParameter.AudioLanguageIds)
                                                    .FilterByCloseCaptionIds(courseParameter.CloseCaptionIds)
                                                    .FilterByLevelIds(courseParameter.LevelIds)
                                                    .FilterByDiscount(courseParameter.IsDiscount)
                                                    .CountAsync();


            // TODO: use For to add total enroll and rating of course

            var pageList = new PagedList<CourseDTO>(coursesDTO, count, courseParameter.PageNumber, courseParameter.PageSize);

            return pageList;
            //return (courses: new Response<IList<CourseDTO>>(true , coursesDTO), metaData: pageList.MetaData);
        }

        /// <summary>
        /// làm thêm phân trang và filter ở đây
        /// </summary>
        /// <returns></returns>
        public async Task<Responses<CourseDTO>> GetAll()
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .IncludeCategory()
                                                     .IncludeUser()
                                                     .ToListAsync(c => _mapper.Map<CourseDTO>(c));

                return new Responses<CourseDTO>(true, courses);
            }
            catch (Exception ex)
            {
                return new Responses<CourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<CourseDTO>> GetAllMyCoures(Guid userId)
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .IncludeCategory()
                                                     .IncludeUser()
                                                     .FilterByUserId(userId)
                                                     .ToListAsync(c => _mapper.Map<CourseDTO>(c));

                return new Responses<CourseDTO>(true, courses);
            }
            catch (Exception ex)
            {
                return new Responses<CourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<CourseDTO>> GetAllMyPurchase(Guid userId)
        {
            try
            {
                var myPurchase = await _cousesRepository.BuildQuery()
                                                        .IncludeCategory()
                                                        .IncludeUser()
                                                        .FilterByOrderd(userId)
                                                        .ToListAsync(c => _mapper.Map<CourseDTO>(c));

                return new Responses<CourseDTO>(true, myPurchase);
            }
            catch (Exception ex)
            {
                return new Responses<CourseDTO>(false, ex.Message, null);
            }
        }


        public async Task<Response<CourseDTO>> Get(Guid id)
        {
            try
            {
                var course = await _cousesRepository.BuildQuery()
                                                    .IncludeCategory()
                                                    .IncludeLanguage()
                                                    .IncludeLevel()
                                                    .IncludeSection()
                                                    .IncludeUser()
                                                    .FilterById(id)
                                                    .AsSelectorAsync(x => _mapper.Map<CourseDTO>(x));

                var courseResponse = _mapper.Map<CourseDTO>(course);
                return new Response<CourseDTO>(true, courseResponse);
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseDTO>> Add(Guid userId, CourseForCreateRequest courseRequest)
        {
            try
            {
                var course = _mapper.Map<Courses>(courseRequest);
                course.UserId = userId;
                course.CreatedAt = DateTime.Now;

                await _cousesRepository.CreateAsync(course);

                #region add sub table
                List<AudioLanguage> audioLanguages = new();
                if (courseRequest.AudioLanguageIds != null)
                {
                    foreach (var id in courseRequest.AudioLanguageIds)
                    {
                        var audioLanguage = await _audioLanguageRepository.GetByIdAsync(id);
                        audioLanguages.Add(audioLanguage);
                    }
                    course.AudioLanguages = audioLanguages;
                }

                //course.AudioLanguages.Clear();

                List<CloseCaption> closeCaptions = new();
                if (courseRequest.CloseCaptionIds != null)
                {
                    foreach (var id in courseRequest.CloseCaptionIds)
                    {
                        var closeCaption = await _closeCaptionRepository.GetByIdAsync(id);
                        closeCaptions.Add(closeCaption);
                    }
                    //course.CloseCaptions.Clear();
                    course.CloseCaptions = closeCaptions;
                }

                List<Level> levels = new();
                if (courseRequest.LevelIds != null)
                {
                    foreach (var id in courseRequest.LevelIds)
                    {
                        var level = await _levelRepository.GetByIdAsync(id);
                        levels.Add(level);
                    }
                    //course.Levels.Clear();
                    course.Levels = levels;
                }
                #endregion

                var result = await _unitOfWork.SaveChangesAsync();

                var CourseDTO = _mapper.Map<CourseDTO>(course);
                return new Response<CourseDTO>(
                    true,
                    CourseDTO
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }


        public async Task<BaseResponse> Remove(Guid idCourse)
        {
            try
            {
                var course = await _cousesRepository.GetByIdAsync(idCourse);
                if (course == null)
                {
                    new Responses<BaseResponse>(false, "can't find course", null);
                }

                _cousesRepository.Remove(course);
                await _unitOfWork.SaveChangesAsync();

                return new BaseResponse(true);

            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CourseDTO>> Update(Guid id, CourseForUpdateRequest courseRequest)
        {
            try
            {
                var course = await _cousesRepository.BuildQuery()
                                                    .FilterById(id)
                                                    .IncludeCategory()
                                                    .IncludeLevel()
                                                    .IncludeLanguage()
                                                    //.IncludeSection()
                                                    .AsSelectorAsync(c => c);


                //var sections = courseRequest.Sections;
                //for (var i = 0; i < sections.Count; i++)
                //{
                //    var section = sections[i];
                //    if (section.Id != null)
                //}


                if (course == null)
                {
                    return new Response<CourseDTO>(false, "can't find course", null);
                }
                _mapper.Map(courseRequest, course);

                course.UpdatedAt = DateTime.Now;
                course.UpdatedBy = id;

                #region
                List<AudioLanguage> audioLanguages = new();
                if (courseRequest.AudioLanguageIds != null)
                {
                    foreach (var idcourse in courseRequest.AudioLanguageIds)
                    {
                        var audioLanguage = await _audioLanguageRepository.GetByIdAsync(idcourse);
                        audioLanguages.Add(audioLanguage);
                    }
                    //course.AudioLanguages.Clear();

                    course.AudioLanguages = audioLanguages;
                }

                //course.AudioLanguages.Clear();

                List<CloseCaption> closeCaptions = new();
                if (courseRequest.CloseCaptionIds != null)
                {
                    foreach (var idcourse in courseRequest.CloseCaptionIds)
                    {
                        var closeCaption = await _closeCaptionRepository.GetByIdAsync(idcourse);
                        closeCaptions.Add(closeCaption);
                    }
                    //course.CloseCaptions.Clear();
                    course.CloseCaptions = closeCaptions;
                }

                List<Level> levels = new();
                if (courseRequest.LevelIds != null)
                {
                    foreach (var idcourse in courseRequest.LevelIds)
                    {
                        var level = await _levelRepository.GetByIdAsync(idcourse);
                        levels.Add(level);
                    }
                    //course.Levels.Clear();
                    course.Levels = levels;
                }
                #endregion

                _cousesRepository.Update(course);
                var CourseResponse = _mapper.Map<CourseDTO>(course);

                await _unitOfWork.SaveChangesAsync();


                return new Response<CourseDTO>(
                    true,
                    CourseResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<CourseDTO>(false, ex.Message, null);
            }
        }
        public async Task<Response<int>> GetTotal(Guid userId)
        {
            try
            {
                var courses = await _cousesRepository.BuildQuery()
                                                     .FilterByUserId(userId)
                                                     .CountAsync();
                return new Response<int>(true, courses);
            }
            catch (Exception ex)
            {
                return new Response<int>(false, ex.Message, null);
            }
        }
        //public async Task<BaseResponse> IsFree(Guid courseId)
        //{
        //    try
        //    {
        //        bool result = await _cousesRepository.IsFree(courseId);
        //        return new BaseResponse(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse(false, ex.Message, null);
        //    }
        //}
        //public async Task<Response<CourseForDetailDTO>> GetDetail(Guid id)
        //{
        //    try
        //    {
        //        var course = await _cousesRepository.BuildQuery()
        //                                            .IncludeCategory()
        //                                            .IncludeLanguage()
        //                                            .IncludeSection()
        //                                            .IncludeUser()
        //                                            .FilterById(id)
        //                                            .AsSelectorAsync(c => _mapper.Map<CourseForDetailDTO>(c));

        //        return new Response<CourseForDetailDTO>(true, course);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<CourseForDetailDTO>(false, ex.Message, null);
        //    }
        //}

        //public async Task<Responses<UpcommingCourseDTO>> GetAllUpcomingCourses(Guid userId)
        //{
        //    try
        //    {
        //        var myCourse = await _cousesRepository.GetAllUpcomingCourses(userId);
        //        return new Responses<UpcommingCourseDTO>(true, myCourse);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<UpcommingCourseDTO>(false, ex.Message, null);
        //    }
        //}


        //public async Task<Responses<CousrsePagingDTO>> GetCoursePaing(CousrsePagingRequest cousrsePagingRequest)
        //{
        //    try
        //    {
        //        var cousrsePaging = await _cousesRepository.GetPagingCourses(cousrsePagingRequest.page, cousrsePagingRequest.pageSize, cousrsePagingRequest.sortBy);
        //        return new Responses<CousrsePagingDTO>(true, cousrsePaging);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<CousrsePagingDTO>(false, ex.Message, null);
        //    }
        //}
        //public async Task<Responses<CousrsePagingDTO>> GetByFilteringCousrse(string key)
        //{
        //    try
        //    {
        //        var cousrsePaging = await _cousesRepository.GetByFilteringCousrse(key);
        //        return new Responses<CousrsePagingDTO>(true, cousrsePaging);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Responses<CousrsePagingDTO>(false, ex.Message, null);
        //    }
        //}
    }
}
