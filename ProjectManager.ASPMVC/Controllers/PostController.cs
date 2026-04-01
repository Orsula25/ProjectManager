using Microsoft.AspNetCore.Mvc;
using ProjectManager.ASPMVC.Handlers;
using ProjectManager.ASPMVC.Handlers.Filters;
using ProjectManager.BLL.Entities;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;

namespace ProjectManager.ASPMVC.Controllers
{
    [AuthFilter]
    public class PostController : Controller
    {
        private readonly IPostRepository<ProjectManager.BLL.Entities.Post> _postService;
        private readonly UserSessionManager _session;

        public PostController(
            IPostRepository<ProjectManager.BLL.Entities.Post> postService,
            UserSessionManager session)
        {
            _postService = postService;
            _session = session;
        }

        public IActionResult Index()
        {
            Guid employeeId = _session.EmployeeId!.Value;
            IEnumerable<Post> posts;

            if (_session.IsManager)
            {
                // Récupérer tous les posts des projets gérés
                posts = _postService.GetAllFromManager(employeeId);
            }
            else
            {
                // Récupérer tous les posts des projets où l'employé est membre
                posts = _postService.GetAllFromEmployee(employeeId);
            }

            ViewData["EmployeeName"] = $"{_session.FirstName} {_session.LastName}";
            ViewData["IsManager"] = _session.IsManager;
            ViewData["UnreadPostsCount"] = posts.Count();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Create(Guid projectId)
        {
            ViewData["EmployeeName"] = $"{_session.FirstName} {_session.LastName}";
            ViewData["IsManager"] = _session.IsManager;
            ViewData["UnreadPostsCount"] = 3;
            ViewData["ProjectId"] = projectId;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Guid projectId, string subject, string content)
        {
            Guid employeeId = _session.EmployeeId!.Value;
            Post post = new Post(subject, content, employeeId, projectId);
            _postService.Insert(post);

            return RedirectToAction("Details", "Project", new { id = projectId });
        }
    }
}
