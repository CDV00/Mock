using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responsesnamespace;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Course.BLL.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Responses<CategoryResponse>> GetAll()
        {
            try
            {
                var result = await _categoryRepository.GetAll().Where(c => c.ParentId == null).Include("SubCategories").ToListAsync();

                return new Responses<CategoryResponse>(true, _mapper.Map<IEnumerable<CategoryResponse>>(result));
            }
            catch (Exception ex)
            {
                return new Responses<CategoryResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CategoryResponse>> Add(CategoryRequest categoryRequest)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryRequest);

                await _categoryRepository.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CategoryResponse>(
                    true,
                    _mapper.Map<CategoryResponse>(category)
                );
            }
            catch (Exception ex)
            {
                return new Response<CategoryResponse>(false, ex.Message, null);
            }
        }

        public async Task<Responsesnamespace.BaseResponse> remove(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);

                _categoryRepository.Remove(category);
                await _unitOfWork.SaveChangesAsync();
                return new Responsesnamespace.BaseResponse(true, null, null);
            }
            catch (Exception ex)
            {
                return new Response<CategoryResponse>(false, ex.Message, null);
            }
        }

        public async Task<Response<CategoryResponse>> Update(CategoryUpdateRequest categoryUpdateRequest)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryUpdateRequest);

                _categoryRepository.Update(category);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CategoryResponse>(
                    true,
                    _mapper.Map<CategoryResponse>(category)
                );
            }
            catch (Exception ex)
            {
                return new Response<CategoryResponse>(false, ex.Message, null);
            }
        }
    }
}
