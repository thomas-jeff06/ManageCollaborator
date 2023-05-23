using ManageCollaborator.Models;
using ManageCollaborator.Repository;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Privacy()
        {
            CompanyRepository companyRepository = new CompanyRepository();

            var newCompany = new Company
            {
                CorporateName = "Empresa1",
                CNPJ = "12.312.321-0001/00",
                FantasyName = "FantasiaNome1",
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            companyRepository.InsertCompany(newCompany);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}