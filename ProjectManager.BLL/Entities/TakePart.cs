using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class TakePart
    {
        public Guid EmployeeId { get; private set; }

        public Guid ProjectId { get; private set; }

        public DateTime StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

        public bool IsActive => EndDate is null; 

        // constructeur DAL -> BLL

        public TakePart(Guid employeeId, Guid projectID, DateTime startDate, DateTime? endDate ) 
        { 
            EmployeeId = employeeId;
            ProjectId = projectID;
            StartDate = startDate;
            EndDate = endDate;
        }

        // constructeur Ajout membre

        public TakePart(Guid employeeId, Guid projectId)
        {
            EmployeeId = employeeId;
            ProjectId = projectId;
          
        }

    }
}
