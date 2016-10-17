using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.WEB.Controllers.Mappers;
using KnowledgeAccountingSystem.WEB.Models.ViewModels;

namespace KnowledgeAccountingSystem.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class EditSkillsController : Controller
    {
        private readonly ISkillsService _skillsService;

        public EditSkillsController(ISkillsService service)
        {
            _skillsService = service;
        }

        // GET: EditSkills
        public ActionResult Index()
        {
            List<CategoryViewModel> skills = Mapper.MapUnvalued(_skillsService.GetSkills());
            return View(skills);
        }

        [HttpPost]
        public JsonResult GetSkills()
        {
            var skills = _skillsService.GetSkills();
            return Json(skills);
        }

        [HttpPost]
        public JsonResult AddSkill(SkillViewModel skill)
        {
            _skillsService.AddSkill(Mapper.Map(skill));
            return Json("OK");
        }

        [HttpPost]
        public JsonResult RenameSkill(SkillViewModel skill)
        {
            try
            {
                _skillsService.RenameSkill(Mapper.Map(skill));
            }
            catch (UnexpectedException e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(e.Message);
            }

            return Json("OK");
        }

        [HttpPost]
        public JsonResult RenameCategory(CategoryViewModel category)
        {
            try
            {
                _skillsService.RenameCategory(Mapper.Map(category));
            }
            catch (UnexpectedException e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(e.Message);
            }

            return Json("OK");
        }

        [HttpPost]
        public JsonResult RemoveCategory(CategoryViewModel category)
        {
            _skillsService.RemoveCategory(Mapper.Map(category));
            return Json("OK");
        }

        [HttpPost]
        public JsonResult RemoveSkill(SkillViewModel skill)
        {
            _skillsService.RemoveSkill(Mapper.Map(skill));
            return Json("OK");
        }
    }
}