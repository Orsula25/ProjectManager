using ProjectManager.BLL.Entities;
using ProjectManager.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class TakePartService : ITakeRepository<TakePart>
    {
        private readonly ITakeRepository<DAL.Entities.TakePart> _dalService;
        

        public TakePartService(ITakeRepository<DAL.Entities.TakePart> dalService)
        {
            _dalService = dalService;
          
        }




        // ajouter un membre à un projet (uniquement manager)
        public void AddMember(Guid employeeId, Guid projectId, DateTime startDate)
        {
        
            _dalService.AddMember(employeeId, projectId, startDate);


        }
        // retirer un membre d'un projet(uniquement manager)
        public void RemoveMember(Guid employeeId, Guid projectId, DateTime endDate)
        {
          
            _dalService.RemoveMember(employeeId, projectId, endDate);
        }

      
       
    }
}
