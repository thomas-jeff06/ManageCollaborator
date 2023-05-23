using ManageCollaborator.Context;
using ManageCollaborator.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageCollaborator.Repository
{
    public class CompanyRepository
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlite("Data Source=bdsqliteproject.db")
        .Options;
        public void InsertCompany(Company newCompany)
        {
            using (var dbContext = new ApplicationDbContext(options))
            {

                // Adicionar a nova entidade ao contexto
                dbContext.Company.Add(newCompany);

                // Salvar as alterações no banco de dados
                dbContext.SaveChanges();

                var companies = dbContext.Company.ToList();

                // Exibir as empresas recuperadas
                foreach (var company in companies)
                {
                    Console.WriteLine($"ID: {company.CompanyId}, Nome: {company.CorporateName}, Descrição: {company.CNPJ}");
                }

                dbContext.Dispose();
            }
        }
    }
}
