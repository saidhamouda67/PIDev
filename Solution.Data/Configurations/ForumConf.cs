using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    class ForumConf: EntityTypeConfiguration<Forum>
    
    {
        public ForumConf()
        {
            HasMany(u =>(ICollection<Post>) u.Posts).WithRequired(k => k.Forum).HasForeignKey(e=>e.ForumId).WillCascadeOnDelete(true);
        }
    }
}
