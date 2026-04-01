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
    public class EmployeeService : IEmployeeRepository<Employee>
    {
        public readonly IEmployeeRepository<DAL.Entities.Employee> _dalService;

        public EmployeeService(IEmployeeRepository<DAL.Entities.Employee> dalService)
        {
            _dalService = dalService;
        }

        // recuperer l'employee via id
        public Employee GetById(Guid id)
        {
            return _dalService.GetById(id).ToBLL();
        }

        // recuperer l'employee via user id 
        public Employee GetEmployeeFromUserId(Guid userId)
        {
            return _dalService.GetEmployeeFromUserId(userId).ToBLL();
        }

        // employe disponible pour un projet
        public IEnumerable<Employee> GetFree()
        {
           return _dalService
                .GetFree()
                .Select(e => e.ToBLL());
        }

        // liste employe d'un projet 
        public IEnumerable<Employee> GetFromProjectId(Guid projectId)
        {
            return _dalService
                .GetFromProjectId(projectId)
                .Select(e => e.ToBLL());
        }
        
        // si project manager 
        public bool IsProjectManager(Guid employeeId)
        {
            return _dalService.IsProjectManager(employeeId);
        }

        // verifie si l'employe travaille sur le projet
        public bool WorkOnProject(Guid employeeId, Guid projectId)
        {
            return _dalService.WorkOnProject(employeeId, projectId);
        }
    }
}
