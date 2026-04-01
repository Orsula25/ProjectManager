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
        private readonly ITakeRepository<ProjectManager.BLL.Entities.TakePart> _takePartService;
        private readonly UserSessionManager _session;

        public ProjectController(
            IProjectRepository<ProjectManager.BLL.Entities.Project> projectService,
            IEmployeeRepository<ProjectManager.BLL.Entities.Employee> employeeService,
            IPostRepository<ProjectManager.BLL.Entities.Post> postService,
            ITakeRepository<ProjectManager.BLL.Entities.TakePart> takePartService,
            UserSessionManager session)
        {
            _projectService = projectService;
            _employeeService = employeeService;
            _postService = postService;
            _takePartService = takePartService;
            _session = session;
        }

        public IActionResult Index()
        {
            Guid employeeId = _session.EmployeeId!.Value;

            IEnumerable<Project> projects = _session.IsManager
                ? _projectService.GetFromProjectManagerId(employeeId)
                : _projectService.GetFromEmployeeId(employeeId);

            ViewData["EmployeeName"] = $"{_session.FirstName} {_session.LastName}";
            ViewData["IsManager"] = _session.IsManager;
            ViewData["UnreadPostsCount"] = 3;

            return View(projects);
        }

        public IActionResult Details(Guid id)
        {
            Project project = _projectService.GetById(id);
            IEnumerable<Employee> members = _employeeService.GetFromProjectId(id);
            IEnumerable<Employee> freeEmployees = _employeeService.GetFree();

            IEnumerable<Post> posts;
            if (_session.IsManager)
                posts = _postService.GetFromProjectIdForManager(id);
            else
            {
                Guid employeeId = _session.EmployeeId!.Value;
                posts = _postService.GetFromProjectIdForEmployee(id, employeeId);
            }

            ViewData["EmployeeName"] = $"{_session.FirstName} {_session.LastName}";
            ViewData["IsManager"] = _session.IsManager;
            ViewData["UnreadPostsCount"] = 3;

            return View(new ProjectDetailsModel
            {
                Project = project,
                Members = members,
                Posts = posts,
                FreeEmployees = freeEmployees
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ManagerOnlyAttribute]
        public IActionResult UpdateDescription(Guid id, string description)
        {
            Project project = _projectService.GetById(id);
            project.UpdateDescription(description);
            _projectService.Update(project);
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ManagerOnlyAttribute]

        public IActionResult AddMember(Guid projectId, Guid employeeId)
        {

            _takePartService.AddMember(employeeId, projectId, DateTime.Now);
            return RedirectToAction("Details", new { id = projectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ManagerOnlyAttribute]
        public IActionResult RemoveMember(Guid projectId, Guid employeeId)
        {
            

            _takePartService.RemoveMember(employeeId, projectId, DateTime.Now);
            return RedirectToAction("Details", new { id = projectId });
        }
    }
}