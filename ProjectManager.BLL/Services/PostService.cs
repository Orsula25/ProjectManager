using ProjectManager.BLL.Entities;
using ProjectManager.BLL.Mappers;
using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class PostService : IPostRepository<BLL.Entities.Post>
    {
        private readonly IPostRepository<DAL.Entities.Post> _dalService;
        private readonly IEmployeeRepository<DAL.Entities.Employee> _employeeService;

        public PostService(IPostRepository<DAL.Entities.Post> dalService, IEmployeeRepository<DAL.Entities.Employee> employeeService)
        {
            _dalService = dalService;
            _employeeService = employeeService;
        }



        // visible pour l'employe 
        public IEnumerable<Entities.Post> GetFromProjectIdForEmployee(Guid projectId, Guid employeeId)
        {
            return _dalService
                .GetFromProjectIdForEmployee(projectId, employeeId)
                .Select(p => p.ToBLL());
        }

        // post visible pour le manager
        public IEnumerable<Entities.Post> GetFromProjectIdForManager(Guid projectId)
        {
            return _dalService
                .GetFromProjectIdForManager(projectId)
                .Select(p => p.ToBLL());
        }

        // creer un post
        public Guid Insert(Entities.Post post)
        {
            // verifier si l'employee travaille bien sur le projet 

            if(!_employeeService.WorkOnProject(post.EmployeeId, post.ProjectId))
            {
                throw new Exception("L'employé ne travaille pas sur ce projet.");
            }
            return _dalService.Insert(post.ToDAL());
        }



        // modifier un post

        public void Update(Entities.Post post)
        {
            _dalService.Update(post.ToDAL());
        }
    }
}
