using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class CousrsePagingDTO
    {
        public Guid CousrseId { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string CategoryName { get; set; }
        public string ThumbnailUrl { get; set; }
        public decimal Price { get; set; } = 0;
        public int Rating { get; set; }
        public int View { get; set; } = 0;
        public int TotalTime { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
    }
}
