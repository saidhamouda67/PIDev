using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models
{
    public class CreateRoleModelView
    {
        [Required]
        public string RoleName { get; set; }
    }
}