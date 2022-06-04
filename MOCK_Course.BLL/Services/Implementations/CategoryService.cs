using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
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
        public async Task<Responses<CategoryDTO>> GetAll()
        {
            try
            {
                var categories = await _categoryRepository.BuildQuery()
                                                      .FilterByParent(null)
                                                      .IncludeSubCategory()
                                                      .ToListAsync(c => _mapper.Map<CategoryDTO>(c));

                return new Responses<CategoryDTO>(true, categories);
            }
            catch (Exception ex)
            {
                return new Responses<CategoryDTO>(false, ex.Message, null);
            }
        }

        public async Task<Response<CategoryDTO>> Add(CategoryRequest categoryRequest)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryRequest);

                await _categoryRepository.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CategoryDTO>(
                    true,
                    _mapper.Map<CategoryDTO>(category)
                );
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> remove(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                // Check category null 

                _categoryRepository.Remove(category);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, null, null);
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>(false, ex.Message, null);
            }
        }

        public async Task<Response<CategoryDTO>> Update(Guid Id, CategoryUpdateRequest categoryUpdateRequest)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(Id);
                // check category null

                _mapper.Map(categoryUpdateRequest, category);

                await _unitOfWork.SaveChangesAsync();
                return new Response<CategoryDTO>(
                    true,
                    _mapper.Map<CategoryDTO>(category)
                );
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO>(false, ex.Message, null);
            }
        }
    }
}
