//using Course.DAL.Data;
//using Course.DAL.Models;
//using Course.DAL.Queries;
//using Course.DAL.Queries.Abstraction;
//using Course.DAL.Repositories.Abstraction;
//using System;

//namespace Repository.Repositories
//{
//    public class QuizSettingRepository : Repository<QuizSetting>, IQuizSettingRepository
//    {
//        private AppDbContext _context;
//        public QuizSettingRepository(AppDbContext context) : base(context)
//        {
//            _context = context;
//        }

//        public IQuizSettingQuery BuildQuery()
//        {
//            return new QuizSettingQuery(_context.QuizSettings.AsQueryable(), _context);
//        }

//    }
//}
