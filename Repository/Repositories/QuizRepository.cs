using AutoMapper;
using Course.BLL.Requests;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.Data;
using Course.DAL.DTOs;
using Course.DAL.Models;
using Course.DAL.Queries;
using Course.DAL.Queries.Abstraction;
using Course.DAL.Repositories.Abstraction;
using System;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class QuizRepository : Repository<Quiz>, IQuizRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public QuizRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQuizQuery BuildQuery()
        {
            return new QuizQuery(_context.Quizs.AsQueryable(), _context);
        }

        public async Task<PagedList<QuizDTO>> GetAllQuiz(QuizParameters parameter)
        {
            var quizzes = await BuildQuery().FilterByKeyword(parameter.Keyword)
                                            .IncludeSection()
                                            .IncludQuestion()
                                            .IncludQuizSetting()
                                            .ApplySort(parameter.Orderby)
                                            .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                                            .Take(parameter.PageSize)
                                            .ToListAsync(c => _mapper.Map<QuizDTO>(c));

            var count = await BuildQuery().FilterByKeyword(parameter.Keyword)
                                         .CountAsync();

            return new PagedList<QuizDTO>(quizzes, count, parameter.PageNumber, parameter.PageSize);
        }

    }
}
