using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Requests
{
    public class QuestionRequest
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public byte Score { get; set; }
        public string Type { get; set; }
        public IList<QuizForCreateRequest> Options { get; set; }

    }
    public class QuestionForCreateRequest
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public byte Score { get; set; }
        public string Type { get; set; }
        public IList<QuizOptionCreateForRequest> Options { get; set; }
        public string TextAnswer { get; set; }

    }
    public class QuestionForUpdateRequest
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public byte Score { get; set; }
        public string Type { get; set; }
        public IList<QuizOptionForUpdateRequest> Options { get; set; }
        public string TextAnswer { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNew { get; set; }

    }

}
