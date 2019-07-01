using LibraryGet.Model.STANDARD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Data
{
    public class AppDataContext : DbContext
    {

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public virtual PagedResult<Book> GetBooksAsync(int page, int pageSize = 10)
        {
            return Books.GetPaged<Book>(page, pageSize);
        }
    }
}
