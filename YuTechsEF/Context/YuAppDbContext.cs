using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuTechsEF.Entites;
using YuTechsEF.Entity;

namespace YuTechsEF.Context
{
    public class YuAppDbContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<News> News { get; set; }

        public YuAppDbContext(DbContextOptions<YuAppDbContext>options):base(options)
        {
        }

    }
}
