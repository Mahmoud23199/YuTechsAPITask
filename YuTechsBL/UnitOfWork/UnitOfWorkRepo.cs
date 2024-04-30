using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuTechsBL.GenericRepository;
using YuTechsBL.Repository;
using YuTechsEF.Context;
using YuTechsEF.Entites;
using YuTechsEF.Entity;

namespace YuTechsBL.UnitOfWork
{
    public class UnitOfWorkRepo : IUnitOfWork
    {
        private readonly YuAppDbContext _context;

        public IGenericRepository<Author> Authors { get; private set; }

        public IGenericRepository<News> News { get; private set; }

        public UnitOfWorkRepo(YuAppDbContext context)
        {
            this._context=context;
            this.Authors = new GenericRepository<Author>(context);
            this.News = new GenericRepository<News>(context);

        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
