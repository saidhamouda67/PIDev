using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Presentation.Models
{
    public class KindergartenModel
    {

        [Key]
        public int KindergartenId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public float Cost { get; set; }
        public int Phone { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MMM-yyyy}")]
        public DateTime DateCreation { get; set; }
        public string Logo { get; set; }
        public string nameDir { get; set; }

        public int NbrEmp { get; set; }
        [Display(Name = "User")]
        public string UserId { get; set; }
        
    }
}