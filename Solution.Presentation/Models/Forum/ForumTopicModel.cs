using Solution.Presentation.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
        
    }
}