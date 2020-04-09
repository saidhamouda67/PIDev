
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace Solution.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public string domain { get; set; }
        public string image { get; set; }
        public string Phone2 { get; set; }

        public int Rating { get; set; }
        public bool IsActive { get; set; }

        
        public DateTime MemberSince { get; set; }
        public virtual ICollection<Kindergarten> Kindergartens { get; set; }
        public virtual ICollection<PostReply> Replies { get; set; }
        public virtual ICollection<Post> Posts { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // public virtual ICollection<TaskPM> Tasks { get; set; }


    }
}
