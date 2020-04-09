using System;

namespace Solution.Presentation.Models.Post
{
    public class PostReplyModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public DateTime Created { get; set; }
        public string ReplyContent { get; set; }
        public int PostId { get; set; }
        public int AuthorRating { get;  set; }
    }
}