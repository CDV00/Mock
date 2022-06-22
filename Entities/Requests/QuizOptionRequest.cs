using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class QuizOptionCreateForRequest
    {
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; } = false;
    }
    public class QuizOptionForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; } = false;
        //public bool IsDeleted { get; set; } = false;
        //public bool IsNew { get; set; } = true;
    }

}
