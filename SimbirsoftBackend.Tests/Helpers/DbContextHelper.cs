using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbirsoftBackend.Tests.Helpers
{
    public class DbContextHelper
    {
        public DbContextHelper()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("Testings");
            Context = new AppDbContext(builder.Options);
            Context.Authors.Add(new Author());
            Context.SaveChanges();
        }

        public AppDbContext Context { get; set; }
    }
}
