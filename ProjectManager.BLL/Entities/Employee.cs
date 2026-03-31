using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime HireDate { get; private set; }

        public bool IsProjectManager { get; private set; }

        public string Email { get; private set; }

        // constructeur

        public Employee(Guid employeeId, string firstName, string lastName, DateTime hireDate, bool isProjectManager, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException(nameof(lastName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));


            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            HireDate = hireDate;
            IsProjectManager = isProjectManager;
            Email = email;
        }


    }
}
