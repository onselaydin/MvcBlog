using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MvcBlog.Models
{
    public class Details
    {
        string constr = @"Data Source=BIM06\\SQLEXPRESS;Initial Catalog=B627_Blog;Persist Security Info=True;MultipleActiveResultSets=True;User ID=sa;Password=123456";

        public void AddIndirme(Indirmeler obj)
        {
            string cmdText = string.Format("insert into Indirmeler values({0},{1},{2},{3},{4},{5})", obj.SNo, obj.DosyaAdi, obj.DosyaAciklama,
                obj.DosyaBoyutu,obj.Link,obj.EklemeTarihi);
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(cmdText, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteIndirme(int i)
        {
            string cmdText = string.Format("Delete from Indirmeler where SNo={0}", i);
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(cmdText, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void EditIndirme(Indirmeler obj)
        {
            string cmdText = string.Format("Update Indirmeler set DosyaAdi='{0}',DosyaAciklama='{1}',DosyaBoyutu='{2}',Link='{3}',EklemeTarihi='{4}' where SNo={5}",obj.DosyaAdi,
                obj.DosyaAciklama,obj.DosyaBoyutu,obj.Link,obj.EklemeTarihi);
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(cmdText, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


    }
}