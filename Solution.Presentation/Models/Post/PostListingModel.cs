using Solution.Presentation.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models.Post
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }

        public string AuthorId { get; set; }
        public int AuthorRating  { get; set; }
        public string DatePosted { get; set; }
        public int  ForumId { get; set; }

        public string ImageUrl { get; set; }
        public string ForumImageUrl { get; set; }

        public string ForumName { get; set; }

        public int RepliesCount { get; set; }

        public ForumListingModel Forum { get; set; }
    
    
    }
}