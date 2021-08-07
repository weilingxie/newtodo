using System;
using Microsoft.Data.SqlClient;

namespace NewTodo.Test.Helpers
{
    public abstract class SqlDbTestBase
    {
        protected SqlConnection Connection;

        protected SqlDbTestBase()
        {
            SetupConnection();
        }

        private void SetupConnection()
        {
            Connection = CreateConnection();
        }

        private static SqlConnection CreateConnection()
        {
            var connectionString = Environment.GetEnvironmentVariable("DBCONNECTION");
            return new SqlConnection(connectionString);
        }
    }
}