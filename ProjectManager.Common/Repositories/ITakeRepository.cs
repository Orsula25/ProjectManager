using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Common.Repositories
{
    public interface ITakeRepository<ITakePart>
    {

        public void AddMenber(Guid employeeId, Guid projectId, DateTime startDate);

        public void RemoveMenber(Guid employeeId, Guid projectId, DateTime endDate);


    }
}
