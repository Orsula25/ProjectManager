
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class User
    {
        public Guid UserId { get; private set; }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Email));
                _email = value;
            }
        }

        public string Password { get; private set; }

        public Guid EmployeeId { get; private set; }

        // constructeur dal -> bll

        public User(Guid userId, string email, Guid employeeId)
        {
          UserId = userId;
          Email = email;
          EmployeeId = employeeId;
        }

        // Constructeur login 

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
