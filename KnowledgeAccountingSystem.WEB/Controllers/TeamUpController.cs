using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.BLL.Util;
using KnowledgeAccountingSystem.WEB.Controllers.Mappers;
using KnowledgeAccountingSystem.WEB.Models.ViewModels;

namespace KnowledgeAccountingSystem.WEB.Controllers
{
    [Authorize(Roles = "admin,manager")]
    public class TeamUpController : Controller
    {
        private readonly ITeamUpService _teamUpService;

        public TeamUpController(ITeamUpService teamUp)
        {
            _teamUpService = teamUp;
        }

        // GET: TeamUp
        public ActionResult Index()
        {
            FilterViewModel placeholder = Mapper.MapFilter(_teamUpService.GetSkills());

            return View(placeholder);
        }

        public ActionResult Teams()
        {
            List<TeamViewModel> model = Mapper.MapLazy(_teamUpService.GetTeams());
            return View(model);
        }

        [HttpPost]
        public JsonResult GetTeams()
        {
            return Json(Mapper.MapLazy(_teamUpService.GetTeams()));
        }

        [HttpPost]
        public JsonResult GetContent(TeamViewModel t)
        {
            return Json(Mapper.Map(_teamUpService.GetUsersByTeam(Mapper.MapLazy(t))));
        }

        [HttpPost]
        public JsonResult Index(FilterViewModel filter)
        {
            try
            {
                var result = Mapper.Map(_teamUpService.GetUsers(Mapper.Map(filter)));
                //return RedirectToAction("Result", result);
                return Json(result);
            }
            catch (ValidationException e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(e.Message);
            }
        }

        [HttpPost]
        public JsonResult RenameTeam(TeamViewModel team)
        {
            try
            {
                _teamUpService.RenameTeam(Mapper.MapLazy(team));
            }
            catch (UnexpectedException e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(e.Message);
            }
            return Json("OK");
        }

        [HttpPost]
        public JsonResult RemoveTeam(TeamViewModel team)
        {
            _teamUpService.RemoveTeam(Mapper.MapLazy(team));
            return Json("OK");
        }

        [HttpPost]
        public JsonResult AddTeam(TeamViewModel team)
        {
            _teamUpService.AddTeam(Mapper.MapLazy(team));
            return Json("OK");
        }

        [HttpPost]
        public JsonResult RemoveUserTeam(UserTeamViewModel team)
        {
            _teamUpService.RemoveUserTeam(Mapper.Map(team));
            return Json("OK");
        }

        [HttpPost]
        public JsonResult AddUserTeam(UserTeamViewModel team)
        {
            _teamUpService.AddUserTeam(Mapper.Map(team));
            return Json("OK");
        }
    }
}