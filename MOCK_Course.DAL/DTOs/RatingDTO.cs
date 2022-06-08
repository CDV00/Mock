using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.DAL.DTOs
{
    public class RatingDTO
    {
        public float AvgRating { get; set; } 
    }

    public class RatingDetailDTO {
        public float OneRatingPercent { get; set; }
       public float TwoRatingPercent { get; set; }
        public float ThreeRatingPercent { get; set; }
        public float FourRatingPercent { get; set; }
        public float FiveRatingPercent { get; set; }
    }
}
