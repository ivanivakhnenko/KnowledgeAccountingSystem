using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Services;
using Microsoft.AspNet.Identity.Owin;

namespace KnowledgeAccountingSystem.WEB.Controllers
{
    public class HomeController : Controller
    {
        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        private readonly ITeamUpService _teamUpService;

        public HomeController(ITeamUpService teamUp)
        {
            _teamUpService = teamUp;
        }

        public async Task<ActionResult> Index()
        {
            await SetInitialDataAsync();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private async Task SetInitialDataAsync()
        {
            _teamUpService.GetTeams();

            await UserService.SetInitialData(new UserIdentityDTO
            {
                Email = "ad@min.ua",
                UserName = "Admin",
                Password = "KAS1337",
                Name = "Admin",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = new RoleDTO { Name = "admin" }
            }, new List<string> { "user", "manager", "admin" });
        }
    }
}