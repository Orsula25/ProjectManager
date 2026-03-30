using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using ProjectManager.DAL.Mappers;
using ProjectManager.DAL.Entities;
using System.Threading.Channels;

namespace ProjectManager.DAL.Services
{
    public class EmployeeService : IEmployeeRepository<Employee>
    {
        private readonly SqlConnection _connection;

        public EmployeeService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Employee GetEmployeeFromUserId(Guid userId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromUserId";
                command.CommandType = CommandType.StoredProcedure;

                // paramètre

                command.Parameters.AddWithValue("@UserId", userId);

                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        return reader.ToEmployee();
                    }
                }
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

        }

        public IEnumerable<Employee> GetFree()
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_GetFree";
                command.CommandType = CommandType.StoredProcedure;
                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        yield return reader.ToEmployee();
                    }
                }
            }
        }

        public IEnumerable<Employee> GetFromProjectId(Guid projectId)
        {
            using(SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Get_FromProjectId";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProjectId", projectId);

                _connection.Open();

                using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while(reader.Read())
                    {
                        yield return reader.ToEmployee();
                    }
                }
            }
        }

        public bool IsProjectManager(Guid employeeId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Check_IsProjectManager";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                _connection.Open();

                object result = command.ExecuteScalar();

                _connection.Close();

                return result != null && (bool)result;
            }
        }

        public bool WorkOnProject(Guid employeeId, Guid projectId)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_Employee_Check_WorkOnProject";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                command.Parameters.AddWithValue("@ProjectId", projectId);
                _connection.Open();

                object result = command.ExecuteScalar();
                _connection.Close();

                return result != null;
            }
        }
    }
}
