using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models.Post
{
    public class PostIndexModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string CurrentUserId { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorRating { get; set; }
        public DateTime Created { get; set; }
        public string PostContennt { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool isCurrentUserEmailConfirmed { get; set; }
        public PostReplyModel postReplyModel { get; set; }
        public IEnumerable<PostReplyModel> Replies { get; set; }
    }
}