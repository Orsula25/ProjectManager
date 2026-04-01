using Microsoft.Data.SqlClient;
using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using ProjectManager.DAL.Mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Services
{
    public class PostService : IPostRepository<Post>
    {

        public readonly SqlConnection _connection;

        public PostService(SqlConnection connection)
        {
            _connection = connection;
        }


        // recuperer les post (employee )


        public IEnumerable<Post> GetFromProjectIdForEmployee(Guid projectId, Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "  SP_Post_Get_FromProjectId_WorkOnProject";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProjectId", projectId);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToPost();
                    }
                }

            }
        }

        // recuperer les post (Project Manager)
        public IEnumerable<Post> GetFromProjectIdForManager(Guid projectId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_ProjectManager";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProjectId", projectId);

                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {

                        {
                            yield return reader.ToPost();
                        }

                    }

                }

            }



        }
        // tous les posts pour un manager
        public IEnumerable<Post> GetAllFromManager(Guid managerId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = @"SELECT p.* FROM Post p 
                                      INNER JOIN Project pr ON p.ProjectId = pr.ProjectId 
                                      WHERE pr.ProjectManagerId = @ManagerId
                                      ORDER BY p.SendDate DESC";
                command.Parameters.AddWithValue("@ManagerId", managerId);

                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToPost();
                    }
                }
            }
        }

        // tous les posts pour un employé
        public IEnumerable<Post> GetAllFromEmployee(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = @"SELECT p.* FROM Post p 
                                      INNER JOIN TakePart tp ON p.ProjectId = tp.ProjectId 
                                      WHERE tp.EmployeeId = @EmployeeId AND tp.EndDate IS NULL
                                      ORDER BY p.SendDate DESC";
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToPost();
                    }
                }
            }
        }

        // ajouter un post
        public Guid Insert(Post post)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Insert";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@employeeId", post.EmployeeId);
                command.Parameters.AddWithValue("@projectId", post.ProjectId);
                command.Parameters.AddWithValue("@subject", post.Subject);
                command.Parameters.AddWithValue("@content", post.Content);
                _connection.Open();

                Guid id = (Guid)command.ExecuteScalar();
                _connection.Close();

                return id;
            }
        }


        // updater  un post
        public void Update(Post post)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Update";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@postId", post.PostId);
                command.Parameters.AddWithValue("@employeeId", post.EmployeeId);
                command.Parameters.AddWithValue("@content", post.Content);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
