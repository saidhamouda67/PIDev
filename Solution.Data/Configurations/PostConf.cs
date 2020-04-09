using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    class PostConf: EntityTypeConfiguration<Post>
    {
        public PostConf()
        {
            //HasMany(u => (ICollection<PostReply>)u.Replies).WithRequired(k => k.Post).HasForeignKey(e => e.PostId).WillCascadeOnDelete(true);

        }
    }
}
