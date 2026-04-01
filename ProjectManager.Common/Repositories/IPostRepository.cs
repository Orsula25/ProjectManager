using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IPostRepository<TPost>
    {

        public IEnumerable<TPost> GetFromProjectIdForManager(Guid projectId);
        public IEnumerable<TPost> GetFromProjectIdForEmployee(Guid projectId, Guid employeeId);
        public IEnumerable<TPost> GetAllFromManager(Guid managerId);
        public IEnumerable<TPost> GetAllFromEmployee(Guid employeeId);

        public Guid Insert (TPost post);

        public void Update (TPost post);
    }
}
