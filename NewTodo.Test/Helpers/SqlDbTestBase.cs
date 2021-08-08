using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Respawn;

namespace NewTodo.Test.Helpers
{
    public abstract class SqlDbTestBase
    {
        private static readonly string ConnectionString = Environment.GetEnvironmentVariable("DBCONNECTION");
        protected SqlConnection Connection;

        private static readonly Checkpoint Checkpoint = new Checkpoint()
        {
            TablesToIgnore = new[] {"VersionInfo"}
        };

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
            return new SqlConnection(ConnectionString);
        }

        protected async Task ResetDatabase()
        {
            await Checkpoint.Reset(ConnectionString);
        }
    }
}