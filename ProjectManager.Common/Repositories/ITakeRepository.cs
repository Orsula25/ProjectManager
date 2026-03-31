using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface ITakeRepository<TTakePart>
    {

        public void AddMember(Guid employeeId, Guid projectId, DateTime startDate);

        public void RemoveMember(Guid employeeId, Guid projectId, DateTime endDate);


    }
}
