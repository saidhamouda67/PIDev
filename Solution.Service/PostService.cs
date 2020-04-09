using Service.Pattern;
using Solution.Data;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class PostService : Service<Post>, IPostService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        private readonly MyContext _context;

        public PostService() : base(utk)
         {
            _context = new MyContext();
        }

        public Task AddReply(PostReply reply)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.ToList();
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            throw new NotImplementedException();
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies.Select(reply => reply.User))
                .Include(post => post.Forum)
                .First();
        }
        public IEnumerable<Post> GetPostsByForum(Forum f)
        {
            var posts = f.Posts;
             
            //ICollection<Post> requestedPosts = new List<Post>();
            //foreach(var post in posts)
            //{
            //    if (post.Forum.Id == id)
            //    {
            //        Console.WriteLine(post.Forum.Id);
            //        requestedPosts.Add(post);
            //    }
            //}

            return posts;
                
        }

       public async  Task Adding(Post post)
        {
     
            await _context.SaveChangesAsync();
        }
    }
}
