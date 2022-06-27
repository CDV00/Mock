using System;
using System.Threading.Tasks;
using AutoMapper;
using Course.BLL.DTO;
using Course.BLL.Requests;
using Course.DAL.Models;
using Course.DAL.Repositories.Abstraction;
using Course.BLL.Services.Abstraction;
using Entities.ParameterRequest;
using Course.BLL.Share.RequestFeatures;

namespace Course.BLL.Services
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
        public async Task<Responses<CategoryDTO_>> GetAll()
        {
            var categories = await _categoryRepository.BuildQuery()
                                                      .FilterByParent(null)
                                                      .IncludeSubCategory()
                                                      .ToListAsync(c => _mapper.Map<CategoryDTO_>(c));

            return new Responses<CategoryDTO_>(true, categories);
        }

        public async Task<Response<PagedList<CategoryDTO_>>> GetSubCategory(CategoryParameters parameters)
        {
            var categories = await _categoryRepository.BuildQuery()
                                                      .FilterBySubCategory()
                                                      .FilterTopCategory()
                                                      .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                                      .Take(parameters.PageSize)
                                                      .ToListAsync(c => _mapper.Map<CategoryDTO_>(c));

            var count = await _categoryRepository.BuildQuery()
                                                 .FilterBySubCategory()
                                                 .FilterTopCategory()
                                                 .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                                                 .Take(parameters.PageSize)
                                                 .CountAsync();

            return new Response<PagedList<CategoryDTO_>>(true, new PagedList<CategoryDTO_>(categories, count, parameters.PageNumber, parameters.PageSize));
        }

        public async Task<Response<CategoryDTO_>> Add(CategoryRequest categoryRequest)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryRequest);

                await _categoryRepository.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return new Response<CategoryDTO_>(
                    true,
                    _mapper.Map<CategoryDTO_>(category)
                );
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO_>(false, ex.Message, null);
            }
        }

        public async Task<BaseResponse> remove(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                // Check category null 

                _categoryRepository.Remove(category, false);
                await _unitOfWork.SaveChangesAsync();
                return new BaseResponse(true, null, null);
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO_>(false, ex.Message, null);
            }
        }

        public async Task<Response<CategoryDTO_>> Update(Guid Id, CategoryUpdateRequest categoryUpdateRequest)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(Id);
                // check category null

                _mapper.Map(categoryUpdateRequest, category);

                await _unitOfWork.SaveChangesAsync();
                return new Response<CategoryDTO_>(
                    true,
                    _mapper.Map<CategoryDTO_>(category)
                );
            }
            catch (Exception ex)
            {
                return new Response<CategoryDTO_>(false, ex.Message, null);
            }
        }
    }
}
