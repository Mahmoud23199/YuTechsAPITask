using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuTechsBL.Repository;
using YuTechsEF.Entites;
using YuTechsEF.Entity;

namespace YuTechsBL.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Author> Authors { get; }
        IGenericRepository<News> News { get; }

        int Complete();
    }
}
