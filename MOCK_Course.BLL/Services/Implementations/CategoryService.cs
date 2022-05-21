using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.Responses;
using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.DAL.Models;
using Course.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

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
                var result = await _categoryRepository.GetAll().ToListAsync();
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
    }
}
