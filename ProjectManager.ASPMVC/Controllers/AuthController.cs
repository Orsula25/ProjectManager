using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Models.Auth;
using ProjectManager.BLL.Services;
using ProjectManager.BLL.Entities;
using ProjectManager.Common.Repositories;
using System.Diagnostics;

namespace ProjectManager.ASPMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly UserSessionManager _session;
        private readonly IEmployeeRepository<Employee> _employeeService;

        public AuthController(UserService userService, UserSessionManager session, IEmployeeRepository<Employee> employeeService)
        {
            _userService = userService;
            _session = session;
            _employeeService = employeeService;
        }

        [HttpGet]

        public IActionResult Login()
        {
            
            return View();
        }

        // traiter le login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginForm form)
        {
            

            // validation du formulaire 
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

            // récupérer les informations de l'employé
            try
            {
             
                Employee employee = _employeeService.GetById(employeeId.Value);
                
                // stocker l'utilisateur en session 
                _session.EmployeeId = employeeId;
                _session.FirstName = employee.FirstName;
                _session.LastName = employee.LastName;
                _session.IsManager = employee.IsProjectManager;
             
            }
            catch (Exception ex)
            {
              
                ModelState.AddModelError("", "Erreur lors de la récupération des informations utilisateur");
                return View(form);
            }

            
            return RedirectToAction("Index", "Project");
        }

        //Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            _session.Clear();
            return RedirectToAction("Login");
        }
    }
}
