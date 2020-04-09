using Service.Pattern;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public interface IPostService: IService<Post>
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        Task AddReply(PostReply reply);

        Task Adding(Post post);
        IEnumerable<Post> GetPostsByForum(Forum id);
    }
}
