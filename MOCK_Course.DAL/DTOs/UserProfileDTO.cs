namespace Course.BLL.DTO
{
    public class UserProfileDTO
    {
        public string Fullname { get; set; }
        public string ProfileLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedlnLink { get; set; }
        public string YoutubeLink { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }

        public int TotalEnrollment { get; set; }
        public int TotalCourse { get; set; }
        public int TotalReviewCourse { get; set; }
        public int TotalSubscription { get; set; }

    }
}
