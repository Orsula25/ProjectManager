using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface IProjectRepository<TProject>
    {
        public IEnumerable <TProject> GetFromEmployeeId(Guid employeeId);

        public IEnumerable <TProject> GetFromProjectManagerId(Guid projectManagerId);
        public TProject GetById(Guid projectId);
         public Guid Insert(TProject project);

        public void Update(TProject project);
    }
}
