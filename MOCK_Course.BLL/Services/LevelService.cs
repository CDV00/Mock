using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Services.Abstraction;
using Course.DAL.DTOs;
using Course.DAL.Repositories.Abstraction;

namespace Course.BLL.Services
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LevelService(ILevelRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _levelRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Responses<LevelDTO>> GetAll()
        {
            try
            {
                var result = await _levelRepository.GetAll();

                return new Responses<LevelDTO>(true, result);
            }
            catch (Exception ex)
            {
                return new Responses<LevelDTO>(false, ex.Message, null);
            }
        }

        //public async Task<Response<LevelResponse>> Add(LevelRequest categoryRequest)
        //{
        //    try
        //    {
        //        var category = _mapper.Map<Level>(categoryRequest);

        //        await _categoryRepository.CreateAsync(category);
        //        await _unitOfWork.SaveChangesAsync();
        //        return new Response<LevelResponse>(
        //            true,
        //            _mapper.Map<LevelResponse>(category)
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<LevelResponse>(false, ex.Message, null);
        //    }
        //}

        //public async Task<BaseResponse> remove(Guid id)
        //{
        //    try
        //    {
        //        var category = await _categoryRepository.GetByIdAsync(id);
        //        // Check category null 

        //        _categoryRepository.Remove(category);
        //        await _unitOfWork.SaveChangesAsync();
        //        return new BaseResponse(true, null, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<LevelResponse>(false, ex.Message, null);
        //    }
        //}

        //public async Task<Response<LevelResponse>> Update(Guid Id, LevelUpdateRequest categoryUpdateRequest)
        //{
        //    try
        //    {
        //        var category = await _categoryRepository.GetByIdAsync(Id);
        //        // check category null

        //        _mapper.Map(categoryUpdateRequest, category);

        //        await _unitOfWork.SaveChangesAsync();
        //        return new Response<LevelResponse>(
        //            true,
        //            _mapper.Map<LevelResponse>(category)
        //        );
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<LevelResponse>(false, ex.Message, null);
        //    }
        //}
    }
}
