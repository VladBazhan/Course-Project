using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectCinema.Models;

namespace ProjectCinema.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<ModelHelp> Articles { get; set; }
    }
}
