using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19.ProductsCatalog.Core.Common
{
    public class ConfigurationSettings
    {
        public static string _connectionString = "";
        public static string _multicastAddress = null, _environmentIdentifier;
        public static string _connectionStringUnitTest = "";

        #region Environment Related
        public static string MulticastAddress
        {
            get
            {
                if (_multicastAddress != null)
                    return (_multicastAddress);
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(GetConnectionStringBase());
                IPHostEntry hostEntries = null;
                try
                {
                    hostEntries =
                        Dns.GetHostEntry(builder.DataSource.Equals(".") || builder.DataSource.Equals("localhost")
                                             ? Dns.GetHostName()
                                             : builder.DataSource);
                }
                catch (Exception ex)
                {
                    //if the datasource is an ip, line 40 below here will parse it and we'll use that... otherwise it'll get IP's from the hostname
                    hostEntries = Dns.GetHostEntry(Dns.GetHostName());
                }
                IPAddress addr = null;
                //Removing instance name from datasource.
                string dataSource = builder.DataSource;
                if (dataSource.Contains('\\'))
                {
                    dataSource = dataSource.Substring(0, dataSource.IndexOf('\\'));
                }
                IPAddress.TryParse(dataSource, out addr);
                if (addr == null)
                {

                    foreach (var ip in hostEntries.AddressList)
                    {
                        if (ip.AddressFamily != AddressFamily.InterNetwork)
                            continue;
                        addr = ip;
                        break;
                    }
                }
                int charCounter = 0;
                foreach (var c in builder.InitialCatalog)
                    charCounter += c;
                byte hc = (byte)(charCounter % 255);
                var toUse = addr.GetAddressBytes();
                _multicastAddress = String.Format("232.{0}.{1}.{2}", toUse[2], toUse[3], hc);

                return _multicastAddress;
            }
        }

        public static String GetEnvironmentIdentifier()
        {
            if (_environmentIdentifier == null)
            {
                _environmentIdentifier = MulticastAddress.Replace(".", "").Substring(3);
                _environmentIdentifier = (Convert.ToInt32(_environmentIdentifier) % 255).ToString();
            }
            return _environmentIdentifier;
        }

        public static string InitialCatalog
        {
            get
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);

                return builder.InitialCatalog;
            }
        }
        #endregion

        #region Connection String accessors

        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionStringUnitTest = string.Empty;
                    _connectionString = GetConnectionStringBase();
                }
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static string SetConnectionString(string connectionString)
        {
            _connectionStringUnitTest = string.Empty;
            _connectionString = connectionString;
            return _connectionString;
        }

        public static string SetConnectionString()
        {
            _connectionStringUnitTest = string.Empty;
            _connectionString = GetConnectionStringBase();
            return _connectionString;
        }

        public static string GetConnectionStringBase()
        {
            var connStringSettings = ConfigurationManager.ConnectionStrings[AppSettings.Environment];
            string connectionString = connStringSettings.ConnectionString;
            return connectionString;
        }

        public static string GetConnectionStringBase(string connectionName)
        {
            var connStringSettings = ConfigurationManager.ConnectionStrings[connectionName];
            string connectionString = connStringSettings.ConnectionString;
            return connectionString;
        }
        #endregion

        #region Connectionstring Need to Used for UnitTestDB

        public static string ConnectionStringUnitTest
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionStringUnitTest))
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectionString);
                    builder.InitialCatalog = builder.InitialCatalog + "_UnitTest";
                    _connectionStringUnitTest = builder.ConnectionString;
                }
                return _connectionStringUnitTest;
            }
            set
            {
                _connectionStringUnitTest = value;
            }
        }


        public static string SetConnectionStringForUnitTest(bool forNewDB = false)
        {
            _connectionStringUnitTest = string.Empty;
            _connectionString = GetConnectionStringBase();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_connectionString);
            if (!builder.InitialCatalog.EndsWith(Environment.MachineName.Replace("-", "_")))
                builder.InitialCatalog = builder.InitialCatalog + "_" + System.Environment.MachineName.Replace("-", "_");
            _connectionString = builder.ConnectionString;
            if (forNewDB)
                return _connectionString;
            try
            {
                SqlConnection con = new SqlConnection(_connectionString);
                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select 1", con);

                    cmd.ExecuteNonQuery();
                    // it exists! use it 

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + Environment.NewLine + _connectionString);
                // machine config didn't work
                _connectionString = GetConnectionStringBase();
            }


            return _connectionString;
        }

        #endregion

    }
}