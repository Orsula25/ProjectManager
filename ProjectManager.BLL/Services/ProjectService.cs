using ProjectManager.BLL.Entities;
using ProjectManager.BLL.Mappers;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class ProjectService : IProjectRepository<Project>
    {
        private  readonly IProjectRepository<DAL.Entities.Project> _dalService;
        private readonly IEmployeeRepository<DAL.Entities.Employee> _employeeService;

        public ProjectService(IProjectRepository<DAL.Entities.Project> dalService, IEmployeeRepository<DAL.Entities.Employee> employeeService)
        {
            _dalService = dalService;
            _employeeService = employeeService;
        }



        // récuperer un  projet 
        public Project GetById(Guid projectId)
        {
            return _dalService.GetById(projectId).ToBLL();
        }

        // récuperer tous les projets dun employé
        public IEnumerable<Project> GetFromEmployeeId(Guid employeeId)
        {
           return _dalService
                .GetFromEmployeeId(employeeId)
                .Select(p => p.ToBLL());
        }


        // récuperer tous les projets dun chef de projet
        public IEnumerable<Project> GetFromProjectManagerId(Guid projectManagerId)
        {
            return _dalService
                .GetFromProjectManagerId(projectManagerId)
                .Select(p => p.ToBLL());

        }

        // creer un projet (uniquement manager)
        public Guid Insert(Project project)
        {
            if (project is null)
                throw new ArgumentNullException(nameof(project));

            if (!_employeeService.IsProjectManager(project.ProjectManagerId))
                throw new UnauthorizedAccessException("Seuls les chefs de projet peuvent créer des projets.");

            return _dalService.Insert(project.ToDAL());

        }

        // modifier un projet (uuniquement manager)
        public void Update(Project project)
        {
            if (project is null)
                throw new ArgumentNullException(nameof(project));
            if (!_employeeService.IsProjectManager(project.ProjectManagerId))
                throw new UnauthorizedAccessException("Seuls les chefs de projet peuvent modifier des projets. ");
            _dalService.Update(project.ToDAL());
        }
    }
}
