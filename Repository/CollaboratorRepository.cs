using ManageCollaborator.Context;
using ManageCollaborator.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageCollaborator.Repository
{
    public class CollaboratorRepository
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlite("Data Source=bdsqliteproject.db")
        .Options;

        public List<Collaborator> GetCollaboratorsByCompanyId (int companyId)
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                return dbContext.Collaborator.ToList();
            }
        }
    }
}
