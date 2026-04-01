using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.ASPMVC.Models.Project;
using ProjectManager.BLL.Entities;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;

namespace ProjectManager.ASPMVC.Controllers
{
    [AuthFilter]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository<ProjectManager.BLL.Entities.Project> _projectService;
        private readonly IEmployeeRepository<ProjectManager.BLL.Entities.Employee> _employeeService;
        private readonly IPostRepository<ProjectManager.BLL.Entities.Post> _postService;
        private readonly UserSessionManager _session;

        public ProjectController(
            IProjectRepository<ProjectManager.BLL.Entities.Project> projectService,
            IEmployeeRepository<ProjectManager.BLL.Entities.Employee> employeeService,
            IPostRepository<ProjectManager.BLL.Entities.Post> postService,
            UserSessionManager session)
        {
            _projectService = projectService;
            _employeeService = employeeService;
            _postService = postService;
            _session = session;
        }

        // LISTE DES PROJETS
        public IActionResult Index()
        {
            Guid employeeId = _session.EmployeeId!.Value;
            IEnumerable<Project> projects = _projectService.GetFromEmployeeId(employeeId);

            ViewData["EmployeeName"] = $"{_session.FirstName} {_session.LastName}";
            ViewData["IsManager"] = _session.IsManager;
            ViewData["UnreadPostsCount"] = 3; // À remplacer par le vrai compteur

            return View(projects);
        }

        // DETAILS (DASHBOARD)
        public IActionResult Details(Guid id)
        {
            Project project = _projectService.GetById(id);
            IEnumerable<Employee> members = _employeeService.GetFromProjectId(id);

            IEnumerable<Post> posts;
            if (_session.IsManager)
            {
                posts = _postService.GetFromProjectIdForManager(id);
            }
            else
            {
                Guid employeeId = _session.EmployeeId!.Value;
                posts = _postService.GetFromProjectIdForEmployee(id, employeeId);
            }

            ViewData["EmployeeName"] = $"{_session.FirstName} {_session.LastName}";
            ViewData["IsManager"] = _session.IsManager;
            ViewData["UnreadPostsCount"] = 3; // À remplacer par le vrai compteur

            return View(new ProjectDetailsModel
            {
                Project = project,
                Members = members,
                Posts = posts
            });
        }

        [ManagerOnlyAttribute]
        public IActionResult Create()
        {
            ViewData["EmployeeName"] = $"{_session.FirstName} {_session.LastName}";
            ViewData["IsManager"] = _session.IsManager;
            ViewData["UnreadPostsCount"] = 3;

            return View();
        }

        [HttpPost]
        [ManagerOnlyAttribute]
        public IActionResult Create(CreateForm form)
        {
            if (!ModelState.IsValid)
                return View(form);

            Guid managerId = _session.EmployeeId!.Value;
            Project project = new Project(form.Name, form.Description, managerId);
            _projectService.Insert(project);

            return RedirectToAction("Index");
        }
    }
}
