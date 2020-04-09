using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models.Post
{
    public class NewPostModel
    {
      public   string ForumName { get; set; }
        public int  ForumId { get; set; }
        public string AuthorName { get; set; }
        public string ForumImageUrl { get; set; }
        public string PostImageUrl { get; set; }
        public string Title { get;  set; }
        public string Content { get;  set; }
    }
}