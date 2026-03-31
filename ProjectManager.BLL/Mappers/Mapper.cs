using ProjectManager.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ProjectManager.BLL.Mappers
{
    public static class Mapper
    {
        #region Employee
        public static BLL.Entities.Employee ToBLL(this DAL.Entities.Employee entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return new Employee(
                entity.EmployeeId,
                entity.FirstName,
                entity.LastName,
                entity.HireDate,
                entity.IsProjectManager,
                entity.Email
            );

        }

        public static DAL.Entities.Employee ToDAL(this BLL.Entities.Employee entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.Employee
            {
                EmployeeId = entity.EmployeeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                HireDate = entity.HireDate,
                IsProjectManager = entity.IsProjectManager,
                Email = entity.Email
            };
        }
        #endregion

        #region Project
        public static Project ToBLL(this DAL.Entities.Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new Project(
                entity.ProjectId,
                entity.Name,
                entity.Description,
                entity.CreationDate,
                entity.ProjectManagerId
            );
        }

        public static DAL.Entities.Project ToDAL(this Project entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return new DAL.Entities.Project()
            {
                ProjectId = entity.ProjectId,
                Name = entity.Name,
                Description = entity.Description,
                CreationDate = entity.CreationDate,
                ProjectManagerId = entity.ProjectManagerId
            };
        }




        #endregion

        #region User

        public static User ToBLL(this DAL.Entities.User entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new User(
                entity.UserId,
                entity.Email,
                entity.EmployeeId
            );
        }

        public static DAL.Entities.User ToDAL(this User entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.User()
            {
                UserId = entity.UserId,
                Email = entity.Email,
                EmployeeId = entity.EmployeeId
            };
        }




        #endregion

        #region Post

        public static BLL.Entities.Post ToBLL(this DAL.Entities.Post entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new BLL.Entities.Post(
                  entity.PostId,
                  entity.Subject,
                  entity.Content,
                  entity.SendDate,
                  entity.EmployeeId,
                  entity.ProjectId

            );
        }

        public static DAL.Entities.Post ToDAL(this BLL.Entities.Post entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.Post()
            {
                PostId = entity.PostId,
                Subject = entity.Subject,
                Content = entity.Content,
                SendDate = entity.SendDate,
                EmployeeId = entity.EmployeeId,
                ProjectId = entity.ProjectId
            };
        }



        #endregion

        #region TakePart

        public static BLL.Entities.TakePart ToBLL(this DAL.Entities.TakePart entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            return new TakePart(
                entity.EmployeeId,
                entity.ProjectId,
                entity.StartDate,
                entity.EndDate
            );
        }


        public static DAL.Entities.TakePart ToDAL(this BLL.Entities.TakePart entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            return new DAL.Entities.TakePart()
            {
                EmployeeId = entity.EmployeeId,
                ProjectId = entity.ProjectId,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate
            };
        }


        #endregion



    }
}