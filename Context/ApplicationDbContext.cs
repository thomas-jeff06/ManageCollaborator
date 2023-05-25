using ManageCollaborator.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageCollaborator.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Company> Company { get; set; }
        public DbSet<Collaborator> Collaborator { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<CollaboratorHistoric> CollaboratorHistoric { get; set; }
    }
}
