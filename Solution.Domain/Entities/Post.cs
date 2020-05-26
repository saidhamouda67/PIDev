using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Solution.Domain.Entities
{
    public class Post
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        
       public int ForumId { get; set; }


        [ForeignKey("ForumId")]
        public virtual Forum Forum { get; set; }


        public virtual ICollection<PostReply> Replies { get; set; }

    }
}
