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

        private readonly SqlConnection _connection;

        public PostService(SqlConnection connection)
        {
            _connection = connection;
        }


        // recuperer les post (employee )


        public IEnumerable<Post> GetFromProjectIdForEmployee(Guid projectId, Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Post_Get_FromProjectId_WorkOnProject";
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
            List<Guid> projectIds = new List<Guid>();

            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SP_Project_Get_FromProjectManagerId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectManagerId", managerId);
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    while (reader.Read())
                        projectIds.Add((Guid)reader["ProjectId"]);
            }

            List<Post> posts = new List<Post>();
            foreach (Guid projectId in projectIds)
            {
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SP_Post_Get_FromProjectId_ProjectManager";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    _connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (reader.Read())
                            posts.Add(reader.ToPost());
                }
            }
            return posts;
        }

        // tous les posts pour un employé
        public IEnumerable<Post> GetAllFromEmployee(Guid employeeId)
        {
            List<Guid> projectIds = new List<Guid>();

            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SP_Project_Get_FromEmployeeId";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                _connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    while (reader.Read())
                        projectIds.Add((Guid)reader["ProjectId"]);
            }

            List<Post> posts = new List<Post>();
            foreach (Guid projectId in projectIds)
            {
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SP_Post_Get_FromProjectId_WorkOnProject";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProjectId", projectId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    _connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        while (reader.Read())
                            posts.Add(reader.ToPost());
                }
            }
            return posts;
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