using Service.Pattern;
using Solution.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using Solution.Data.Infrastructure;

namespace Solution.Service
{
    public class UserService : Service<User>, IUserService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public UserService() : base(utk)
        {

        }

        public List<User> getUsers()
        {
            // IEnumerable<User> u = (from user in utk.GetRepositoryBase<User>().GetMany() select user);
            List<User> list = new List<User>();
            return list;
        }
       
    }
}
