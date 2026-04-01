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
            Debug.WriteLine("GET Login called");
            return View();
        }

        // traiter le login
        [HttpPost]
        public ActionResult Login(LoginForm form)
        {
            Debug.WriteLine("POST Login called");
            Debug.WriteLine($"Email: {form.Email}");
            Debug.WriteLine($"Password provided: {(!string.IsNullOrEmpty(form.Password))}");

            // validation du formulaire 
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("ModelState is invalid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Debug.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(form);
            }

            // verifier email et password
            Debug.WriteLine("Checking password...");
            Guid? employeeId = _userService.CheckPassword(form.Email, form.Password);
            Debug.WriteLine($"EmployeeId result: {employeeId}");

            if (employeeId == null)
            {
                Debug.WriteLine("Login failed - invalid credentials");
                ModelState.AddModelError("", "Email ou mot de passe incorrect");
                return View(form);
            }

            // récupérer les informations de l'employé
            try
            {
                Debug.WriteLine("Getting employee info...");
                Employee employee = _employeeService.GetById(employeeId.Value);
                Debug.WriteLine($"Employee found: {employee.FirstName} {employee.LastName}, IsManager: {employee.IsProjectManager}");
                // stocker l'utilisateur en session 
                _session.EmployeeId = employeeId;
                _session.FirstName = employee.FirstName;
                _session.LastName = employee.LastName;
                _session.IsManager = employee.IsProjectManager;
                Debug.WriteLine("Session data stored successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting employee info: {ex.Message}");
                ModelState.AddModelError("", "Erreur lors de la récupération des informations utilisateur");
                return View(form);
            }

            Debug.WriteLine("Redirecting to Project/Index");
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
