using COVID_19.ProductsCatalog.Core.DomainModels;
using Dapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace COVID_19.ProductsCatalog.Core.Common
{
    public abstract class ConnectionManager
    {
        #region Declarations
        private string connString;
        private int COMMAND_TIMEOUT = 600;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private SqlConnection _sqlConnection;
        #endregion

        #region Constructors
        public ConnectionManager()
            : this(ConfigurationSettings.ConnectionString)
        {
        }

        public ConnectionManager(string ConnectionString)
        {
            connString = ConnectionString;
        }
        #endregion

        #region Connection Operations
        private SqlConnection Make(string connectionstring)
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(connectionstring);
            }
            return _sqlConnection;
        }

        private void Disconnect()
        {
            if (_sqlConnection != null)
                _sqlConnection.Dispose();
        }
        #endregion
              

        #region Generic Actions Using Dapper ORM

        #region Common Method/Function used to get the Filtered information or specific information by scalar/int/string in return
        /// <summary>
        /// This is the generic function used accross the business logic whenever needed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns>IEnumerable of Object Type T</returns>
        protected IEnumerable<T> ExecuteAndGetAsObject<T>(string commandText, object param, CommandType commandType)
        {
            IEnumerable<T> results = null;
            SqlConnection conn = Make(connString);

            try
            {
                results = conn.Query<T>(commandText, param, commandType: commandType);
            }
            catch (Exception e)
            {
                _logger.LogException(LogLevel.Error, "Error occurred in ExecuteAndGetAsObject", e);
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        /// <summary>
        /// This Function returns nothing but process the command text which are just need to be executed
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        protected void ExecuteWithNoResult(string commandText, object param, CommandType commandType)
        {
            SqlConnection conn = Make(connString);

            try
            {
                conn.Execute(commandText, param, commandType: commandType);
            }
            catch (Exception e)
            {
                _logger.LogException(LogLevel.Error, "Error occurred in ExecuteWithNoResult", e);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// This Function as generics of the Above to Utilize if the output is just an Int
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns>Int</returns>
        protected int ExecuteAndGetAsInteger(string commandText, object param, CommandType commandType)
        {
            int result = 0;
            SqlConnection conn = Make(connString);

            try
            {
                var outPut = conn.Query<int>(commandText, param, commandType: commandType);
                if (outPut != null && outPut.Any())
                {
                    result = outPut.First();
                }
            }
            catch (Exception e)
            {
                _logger.LogException(LogLevel.Error, "Error occurred in ExecuteAndGetAsInteger", e);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// This Function as generics of the Above to Utilize if the output is just an bool
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns>bool</returns>
        protected bool ExecuteAndGetAsBoolean(string commandText, object param, CommandType commandType)
        {
            bool result = false;
            SqlConnection conn = Make(connString);

            try
            {
                var outPut = conn.Query<bool>(commandText, param, commandType: commandType);
                if (outPut != null && outPut.Any())
                {
                    result = outPut.First();
                }
            }
            catch (Exception e)
            {
                _logger.LogException(LogLevel.Error, "Error occurred in ExecuteAndGetAsBoolean", e);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        /// <summary>
        /// This Function as generics of the Above to Utilize if the output is just an string
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="param"></param>
        /// <param name="commandType"></param>
        /// <returns>string</returns>
        protected string ExecuteAndGetAsString(string commandText, object param, CommandType commandType)
        {
            string result = string.Empty;
            SqlConnection conn = Make(connString);

            try
            {
                var outPut = conn.Query<string>(commandText, param, commandType: commandType);
                if (outPut != null && outPut.Any())
                {
                    result = outPut.First();
                }
            }
            catch (Exception e)
            {
                _logger.LogException(LogLevel.Error, "Error occurred in ExecuteAndGetAsString", e);
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        #endregion
               

        #region Few Methods that can be used accross BL If needed to get data in different forms
        protected IEnumerable<TParent> QueryParentChild<TParent, TChild, TParentKey>(Func<TParent, TParentKey> parentKeySelector,
                                                                                     Func<TParent, ICollection<TChild>> childSelector,
                                                                                     string command,
                                                                                     dynamic param,
                                                                                     string splitOn,
                                                                                     CommandType commandType,
                                                                                     IDbTransaction transaction = null,
                                                                                     bool buffered = true,
                                                                                     int? commandTimeout = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();
            SqlConnection conn = Make(connString);

            try
            {
                conn.Query<TParent, TChild, TParent>(
                    command,
                    (parent, child) =>
                    {
                        if (!cache.ContainsKey(parentKeySelector(parent)))
                        {
                            cache.Add(parentKeySelector(parent), parent);
                        }

                        TParent cachedParent = cache[parentKeySelector(parent)];
                        ICollection<TChild> children = childSelector(cachedParent);
                        children.Add(child);
                        return cachedParent;
                    },
                    param as object, null, true, splitOn, 30, commandType);
            }
            catch (Exception e)
            {
                _logger.LogException(LogLevel.Error, "Error occurred in ExecuteAndGetAsObject", e);
            }
            finally
            {
                conn.Close();
            }
            return cache.Values;
        }

        protected IEnumerable<TParent> QueryParentChild<TParent, TChildA, TChildB, TParentKey>(Func<TParent, TParentKey> parentKeySelector,
                                                                             Func<TParent, ICollection<TChildA>> childSelectorA,
                                                                             Func<TParent, ICollection<TChildB>> childSelectorB,
                                                                             string command,
                                                                             dynamic param,
                                                                             string splitOn,
                                                                             CommandType commandType,
                                                                             IDbTransaction transaction = null,
                                                                             bool buffered = true,
                                                                             int? commandTimeout = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();
            SqlConnection conn = Make(connString);

            try
            {
                conn.Query<TParent, TChildA, TChildB, TParent>(
                    command,
                    (parent, childA, childB) =>
                    {
                        if (!cache.ContainsKey(parentKeySelector(parent)))
                        {
                            cache.Add(parentKeySelector(parent), parent);
                        }

                        TParent cachedParent = cache[parentKeySelector(parent)];
                        ICollection<TChildA> childrenA = childSelectorA(cachedParent);
                        childrenA.Add(childA);
                        ICollection<TChildB> childrenB = childSelectorB(cachedParent);
                        childrenB.Add(childB);
                        return cachedParent;
                    },
                    param as object, null, true, splitOn, 30, commandType);
            }
            catch (Exception e)
            {
                _logger.LogException(LogLevel.Error, "Error occurred in ExecuteAndGetAsObject", e);
            }
            finally
            {
                conn.Close();
            }
            return cache.Values;
        }
        #endregion

        #endregion Generic Actions Using Dapper ORM
    }
}