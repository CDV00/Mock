using Course.BLL.Requests;
using Course.BLL.Responses;
using Course.BLL.Responsesnamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services
{
    public interface ICourseReviewService
    {
        Task<Responses<CourseReviewResponse>> GetAll();
        Task<Response<CourseReviewResponse>> GetById(Guid id);
        Task<Response<CourseReviewResponse>> Add(CourseReviewRequest courseReviewRequest);
        Task<Response<CourseReviewResponse>> Update(CourseReviewUpdateRequest courseReviewUpdateRequest);
        Task<BaseResponse> Delete(Guid id);
    }
}
