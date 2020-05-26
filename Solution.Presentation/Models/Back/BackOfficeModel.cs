using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models.Back
{
    public class BackOfficeModel
    {
        public IEnumerable<User> UserList { get; set; }
        public IEnumerable<IdentityRole> RoleList { get; set; }
        public IdentityRole Role { get; set; }
        public string UserId { get; set; }

        public string UserName { get; set; }
        public string image { get; set; }
    }
}