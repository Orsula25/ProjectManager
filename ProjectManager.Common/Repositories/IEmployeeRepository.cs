using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectManager.Common.Repositories
{
    public interface IEmployeeRepository<TEmployee>
    {

    public TEmployee GetEmployeeFromUserId(Guid userId);

    public bool IsProjectManager(Guid employeeId);

    public bool WorkOnProject (Guid employeeId, Guid projectId);

    public IEnumerable<TEmployee> GetFromProjectId(Guid projectId);

    public IEnumerable<TEmployee> GetFree();


    }
}
