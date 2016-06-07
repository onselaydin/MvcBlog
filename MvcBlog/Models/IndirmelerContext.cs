using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcBlog.Models
{
    public class IndirmelerContext
    {
        string constr = @"Data Source = BIM06\SQLEXPRESS;Initial Catalog = B627_Blog; Persist Security Info=True;MultipleActiveResultSets=True;User ID = sa; Password=123456";

        public List<Indirmeler> GetIndirmeler()
        {
            string cmdText = "Select * from Indirmeler";
            SqlDataAdapter da = new SqlDataAdapter(cmdText, constr);
            DataSet ds = new DataSet();
            da.Fill(ds);
            List<Indirmeler> DosyaList = new List<Indirmeler>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Indirmeler objIndirmeler = new Indirmeler();
                objIndirmeler.SNo = Convert.ToInt32(item["SNo"].ToString());
                objIndirmeler.DosyaAdi = item["DosyaAdi"].ToString();
                objIndirmeler.DosyaAciklama = item["DosyaAciklama"].ToString();
                objIndirmeler.DosyaBoyutu = item["DosyaBoyutu"].ToString();
                objIndirmeler.Link = item["Link"].ToString();
                objIndirmeler.EklemeTarihi = DateTime.Parse(item["EklemeTarihi"].ToString());
                DosyaList.Add(objIndirmeler);
            }
            return DosyaList;

        }

        public Indirmeler GetDosya(int i)
        {
            string cmdText = "select * from Indirmeler where SNo="+i;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(cmdText, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            Indirmeler objIndirmeler = new Indirmeler();
            if(dr.Read()==true)
            {
                objIndirmeler.SNo = int.Parse(dr["SNo"].ToString());
                objIndirmeler.DosyaAdi = dr["DosyaAdi"].ToString();
                objIndirmeler.DosyaAciklama = dr["DosyaAciklama"].ToString();
                objIndirmeler.DosyaBoyutu = dr["DosyaBoyutu"].ToString();
                objIndirmeler.Link = dr["Link"].ToString();
                objIndirmeler.EklemeTarihi = DateTime.Parse(dr["EklemeTarihi"].ToString());
            }
            con.Close();
            return objIndirmeler;
        }


    }
}