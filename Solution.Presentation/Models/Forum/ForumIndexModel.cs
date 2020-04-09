using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models.Forum
{
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }

    }
}