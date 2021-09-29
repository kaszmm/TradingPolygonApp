using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ServerLogicLibrary
{
    public class DataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (IDbConnection connect = new SqlConnection(connectionString))
            {
                List<T> rows = connect.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (IDbConnection connect = new SqlConnection(connectionString))
            {
                connect.Execute(sqlStatement, parameters);
            }
        }
    }
}
