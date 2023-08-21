using System;
using System.Collections.Generic;
using System.Text;

namespace CargoDataCollector.Repository
{
    interface IRepository<T>
    {
        IEnumerable<T> Read();
        IEnumerable<T> Write();
    
    }
}
