using Service.Pattern;
using Solution.Data;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
   public  class EventService  : Service<Event>, IEventService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        private readonly MyContext _context;
        public EventService(MyContext context) : base(utk)
        {
            _context = context;
        }
        public EventService() : base(utk)
        {
            _context = new MyContext();
        }

    }
}
