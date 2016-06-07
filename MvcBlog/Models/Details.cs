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
        string constr = @"Data Source=BIM06\SQLEXPRESS;Initial Catalog=B627_Blog;Persist Security Info=True;MultipleActiveResultSets=True;User ID=sa;Password=123456";

        public void AddIndirme(Indirmeler obj)
        {
            string cmdText = string.Format("insert into Indirmeler(DosyaAdi,DosyaAciklama,DosyaBoyutu,Link) values ('"+ obj.DosyaAdi + "',"+
               " '"+ obj.DosyaAciklama + "','" + obj.DosyaBoyutu + "','"+ obj.Link + "')");
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


        public void EditIndirme(Indirmeler obj,int id)
        {
            string cmdText = string.Format("Update Indirmeler set DosyaAdi='"+ obj.DosyaAdi + "',DosyaAciklama='" + obj.DosyaAciklama + "', "+
               " DosyaBoyutu='"+ obj.DosyaBoyutu + "',Link='"+ obj.Link + "' where SNo='"+id+"'");
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(cmdText, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }


    }
}