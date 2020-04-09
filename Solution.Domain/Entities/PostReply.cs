using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class PostReply
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
      
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }


    }
}
