using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Forum
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descrition { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        
    }
}
