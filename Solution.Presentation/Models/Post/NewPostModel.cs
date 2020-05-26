using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Solution.Domain.Entities;

namespace Solution.Presentation.Models.Post
{
    public class NewPostModel
    {
      public   string ForumName { get; set; }
        public int  ForumId { get; set; }
        public string AuthorName { get; set; }
        public string ForumImageUrl { get; set; }
        public int PostId { get; set; }

        [DisplayName("Upload Image")]
        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public string Title { get;  set; }
        public string Content { get;  set; }

        public DateTime Created { get; set; }

        public string CurrentImageUrl { get; set; }
    
    }
}