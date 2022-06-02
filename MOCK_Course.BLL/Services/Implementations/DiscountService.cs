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
using Course.BLL.Responses;

namespace Course.BLL.Services.Implementations
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository discountRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Responses<DiscountDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        /*public async Task<Responses<CategoryDTO>> GetAll()
{
   try
   {
       var result = await _categoryRepository.GetAll().Where(c => c.ParentId == null).Include("SubCategories").ToListAsync();

       return new Responses<CategoryDTO>(true, _mapper.Map<IEnumerable<CategoryDTO>>(result));
   }
   catch (Exception ex)
   {
       return new Responses<CategoryDTO>(false, ex.Message, null);
   }
}*/


    }
}
