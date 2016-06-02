using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;


namespace MvcBlogDbEngine
{
    public class DbEngine
    {
        private string ConnectionString = "Data Source=BIM06\\SQLEXPRESS;Initial Catalog=B627_Blog;Persist Security Info=True;MultipleActiveResultSets=True;User ID=sa;Password=123456";
        private string ConnectionString2 = "";
      

        public enum PType
        {
            MakaleGetir,
        }


        public enum RTypes
        {
            List,
            DataTable
        }
        private SqlConnection _dnCon;

        public DbEngine(int _type, string cnnStr = "")
        {
            ConnectionString2 = cnnStr;
            OpenConnection(_type);
        }
        ~DbEngine()
        {
            CloseConnection();
        }

        private void OpenConnection(int _type)
        {
            try
            {
                var str = "";
                if (_type == 0) str = ConnectionString;
                else if (_type == 1) str = ConnectionString2;
                _dnCon = new SqlConnection(str);
                if (_dnCon.State == ConnectionState.Closed) _dnCon.Open();
            }
            catch (SqlException e)
            {
                throw e;
            }
        }

        private void CloseConnection()
        {
            if (_dnCon == null) return;
            if (_dnCon.State != ConnectionState.Closed)
            {
                _dnCon.Close();
                _dnCon.Dispose();
            }
            else _dnCon.Dispose();
        }

        public void CloseAllConnections()
        {
            CloseConnection();
        }
        public object ReadData(PType dataType, List<DbParam> values, RTypes returnType, bool extraFields = false, string tmpQuery = "")
        {
            var query = DataQueries.Queries[dataType];

            var comm = _dnCon.CreateCommand();
            var txn = _dnCon.BeginTransaction(IsolationLevel.ReadCommitted);
            // comm.BindByName = true;
            comm.Transaction = txn;

            if (extraFields)
            {
                var strParams = "";

                if (values != null)
                {
                    foreach (var v in values)
                    {
                        var p = new SqlParameter(v.key, SqlDbType.VarChar) { Value = v.value, Direction = ParameterDirection.Input };
                        comm.Parameters.Add(p);
                        strParams += v.key + ",";
                    }

                    strParams += "#{LA_ST}#";
                    strParams = strParams.Replace(",#{LA_ST}#", "");
                }

                if (!String.IsNullOrEmpty(tmpQuery))
                {
                    comm.CommandText = tmpQuery.Replace("{PARAMS}", strParams);
                }
                else
                {
                    comm.CommandText = query.Query.Replace("{PARAMS}", strParams);
                }
            }
            else
            {
                if (values != null)
                {
                    foreach (var v in values)
                    {
                        var p = new SqlParameter(v.key, SqlDbType.VarChar) { Value = v.value, Direction = ParameterDirection.Input };
                        comm.Parameters.Add(p);
                    }
                }

                comm.CommandText = query.Query;
            }

            SqlDataReader reader = null;
            dynamic res = null;

            try
            {
                reader = comm.ExecuteReader();
                txn.Commit();

                switch (query.ResultType)
                {
                    case ResultTypes.SINGLE:
                        if (returnType == RTypes.List)
                        {
                            res = new Dictionary<string, object>();
                            while (reader.Read())
                            {
                                for (var i = 0; i < reader.FieldCount; i++)
                                {
                                    try
                                    {
                                        res.Add(reader.GetName(i), reader[i].ToString());
                                    }
                                    catch
                                    {
                                        res.Add(reader.GetName(i), null);
                                    }
                                }
                            }
                        }
                        else
                        {
                            res = new DataTable();
                            res.Load(reader);
                        }
                        break;
                    case ResultTypes.MULTIPLE:
                        if (returnType == RTypes.List)
                        {
                            res = new List<Dictionary<string, object>>();
                            while (reader.Read())
                            {
                                var row = new Dictionary<string, object>();

                                for (var i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetName(i), reader[i] == DBNull.Value ? null : reader[i]);
                                }

                                res.Add(row);
                            }
                        }
                        else
                        {
                            res = new DataTable();
                            res.Load(reader);
                        }
                        break;
                }

                reader.Close();
            }
            catch //(SqlException ex)
            {
                //throw ex;
            }
            CloseConnection();

            return res;
        }

        public object ReadData2(string qry, Dictionary<string, object> values, RTypes returnType)
        {
            dynamic res = null;

            try
            {
                var comm = _dnCon.CreateCommand();
                if (values != null)
                {
                    foreach (var v in values)
                    {
                        comm.Parameters.AddWithValue(v.Key, v.Value);
                    }
                }

                comm.CommandText = qry;

                var reader = comm.ExecuteReader();

                if (returnType == RTypes.List)
                {
                    res = new List<Dictionary<string, object>>();
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader[i]);
                        }

                        res.Add(row);
                    }
                }
                else
                {
                    res = new DataTable();
                    res.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            CloseConnection();

            return res;
        }

        //public static string ReadConnString()
        //{
        //    var str = "";

        //    try
        //    {
        //        str = File.ReadAllText(AssemblyDirectory() + "/constr.txt");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return str;
        //}

        public object WriteData(PType dataType, Dictionary<string, object> values)
        {
            try
            {
                var comm = _dnCon.CreateCommand();

                if (values != null)
                {
                    foreach (var v in values)
                    {
                        comm.Parameters.AddWithValue(v.Key, v.Value);
                    }
                }

                comm.CommandText = DataQueries.Queries[dataType].Query;
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseAllConnections();

            return 1;
        }

        private static string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        

        

    }
}