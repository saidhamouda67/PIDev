using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class KindergartenService : Service<Kindergarten>, IKindergartenService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public KindergartenService() : base(utk)
        {
        }
        public void commit()
        {
            utk.commit();
        }
        public void Dispose()
        {
            utk.Dispose();
        }

       

       


        public IEnumerable<Kindergarten> SearchKindergartenByName(string searchString)
        {
            IEnumerable<Kindergarten> KindergartenDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                KindergartenDomain = GetMany(x => x.Name.Contains(searchString));
            }
           
            return KindergartenDomain;
        }
    }
}
