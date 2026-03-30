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

        public readonly SqlConnection _connection;

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
                    if (reader.Read())
                    {
                        return reader.ToProject();
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
                command.Parameters.AddWithValue("@Name", project.Name);
                command.Parameters.AddWithValue("@Description", project.Description);
                command.Parameters.AddWithValue("@ProjectManagerId", project.ProjectManagerId);
                _connection.Open();
                
                Guid id = (Guid)command.ExecuteScalar();

                _connection.Close();

                return id;
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
                command.ExecuteNonQuery();
                _connection.Close();
            }
           
        }
    }
}
