using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Respawn;

namespace NewTodo.Test.Helpers
{
    public abstract class SqlDbTestBase
    {
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
            var connectionString = TestHelper.GetConnectionString(Environment.CurrentDirectory);
            return new SqlConnection(connectionString);
        }

        protected async Task ResetDatabase()
        {
            var connectionString = TestHelper.GetConnectionString(Environment.CurrentDirectory);
            await Checkpoint.Reset(connectionString);
        }
    }
}