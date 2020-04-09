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
    public class ForumService : Service<Forum>, IForumService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        private readonly MyContext _context;
        public ForumService(MyContext context) : base(utk)
        {
            _context = context;
        }
        public ForumService():base(utk)
        {
            _context = new MyContext();
        }

        public IEnumerable<Forum> GetAll()
        {
            return _context.theForums.Include(e=>e.Posts);
        }


        public Forum GetById(int id)
        {
            var forum = _context.theForums.Where(f => f.Id == id)
            .Include(f => f.Posts.Select(p => p.User))
            .Include(f => f.Posts.Select(p => p.Replies.Select(r => r.User)))
            .FirstOrDefault();

            return forum;
                
        }
        public IEnumerable<User> GetAllActiveUsers()
        {
            throw new NotImplementedException();
        }
    }
}
