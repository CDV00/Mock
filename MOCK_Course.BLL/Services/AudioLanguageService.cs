﻿using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.DTO;
using Course.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;

namespace Course.BLL.Services
{
    public class AudioLanguageService : IAudioLanguageService
    {
        private readonly IAudioLanguageRepository _AudioLanguageRepositoty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AudioLanguageService(IAudioLanguageRepository AudioLanguageRepositoty,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _AudioLanguageRepositoty = AudioLanguageRepositoty;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<AudioLanguageDTO>> Add(AudioLanguageForCreateRequest AudioLanguageRequest, Guid courseId)
        {
            try
            {
                var AudioLanguage = _mapper.Map<AudioLanguage>(AudioLanguageRequest);
                //AudioLanguage.CourseId = courseId;

                await _AudioLanguageRepositoty.CreateAsync(AudioLanguage);
                await _unitOfWork.SaveChangesAsync();

                return new Response<AudioLanguageDTO>(
                    true,
                    _mapper.Map<AudioLanguageDTO>(AudioLanguage)
                );
            }
            catch (Exception ex)
            {
                return new Response<AudioLanguageDTO>(false, ex.Message, null);
            }
        }

        public async Task<Responses<AudioLanguageDTO>> GetAll()
        {
            try
            {

                var audioLaguanges = await _AudioLanguageRepositoty.GetAll().ToListAsync();

                return new Responses<AudioLanguageDTO>(
                    true,
                    _mapper.Map<IList<AudioLanguageDTO>>(audioLaguanges)
                );
            }
            catch (Exception ex)
            {
                return new Responses<AudioLanguageDTO>(false, ex.Message, null);
            }
        }
    }
}
