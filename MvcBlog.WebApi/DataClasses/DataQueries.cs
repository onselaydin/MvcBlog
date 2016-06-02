using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBlog.WebApi.Containers;
using System.IO;
using log4net;
using System.Reflection;

namespace MvcBlogDbEngine
{
    public static class DataQueries
    {
        static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static Dictionary<DbEngine.PType, QueryTypes> Queries;

        private static string Sql_Makale;
        // private static string Sql_Makale;
        static DataQueries()
        {
            Sql_Makale = GetQuery("MakaleListesi.sql");

            LoadQueires();
        }


        public static void LoadQueires()
        {
           // if (Queries != null && Queries.Count > 0) return;

            

            Queries = new Dictionary<DbEngine.PType, QueryTypes>
            {
                 {
                    DbEngine.PType.MakaleGetir, new QueryTypes()
                    {
                        Query = Sql_Makale,
                        ResultType = ResultTypes.MULTIPLE
                    } 
                    //,
                    // DbEngine.PType.MakaleGetir, new QueryTypes()
                    //{
                    //    Query = Sql_Makale,
                    //    ResultType = ResultTypes.MULTIPLE
                    //}
                 }
            };

        }

        

        public static string GetQuery(string fileName)
        {
            var str = "";

            try
            {
                str = File.ReadAllText(AssemblyDirectory() + "/sql/" + fileName);
            }
            catch (Exception ex)
            {
                log.ErrorFormat(ex.Message);
            }

            return str;
        }
        private static string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

    }
    public class QueryTypes
    {
        public string Query;
        public ResultTypes ResultType;
    }
    public enum ResultTypes
    {
        NONE,
        SINGLE,
        MULTIPLE
    }


}