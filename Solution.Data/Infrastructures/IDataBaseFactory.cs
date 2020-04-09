using Solution.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Infrastructure
{
    public interface IDataBaseFactory:IDisposable
    {
         MyContext DataContext { get;}
        //void Dispose(); hidden
    }
}
