namespace Course.BLL.Requests
{
    public class UpdateProfileRequest
    {
        public string AvatarUrl { get; set; }
        public string FullName { get; set; }
        //public string LastName { get; set; }
        //public string FirstName { get; set; }
        public string ProfileLink { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedlnLink { get; set; }
        public string YoutubeLink { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }
    }
}
