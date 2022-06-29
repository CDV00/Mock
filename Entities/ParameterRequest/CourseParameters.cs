using Course.BLL.Share.RequestFeatures;
using Course.DAL.Models;
using System;
using System.Collections.Generic;

namespace Entities.ParameterRequest
{
    public class CourseParameters : RequestParameters
    {
        public string Keyword { get; set; }
        public Guid? userId { get; set; }
        public List<Guid?> CategoryId { get; set; }
        public List<Guid?> AudioLanguageIds { get; set; }
        public List<Guid?> CloseCaptionIds { get; set; }
        public List<Guid?> LevelIds { get; set; }
        public Status? status { get; set; } = null;
        public int? Rate { get; set; }
        public string Orderby { get; set; }
        public bool IsDiscount { get; set; } = false;
        public bool IsFree { get; set; } = false;
        public bool? IsActive { get; set; } = null;
        public bool? IsOwner { get; set; } = null;
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 999999999;

        public bool? IsEnrollemt { get; set; } = null;
        public bool? IsPurchased { get; set; } = null;

        public StatusOfUser? StatusOfUser { get; set; } = null;
    }

    public enum StatusOfUser
    {
        IsEnrollemt,
        IsSaved,
        IsCart,
        IsPurchased
    }
}
