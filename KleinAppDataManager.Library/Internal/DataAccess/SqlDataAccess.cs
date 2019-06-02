using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KleinAppDataManager.Library.Internal.DataAccess
{
    //internal means this can't be used in outside the library
    internal class SqlDataAccess
    {
        public string GetConnectionString(string name)
        {
            string x = ConfigurationManager.ConnectionStrings["KleinAppData"].ConnectionString;
            // return ConfigurationManager.ConnectionStrings[name].ConnectionString;
            //return @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KleinAppData;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return x;
        }


        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName = "KleinAppData")
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }


        }
// Did i need U?
        public void SaveData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
               connection.Execute(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);

          
            }


        }
    }
}
