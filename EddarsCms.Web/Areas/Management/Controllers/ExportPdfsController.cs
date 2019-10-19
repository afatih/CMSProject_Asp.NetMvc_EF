using EddarsCms.BLL.IServices;
using EddarsCms.BLL.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EddarsCms.Web.Areas.Management.Controllers
{
    public class ExportPdfsController : Controller
    {

        IInformationFromUsService infoServ;

        public ExportPdfsController()
        {
            infoServ = new InformationFromUsService();
        }


        public void InformationFromUs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Sıra No", typeof(string));
            dt.Columns.Add("Mail", typeof(string));
            dt.Columns.Add("Tarih", typeof(string));

            var infos = infoServ.GetAll().Result;
            var count = 1;
            if (infos!=null)
            {
                if (infos.Count>0)
                {
                    foreach (var item in infos)
                    {
                        dt.Rows.Add(count.ToString(), item.Mail,item.Date);
                        count++;
                    }
                }
            }


            Response.Clear();
            string fileName = string.Format("{0}{1}.xls", "Haberder_Olun_",
               DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString());

            Response.AddHeader("Content-disposition", "attachment; filename=" + fileName + "");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = Encoding.Unicode;
            Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    GridView grid = new GridView { DataSource = dt };
                    grid.DataBind();
                    grid.RenderControl(htw);
                    Response.Write(sw.ToString());
                }
            }
            Response.End();
        }
    }
}