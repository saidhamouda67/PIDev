using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasMany(u => u.Kindergartens).WithRequired(k => k.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);
            HasMany(u =>u.Replies).WithRequired(k => k.User).HasForeignKey(e => e.UserId).WillCascadeOnDelete(true);
            HasMany(u => u.Posts).WithRequired(k => k.User).HasForeignKey(e => e.UserId).WillCascadeOnDelete(true);

        }

    }
}
