using BagAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BagAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bag> Bags { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
