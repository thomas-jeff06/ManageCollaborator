using ManageCollaborator.Context;
using ManageCollaborator.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

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
                dbContext.Company.Add(newCompany);

                dbContext.SaveChanges();

                dbContext.Dispose();
            }
        }

        public Boolean UpdateCompany(string corporateName, string fantasyName, int companyId)
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                Company company = dbContext.Company.FirstOrDefault(e => e.CompanyId == companyId);

                if (company != null)
                {
                    company.CorporateName = corporateName;
                    company.FantasyName = fantasyName;
                    company.UpdateDate = DateTime.Now;

                    dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public void UpdateCompanyStatus(int companyId)
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                Company company = dbContext.Company.FirstOrDefault(e => e.CompanyId == companyId);

                if(company.DeleteDate != null)
                {
                    company.DeleteDate = null;

                    dbContext.SaveChanges();
                }
                else
                {
                    company.DeleteDate = DateTime.Now;
                    dbContext.SaveChanges();
                }
            }
        }
        public void DeleteCompany(int companyId) 
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                Company company = GetCompanyById(companyId);

                dbContext.Company.Remove(company);
            }
        }
        public List<Company> GetCompanies()
        {
            using (var dbContext = new ApplicationDbContext(options))
            {
                return dbContext.Company.ToList();
            }
        }

        public List<Company> GetCompaniesByFilter(string filter)
        {
            List<Company> companies = new List<Company>();

            using (var dbContext = new ApplicationDbContext(options))
            {
                companies = dbContext.Company.Where(e => e.CNPJ.Contains(filter) || e.CorporateName.ToLower().Contains(filter) || e.FantasyName.ToLower().Contains(filter)).ToList();
            }
            return companies;
        }

        public Company GetCompanyById(int companyId)
        {
            Company company = new Company();

            using (var dbContext = new ApplicationDbContext(options))
            {
                company = dbContext.Company.Find(companyId);
            }

            return company;
        }
    }
}
