//using Course.DAL.Data;
//using Course.DAL.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Course.DAL.Repositories.Implementations
//{
//    public class CourseLevelRepository : Repository<Level, Guid>, ICourseLevelRepository
//    {
//        private AppDbContext _context;
//        public CourseLevelRepository(AppDbContext context) : base(context)
//        {
//            _context = context;
//        }
//        //public override void Remove(Level _object)
//        //{
//        //    if (_object == null)
//        //        return;
//        //    _context.Levels.Remove(_object);
//        //}

//        //public async Task<bool> RemoveAll(Guid courseId)
//        //{
//        //    Entity().RemoveRange(GetAll().Where(a => a.CourseId == courseId));
//        //    return true;
//        //}
//    }
//}
