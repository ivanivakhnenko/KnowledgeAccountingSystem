using System.Web.Mvc;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.BLL.Util;
using KnowledgeAccountingSystem.WEB.Controllers.Mappers;
using KnowledgeAccountingSystem.WEB.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace KnowledgeAccountingSystem.WEB.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IEvaluateService _evaluateService;

        public UserController(IEvaluateService evaluateService)
        {
            this._evaluateService = evaluateService;
        }

        [HttpGet]
        public ActionResult Edit()
        {
            try
            {
                string id = User.Identity.GetUserId();
                var user = _evaluateService.GetCategorizedUser(id);
                return View(Mapper.Map(user));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Edit(CategorizedUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _evaluateService.SetUserSkills(Mapper.Map(user));
            }

            try
            {
                var u = _evaluateService.GetCategorizedUser(user.Id);
                return Json(Mapper.Map(u));
            }
            catch (ValidationException)
            {
                return null;
            }
        }
    }
}