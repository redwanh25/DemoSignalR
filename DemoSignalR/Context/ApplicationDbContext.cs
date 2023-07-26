using DemoSignalR.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoSignalR.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
