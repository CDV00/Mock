using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Course.BLL.DTO;

namespace Course.BLL.Services.Implementations
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _LanguageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LanguageService(ILanguageRepository LanguageRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _LanguageRepository = LanguageRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Responses<LanguageResponse>> GetAll()
        {
            try
            {
                var result = await _LanguageRepository.GetAll().ToListAsync();

                return new Responses<LanguageResponse>(true, _mapper.Map<IEnumerable<LanguageResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<LanguageResponse>(false, ex.Message, null);
            }
        }
    }
}
