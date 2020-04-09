using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    class PostReplyConf: EntityTypeConfiguration<PostReply>
    {
        public PostReplyConf()
        {
        }
    }
}
