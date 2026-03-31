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
    public class UserService : IUserRepository<User>
    {
        private readonly IUserRepository<DAL.Entities.User> _dalService;

        public UserService(IUserRepository<DAL.Entities.User> dalService)
        {
            _dalService = dalService;
        }

        // login 
        public Guid? CheckPassword(string email, string password)
        {
            return _dalService.CheckPassword(email, password);
        }


       // ecuperer user via employee 
        public User GetFromEmployeeId(Guid employeeId)
        {
           return _dalService.GetFromEmployeeId(employeeId).ToBLL();
        }
    }
}
