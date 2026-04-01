using Microsoft.Data.SqlClient;
using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using ProjectManager.DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Services
{
    public class ProjectService : IProjectRepository<Project>
    {

        private readonly SqlConnection _connection;

        public ProjectService(SqlConnection connection)
        {
            _connection = connection;
        }


        //SP_Project_Get_ById
        public Project GetById(Guid projectId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_ById";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProjectId", projectId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        Project p = reader.ToProject();

                        if (p.ProjectId == projectId)
                            return p;
                    }

                    throw new ArgumentOutOfRangeException(nameof(projectId));
                }
            }

        }
        // liste des projets d'un employe:SP_Project_Get_FromEmployeeId

        public IEnumerable<Project> GetFromEmployeeId(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {

                command.CommandText = "SP_Project_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToProject();


                    }
                }
            }

        }

        public IEnumerable<Project> GetFromProjectManagerId(Guid projectManagerId)
        {

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Get_FromProjectManagerId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProjectManagerId", projectManagerId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToProject();

                    }

                }

            }

        }

        //SP_Project_Insert
        public Guid Insert(Project project)
        {

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Insert";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@projectManagerId", project.ProjectManagerId);
                command.Parameters.AddWithValue("@name", project.Name);
                command.Parameters.AddWithValue("@description", project.Description);
                _connection.Open();

                
                try
                {
                    Guid id = (Guid)command.ExecuteScalar();
                    return id;
                }
                finally
                {
                    _connection.Close();
                }
            }

        }

        // SP_Project_Update
        public void Update(Project project)
        {

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Project_Update";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProjectId", project.ProjectId);
                command.Parameters.AddWithValue("@Description", project.Description);

                _connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                finally
                {
                    _connection.Close();
                }
            }
        }
    }
}