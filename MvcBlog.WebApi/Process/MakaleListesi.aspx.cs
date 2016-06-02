using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MvcBlogDbEngine;
using log4net;
namespace MvcBlog.WebApi.Process
{
    public partial class MakaleListesi : System.Web.UI.Page
    {
        static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Dictionary<string, object>> result;
        protected void Page_Load(object sender, EventArgs e)
        {
            var dt = new DbEngine(0);

            var prms = new List<DbParam>
            {
              //new DbParam { }
            };
            result = (List<Dictionary<string, object>>)dt.ReadData(DbEngine.PType.MakaleGetir, prms, DbEngine.RTypes.List);
            Response.Write(LitJson.JsonMapper.ToJson(result));
        }
    }
}