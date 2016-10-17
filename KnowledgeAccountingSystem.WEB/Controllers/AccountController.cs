using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KnowledgeAccountingSystem.BLL.DTO;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Services;
using KnowledgeAccountingSystem.BLL.Util;
using KnowledgeAccountingSystem.WEB.Controllers.Mappers;
using KnowledgeAccountingSystem.WEB.Models;
using KnowledgeAccountingSystem.WEB.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace KnowledgeAccountingSystem.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEvaluateService _evaluateService;

        private IUserService UserService => HttpContext.GetOwinContext().GetUserManager<IUserService>();

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IEvaluateService service)
        {
            _evaluateService = service;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdentityDTO userDto = new UserIdentityDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserIdentityDTO userDto = new UserIdentityDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = new RoleDTO { Name = "user" }
                };                      //TODO MAPPER HERE
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                {
                    ClaimsIdentity claim = await UserService.Authenticate(userDto);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    _evaluateService.AddUser(claim.GetUserId(), userDto.Name, userDto.Email);
                    return View("SuccessRegister");
                }

                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ManageUsers()
        {
            return View(Mapper.Map(UserService.GetAll()));
        }

        [Authorize(Roles = "admin")]
        public JsonResult GetUsers()
        {
            return Json(Mapper.Map(UserService.GetAll()));
        }

        [HttpPost]
        public JsonResult GetRoles()
        {
            return Json(Mapper.Map(UserService.GetRoles()));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> Assign(UserIdViewModel user)
        {
            var result = await UserService.Assign(Mapper.Map(user));
            return Json(Mapper.Map(result));
        }
    }
}