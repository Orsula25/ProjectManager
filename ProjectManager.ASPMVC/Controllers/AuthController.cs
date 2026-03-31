using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Models.Auth;
using ProjectManager.BLL.Services;

namespace ProjectManager.ASPMVC.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserService _userService;

        private readonly UserSessionManager _session;

        public AuthController(UserService userService, UserSessionManager session)
        {
            _userService = userService;
            _session = session;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        // traiter le login

        [HttpPost]
        public ActionResult Login(LoginForm form)
        {
            // validatin du formualire 
            if (!ModelState.IsValid)
            {
                return View(form);

            }

                // verifier email et password

                Guid? employeeId = _userService.CheckPassword(form.Email, form.Password);

            if (employeeId == null)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect");
                return View(form);
            }

            // stocke l'utilisateur en session 

            _session.EmployeeId = employeeId;

            return RedirectToAction("Index", "Project");



            
        }

        //Logout 
        public ActionResult Logout()
        {
            _session.Clear();
            return RedirectToAction("Login");
        }

    }
}
