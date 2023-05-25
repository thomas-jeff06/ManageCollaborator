using ManageCollaborator.Models;
using ManageCollaborator.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace ManageCollaborator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageCompanyHome()
        {
            CompanyRepository companyRepository = new CompanyRepository();

            List<Company> companies = companyRepository.GetCompanies();  // Recupera a lista de empresas do banco de dados

            return View("~/Views/Company/ManageCompanyHome.cshtml", companies);

        }

        public IActionResult InsertCompanyPage()
        {
            return View("~/Views/Company/InsertCompany.cshtml");
        }

        public IActionResult InsertCompany(Company company)
        {
            CompanyRepository companyRepository = new CompanyRepository();

            //Verificar cpnj valido

            companyRepository.InsertCompany(company);

            return View("~/Views/Alert/Alert.cshtml", "Uma nova empresa foi Adiconada com sucesso!");
        }

        public IActionResult GetCollaborators(int id)
        {
            CollaboratorRepository companyRepository = new CollaboratorRepository();

            List<Collaborator> collaborators = companyRepository.GetCollaboratorsByCompanyId(id);

            return View("~/Views/Collaborator/ManageCollaboratorHome.cshtml", collaborators);
        }

        public IActionResult FilterCompany(string input)
        {
            if (input == null)
            {
                return RedirectToAction("ManageCompanyHome");

            }
            
            List<Company> companies = GetCompanyByFilter(input);

            return View("~/Views/Company/ManageCompanyHome.cshtml", companies);
        }

        private List<Company> GetCompanyByFilter (string input)
        {
            CompanyRepository companyRepository = new CompanyRepository();

            List<Company> companies = companyRepository.GetCompaniesByFilter(input);

            return companies;
        }

        public IActionResult UpdateCompanyPage(int companyId)
        {
            CompanyRepository companyRepository = new CompanyRepository();

            Company company = companyRepository.GetCompanyById(companyId);

            if(company != null)
            {
                return View("~/Views/Company/CompanyUpdate.cshtml", company);
            }

            return View("Error");
        }

        public IActionResult UpdateCompany(string fantasyName, string corporateName, int companyId)
        {
            string message;
            try
            {
                CompanyRepository companyRepository = new CompanyRepository();

                if(companyRepository.UpdateCompany(corporateName, fantasyName, companyId))
                {

                     message = "A empresa foi excluída com sucesso!";
                }
                else
                {
                     message = "A empresa foi excluída com sucesso!";
                }

                return View("~/Views/Alert/Alert.cshtml", message);

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        public IActionResult DeleteCompany(int companyId)
        {
            try
            {
                CompanyRepository companyRepository = new CompanyRepository();

                companyRepository.DeleteCompany(companyId);

                TempData["Mensagem"] = "Empresa foi Deletada com sucesso!";

                return RedirectToAction("ManageCompanyHome");

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

        public IActionResult ChanceStatus(int companyId)
        {
            CompanyRepository companyRepository = new CompanyRepository();
            companyRepository.UpdateCompanyStatus(companyId);

            TempData["Mensagem"] = "Status da empresa foi Atualizado com sucesso!";

            return RedirectToAction("ManageCompanyHome");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}