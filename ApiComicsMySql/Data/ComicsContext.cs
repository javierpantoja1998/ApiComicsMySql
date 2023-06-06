using ApiComicsMySql.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiComicsMySql.Data
{
    public class ComicsContext : DbContext
    {
        public ComicsContext(DbContextOptions<ComicsContext> options)
            : base(options) { }
        public DbSet<Comic> Comics { get; set; }
    }
}
