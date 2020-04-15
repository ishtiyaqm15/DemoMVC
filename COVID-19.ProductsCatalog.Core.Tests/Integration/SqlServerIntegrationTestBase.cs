using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19.ProductsCatalog.Core.Tests.Integration
{
    [IntegrationTest]
    public class SqlServerIntegrationTestBase
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["Development"].ConnectionString;

        protected string ConnectionString { get { return _connectionString; } }

        protected void ExecuteSqlScript(string filePath)
        {
            var scriptDir = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));
            string script = File.ReadAllText(Path.Combine(scriptDir, "Scripts", filePath));

            SqlConnection conn = new SqlConnection(_connectionString);
            using (conn)
            {
                conn.Open();
                var cmd = new SqlCommand(script, conn);
                cmd.ExecuteNonQuery();
            }
        }

    }

    public class IntegrationTestAttribute : CategoryAttribute
    {
        public IntegrationTestAttribute()
            : base("Integration")
        {
        }
    }
}
