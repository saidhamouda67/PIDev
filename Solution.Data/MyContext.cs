using Microsoft.AspNet.Identity.EntityFramework;
using Solution.Data.Configurations;
using Solution.Data.CustomConventions;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data
{
    public class MyContext :  IdentityDbContext<User>
    {
        public MyContext():base("ConsomiTounsiDataBase")
        {
            Database.SetInitializer(new ContexInit());

        }
        public static MyContext Create()
        {
            return new MyContext();
        }
        //les dbsets
        public DbSet<Event> Events { get; set; }
       
   
        public DbSet<PostReply> PostReplies { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Forum> theForums { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration());
            modelBuilder.Configurations.Add(new IdentityUserRoleConfiguration());
            modelBuilder.Entity<User>().HasMany(e => e.Posts).WithRequired(e => e.User).HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Forum>().HasMany(e => e.Posts).WithRequired(e => e.Forum).HasForeignKey(e => e.ForumId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasMany(e => e.Replies).WithRequired(e => e.Post).HasForeignKey(e => e.PostId).WillCascadeOnDelete(false);
            modelBuilder.Entity<Post>().HasRequired(e => e.Forum).WithMany(e => (ICollection<Post>)e.Posts).HasForeignKey(e => e.ForumId).WillCascadeOnDelete(false);
            modelBuilder.Entity<PostReply>().HasRequired(e => e.Post).WithMany(e => (ICollection<PostReply>)e.Replies).HasForeignKey(e => e.PostId).WillCascadeOnDelete(false);
            modelBuilder.Conventions.Add(new DateTimeTwoConvention());
        }
        public class ContexInit : DropCreateDatabaseIfModelChanges<MyContext>
        {
            protected override void Seed(MyContext context)
            {
                
            }
        }
    }
}
