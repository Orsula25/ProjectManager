using ProjectManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Mappers
{
    public static class Mapper
    {

        public static Employee ToEmployee(this IDataRecord record)
        {
            return new Employee
            {
                EmployeeId = (Guid)record["EmployeeId"],
                FirstName = (string)record["Firstname"],
                LastName = (string)record["Lastname"],
                HireDate = (DateTime)record["Hiredate"],
                IsProjectManager = (bool)record["IsProjectManager"],
                Email = (string)record["Email"]
            };
        }

        public static User ToUser(this IDataRecord record)
        {
            return new User
            {
                UserId = (Guid)record[nameof(User.UserId)],
                Email = (string)record[nameof(User.Email)],
                Password = (byte[])record[nameof(User.Password)],
                EmployeeId = (Guid)record[nameof(User.EmployeeId)]
            };

        }

        public static Project ToProject(this IDataRecord record)
        {
            return new Project
            {
                ProjectId = (Guid)record[nameof(Project.ProjectId)],
                Name = (string)record[nameof(Project.Name)],
                Description = (string)record[nameof(Project.Description)],
                CreationDate = (DateTime)record[nameof(Project.CreationDate)],
                ProjectManagerId = (Guid)record[nameof(Project.ProjectManagerId)]
            };
        }


        public static Post ToPost(this IDataRecord record)
        {
            return new Post
            {
                PostId = (Guid)record[nameof(Post.PostId)],
                Subject = (string)record[nameof(Post.Subject)],
                Content = (string)record[nameof(Post.Content)],
                SendDate = (DateTime)record[nameof(Post.SendDate)],
                EmployeeId = (Guid)record[nameof(Post.EmployeeId)],
                ProjectId = (Guid)record[nameof(Post.ProjectId)]
            };

        }




    }


}