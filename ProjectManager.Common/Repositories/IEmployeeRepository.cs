using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IEmployeeRepository<TEmployee>
    {
        TEmployee GetById(Guid id);
        TEmployee GetEmployeeFromUserId(Guid userId);
        bool IsProjectManager(Guid employeeId);
        bool WorkOnProject(Guid employeeId, Guid projectId);
        IEnumerable<TEmployee> GetFromProjectId(Guid projectId);
        IEnumerable<TEmployee> GetFree();
    }
}
