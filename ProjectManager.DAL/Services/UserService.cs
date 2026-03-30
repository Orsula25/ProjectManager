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
    public class UserService : IUserRepository<User>
    {
        private readonly SqlConnection _connection;

      
        public UserService(SqlConnection connection)
        {
            _connection = connection;
        }
        // verificaton mail+ mot de passe : SP_User_CheckPassword
        public Guid? CheckPassword(string email, string password)
        {
           using(SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_User_CheckPassword";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                _connection.Open();
           

                object result = command.ExecuteScalar();

                _connection.Close();

                return result != null ? (Guid?)result : null;
            }
        }

       
        public User GetFromEmployeeId(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_User_Get_FromEmployeeId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                _connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.ToUser();

                    }
                }
                throw new ArgumentOutOfRangeException(nameof(employeeId));
            }
    }
    }
}
