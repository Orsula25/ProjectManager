using ProjectManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Project
    {
        public Guid ProjectId { get; private set; }

        public string Name { get; private set; }

        private string _description;
        public string Description 
        {
            get { return _description; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                
                    throw new ArgumentException(nameof(Description));

                    _description = value;
                }
            }
            public DateTime CreationDate { get; private set; }
            public Guid ProjectManagerId { get; private set; }

        // Navigation properties
        public ICollection<Employee>? Members { get; set; }

        // Constructeur (DAL -> BLL)

        public Project(Guid projectId, string name, string description, DateTime creationDate, Guid projectManagerId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
           
            ProjectId = projectId;
            Name = name;
            Description = description;
            CreationDate = creationDate;
            ProjectManagerId = projectManagerId;

        }

        // constructeur BLL

        public Project(string name, string description, Guid projectManagerId) 
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            ProjectId = Guid.NewGuid();
            Name = name;
            Description = description;
            CreationDate = DateTime.Now;
            ProjectManagerId = projectManagerId;

        }

        // modifier la description

        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

    }

}
