using ManageCollaborator.Models;
using ManageCollaborator.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
            Company company = new Company();
            return View("~/Views/Company/InsertCompany.cshtml", company);
        }

        public IActionResult InsertCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                CompanyRepository companyRepository = new CompanyRepository();

                //Verificar cpnj valido
                company.CreationDate = DateTime.Now;
                company.UpdateDate = DateTime.Now;
                companyRepository.InsertCompany(company);

                return View("~/Views/Alert/Alert.cshtml", "Uma nova empresa foi Adiconada com sucesso!");
            }

            return RedirectToAction("InsertCompanyPage");
        }

        public IActionResult GetCollaborators(int companyId)
        {
            CollaboratorRepository collaboratorRepository = new CollaboratorRepository();
            CompanyRepository companyRepository = new CompanyRepository();
            PositionRepository positionRepository = new PositionRepository();

            ViewModel viewModel = new ViewModel();

            viewModel.Collaborators = collaboratorRepository.GetCollaboratorsByCompanyId(companyId);
            viewModel.Company = companyRepository.GetCompanyById(companyId);
            viewModel.Positions = positionRepository.GetPositions();
            viewModel.PositionsCollaborators = positionRepository.GetPositionsByCollaborators(viewModel.Collaborators);

            return View("~/Views/Collaborator/ManageCollaboratorHome.cshtml", viewModel);
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

                     message = "A empresa foi atualizada com sucesso!";
                }
                else
                {
                     message = "Algo deu errado no processo!";
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

                return View("~/Views/Alert/Alert.cshtml", "A empresa foi excluída com sucesso!");

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

        public IActionResult InsertCollaboratorPage(int companyId)
        {
            ViewModel viewModel = new ViewModel();
            CompanyRepository companyRepository = new CompanyRepository();
            PositionRepository positionRepository = new PositionRepository();

            viewModel.NewCollaborator = new Collaborator();
            viewModel.Company = companyRepository.GetCompanyById(companyId);
            viewModel.Positions = positionRepository.GetPositions();

            return View("~/Views/Collaborator/InsertCollaboratorHome.cshtml", viewModel);
        }

        public IActionResult InsertCollaborator(ViewModel viewModel)
        {

            string cpf = viewModel.NewCollaborator.CPF.Replace("/", "").Replace("\\", "").Replace("-", "").Replace(".", "");
            if (IsCPFExist(cpf))
            {
                CollaboratorRepository collaboratorRepository = new CollaboratorRepository();

                Collaborator newCollaborator = new Collaborator();

                newCollaborator.CompanyId = viewModel.Company.CompanyId;
                newCollaborator.PositionId = viewModel.ChoicePosition.PositionId;

                newCollaborator.Name = viewModel.NewCollaborator.Name;
                newCollaborator.CPF = cpf;
                newCollaborator.AdmissionDate = viewModel.NewCollaborator.AdmissionDate;

                newCollaborator.Matricula = GerarMatricula();

                newCollaborator.CreationDate = DateTime.Now;
                newCollaborator.UpdateDate = DateTime.Now;

                collaboratorRepository.InsertCollaborator(newCollaborator);

                return View("~/Views/Alert/Alert.cshtml", "Um novo Funcionario foi Adiconada com sucesso!");
            }

            return View("~/Views/Alert/Alert.cshtml", "Funcionario ja Adiconada!");
        }

        private string GerarMatricula()
        {
            Random random = new Random();

            string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numeros = "0123456789";

            string stringAleatoria = "";

            for (int i = 0; i < 2; i++)
            {
                int indice = random.Next(letras.Length);
                char letraAleatoria = letras[indice];
                stringAleatoria += letraAleatoria;
            }

            // Gerar os três números aleatórios
            for (int i = 0; i < 3; i++)
            {
                int indice = random.Next(numeros.Length);
                char numeroAleatorio = numeros[indice];
                stringAleatoria += numeroAleatorio;
            }

            return stringAleatoria;
        }

        private bool IsCPFExist(string cpf)
        {
            CollaboratorRepository collaboratorRepository = new CollaboratorRepository();

            Collaborator collaborator = collaboratorRepository.GetCollaboratorByCPF(cpf);

            if (collaborator == null) return true;

            return false;
        }

        public IActionResult UpdateCollaboratorPage()
        {
            return RedirectToAction("ManageCompanyHome");
        }

        public IActionResult InsertPositionPage()
        {
            return RedirectToAction("ManageCompanyHome");
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}