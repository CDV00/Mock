﻿using AutoMapper;
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

namespace Course.BLL.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _QuizRepository;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// contructer CourseReviewService 
        /// </summary>
        /// <param name="courseReviewRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public QuizService(IQuizRepository QuizRepository, IAttachmentRepository attachmentRepository,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _QuizRepository = QuizRepository;
            _attachmentRepository = attachmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //load attacment
        // Todo: filter and paging
        /// <summary>
        /// Get all course review
        /// </summary>
        /// <returns></returns>
        public async Task<PagedList<QuizDTO>> GetAll(Guid sectionId, QuizParameters quizParameters)
        {
            var quiz = await _QuizRepository.BuildQuery()
                                            .FilterByKeyword(quizParameters.Keyword)
                                            .FilterBySectionId(sectionId)
                                            .IncludeSection()
                                            .ApplySort(quizParameters.Orderby)
                                            .Skip((quizParameters.PageNumber - 1) * quizParameters.PageSize)
                                            .Take(quizParameters.PageSize)
                                            .ToListAsync(c => _mapper.Map<QuizDTO>(c));

            var count = await _QuizRepository.BuildQuery()
                                             .FilterByKeyword(quizParameters.Keyword)
                                             .FilterBySectionId(sectionId)
                                             .CountAsync();
            var pageList = new PagedList<QuizDTO>(quiz, count, quizParameters.PageNumber, quizParameters.PageSize);

            return pageList;
        }
        ////public async Task<Response<QuizDTO>> Add(QuizForCreateRequest QuizForCreateRequest)
        ////{
        ////    try
        ////    {
        ////        var Quiz = _mapper.Map<Quiz>(QuizForCreateRequest);
        ////        Quiz.CreatedAt = DateTime.Now;
        ////        await _QuizRepository.CreateAsync(Quiz);

        ////        await _unitOfWork.SaveChangesAsync();
        ////        return new Response<QuizDTO>(true, _mapper.Map<QuizDTO>(Quiz));
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        return new Response<QuizDTO>(false, ex.Message, null);
        ////    }
        ////}
        ///
        
        //Quiz; Get all theo sectionid 



    }
}
