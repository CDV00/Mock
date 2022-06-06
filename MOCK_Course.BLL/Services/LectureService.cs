﻿using AutoMapper;
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
    public class LectureService : ILectureService
    {
        private readonly ILectureRepository _lectureRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LectureService(ILectureRepository lectureRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _lectureRepositoty = lectureRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<LectureDTO>> Add(LectureForCreateRequest LectureRequest)
        {
            try
            {
                var Lecture = _mapper.Map<Lecture>(LectureRequest);

                await _lectureRepositoty.CreateAsync(Lecture);
                await _unitOfWork.SaveChangesAsync();
                return new Response<LectureDTO>(
                    true,
                    _mapper.Map<LectureDTO>(Lecture)
                );
            }
            catch (Exception ex)
            {
                return new Response<LectureDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<LectureDTO>> GetAll(Guid sectionId)
        {
            try
            {
                var lesson = await _lectureRepositoty.BuildQuery()
                                                     .FilterBySectionId(sectionId)
                                                     .ToListAsync(l => _mapper.Map<LectureDTO>(l));

                return new Responses<LectureDTO>(true, lesson);
            }
            catch (Exception ex)
            {
                return new Responses<LectureDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> Remove(Guid idLecture)
        {
            try
            {
                var lesson = await _lectureRepositoty.GetByIdAsync(idLecture);

                if (lesson is null)
                {
                    return new BaseResponse(false, null, "can't find lesson");
                }

                _lectureRepositoty.Remove(lesson);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                return new Responses<BaseResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<LectureDTO>> Update(Guid id, LectureForUpdateRequest LectureRequest)
        {
            try
            {
                var lesson = _lectureRepositoty.GetByIdAsync(id);

                if (lesson is null)
                {
                    return new Response<LectureDTO>(false, null, "can't find lesson");
                }
                await _mapper.Map(LectureRequest, lesson);
                await _unitOfWork.SaveChangesAsync();

                var lessonResponse = _mapper.Map<LectureDTO>(LectureRequest);
                return new Response<LectureDTO>(
                    true,
                   lessonResponse
                );
            }
            catch (Exception ex)
            {
                return new Response<LectureDTO>(false, ex.Message, null);
            }
        }
    }
}