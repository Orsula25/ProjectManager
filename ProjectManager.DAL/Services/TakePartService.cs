using Microsoft.Data.SqlClient;
using ProjectManager.Common.Repositories;
using ProjectManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Services
{
    public class TakePartService : ITakeRepository<TakePart>
    {

         public readonly SqlConnection _connection;

        public TakePartService(SqlConnection connection)
        {
            _connection = connection;
        }

        // ajouter un employe a un projet
        

        public void AddMenber(Guid employeeId, Guid projectId, DateTime startDate)
        {
            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_TakePart_Insert";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@employeeId", employeeId);
                command.Parameters.AddWithValue("@projectId", projectId);
                command.Parameters.AddWithValue("@startDate", startDate);

                _connection.Open();

                command.ExecuteNonQuery();
                _connection.Close();
            }
        }

        // retirer un employe d'un projet


        public void RemoveMenber(Guid employeeId, Guid projectId, DateTime endDate)
        {

            using (SqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SP_TakePart_SetEnd";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@employeeId", employeeId);
                command.Parameters.AddWithValue("@projectId", projectId);
                command.Parameters.AddWithValue("@endDate", endDate);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }


        }
    }
}
