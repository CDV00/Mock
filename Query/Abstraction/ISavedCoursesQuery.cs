using Course.DAL.Models;
using System;

namespace Course.DAL.Queries.Abstraction
{
    public interface ISavedCoursesQuery : IQuery<SavedCourses>
    {
        ISavedCoursesQuery FilterByUserId(Guid userId);
        ISavedCoursesQuery FilterByCourseId(Guid? CourseId);
        ISavedCoursesQuery IncludeCategory();
        ISavedCoursesQuery IncludeCourse();
        ISavedCoursesQuery IncludeUser();
        ISavedCoursesQuery IncludeDiscount();
    }
}