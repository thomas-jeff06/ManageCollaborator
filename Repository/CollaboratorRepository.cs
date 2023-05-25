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
                return dbContext.Collaborator.Where(c => c.DeleteDate == null).ToList();
            }
        }

        public void InsertCollaborator(Collaborator newCollaborator)
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                dbContext.Collaborator.Add(newCollaborator);

                dbContext.SaveChanges();

                dbContext.Dispose();
            }
        }

        public Collaborator GetCollaboratorByCPF(string cpf)
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                Collaborator collaborator = new Collaborator();

                collaborator = dbContext.Collaborator.FirstOrDefault(e => e.CPF == cpf);

                return collaborator;
            }
        }
    }
}
