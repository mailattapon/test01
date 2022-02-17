using Aspose.Cells;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using test01.Models;

namespace test01.Controllers
{
    public class HomeController : Controller
    {
        private object excelPackage;

        public ActionResult Index()
        {
            //if (Request.Cookies["count"] != null)
            //{
            //    int count = 1;
            //    while (count <= Convert.ToInt32(Request.Cookies["count"]["num"].ToString()))
            //    {

            //        HttpCookie delCookie1 = new HttpCookie(count.ToString());
            //        delCookie1.Expires = DateTime.Now.AddDays(-1D);
            //        Response.Cookies.Add(delCookie1);

            //        count++;
            //    }

            //    HttpCookie delCookie = new HttpCookie("count");
            //    delCookie.Expires = DateTime.Now.AddDays(-1D);
            //    Response.Cookies.Add(delCookie);
            //}


            return View();
        }

        public ActionResult clickAdd( string data)
        {
			HttpCookie newCookie = new HttpCookie("count");
			if (Request.Cookies["count"] == null)
			{
				newCookie["num"] = "1";
			}
			else
			{
				newCookie["num"] = (Convert.ToInt32(Request.Cookies["count"]["num"].ToString()) + 1).ToString();
			}
			newCookie.Expires = DateTime.Now.AddDays(7);
			Response.Cookies.Add(newCookie);


			HttpCookie newCookie2 = new HttpCookie(Request.Cookies["count"]["num"].ToString());
			newCookie2["id"] = Request.Cookies["count"]["num"];
			newCookie2["input"] = data;
			newCookie2["time"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			newCookie2["status"] = "false";
			newCookie2.Expires = DateTime.Now.AddDays(7);
			Response.Cookies.Add(newCookie2);

			return Json("", JsonRequestBehavior.AllowGet);

		}

		public ActionResult getData()
        {

			int count = 0;
			List<dataSet> list = new List<dataSet>();

			if (Request.Cookies["count"] != null)
			{
				count = Convert.ToInt32(Request.Cookies["count"]["num"].ToString()) - 1;
				while (count >= 1)
				{

					if (Request.Cookies[count.ToString()] != null)
					{
						list.Add(new dataSet()
						{
							id = Request.Cookies[count.ToString()]["id"].ToString(),
							data = Request.Cookies[count.ToString()]["input"].ToString(),
							time = Request.Cookies[count.ToString()]["time"].ToString(),
							status = Request.Cookies[count.ToString()]["status"].ToString()
						});
					}
					count--;
				}
			}

			return Json(list, JsonRequestBehavior.AllowGet);
		}

		public ActionResult clickComplete(string id)
		{

			HttpCookie cookie = Request.Cookies[id];

			cookie.Values["status"] = "true";
			cookie.Expires = DateTime.UtcNow.AddDays(7);
			Response.Cookies.Add(cookie);

			return Json("", JsonRequestBehavior.AllowGet);
		}

		public ActionResult clickDelete(string id)
		{

            if (Request.Cookies["count"] != null)
            {
                HttpCookie delCookie = new HttpCookie(id);
                delCookie.Expires = DateTime.Now.AddDays(-1D);
                Response.Cookies.Add(delCookie);
            }

			return Json("", JsonRequestBehavior.AllowGet);
		}

		public ActionResult clickClear()
		{

            if (Request.Cookies["count"] != null)
            {
                int count = 1;
                while (count <= Convert.ToInt32(Request.Cookies["count"]["num"].ToString()))
                {

                    HttpCookie delCookie1 = new HttpCookie(count.ToString());
                    delCookie1.Expires = DateTime.Now.AddDays(-1D);
                    Response.Cookies.Add(delCookie1);

                    count++;
                }

                HttpCookie delCookie = new HttpCookie("count");
                delCookie.Expires = DateTime.Now.AddDays(-1D);
                Response.Cookies.Add(delCookie);
            }

            return Json("data", JsonRequestBehavior.AllowGet);
		}

		public void printExcel()
		{

			//EXCEL
			ExcelPackage excelPackage = new ExcelPackage();
			excelPackage.Workbook.Properties.Title = "Title of Document";
			ExcelWorksheet ExcelSheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
			var format = new ExcelTextFormat();
			format.Delimiter = '|';

			ExcelSheet.Cells[1, 1].Value = "Id";
			ExcelSheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 2].Value = "Data";
			ExcelSheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 3].Value = "Date Time";
			ExcelSheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
			ExcelSheet.Cells[1, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

			int count = 0;
			int countExcel = 2;
			if (Request.Cookies["count"] != null)
			{
				count = Convert.ToInt32(Request.Cookies["count"]["num"].ToString()) - 1;
				while (count >= 1)
				{

					if (Request.Cookies[count.ToString()] != null)
					{
						ExcelSheet.Cells[countExcel, 1].Value = Request.Cookies[count.ToString()]["id"].ToString();
						ExcelSheet.Cells[countExcel, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 2].Value = Request.Cookies[count.ToString()]["input"].ToString();
						ExcelSheet.Cells[countExcel, 2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 3].Value = Request.Cookies[count.ToString()]["time"].ToString();
						ExcelSheet.Cells[countExcel, 3].Style.Border.Top.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 3].Style.Border.Right.Style = ExcelBorderStyle.Thin;
						ExcelSheet.Cells[countExcel, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
						countExcel = countExcel + 1;
					}
					count--;
					
				}
			}
			
			Response.Clear();
			Response.ContentType = "application/csv";
			Response.AddHeader("Content-Disposition", "attachment;filename=test" + ".csv");
			Response.BinaryWrite(excelPackage.GetAsByteArray());

			Response.End();
		}

		public ActionResult clickImport()
        {
			var attachedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];
			if (attachedFile == null || attachedFile.ContentLength <= 0) return Json(null);
			using (var package = new ExcelPackage(attachedFile.InputStream))
			{
				var workbook = package.Workbook;
				var worksheet = workbook.Worksheets.First();
				DataTable dt = ConvertToDataTable(worksheet);
				int count = 1;
				foreach(DataRow dr in dt.Rows)
                {

					HttpCookie newCookie = new HttpCookie("count");
					if (Request.Cookies["count"] == null)
					{
						newCookie["num"] = "1";
					}
					else
					{
						newCookie["num"] = (Convert.ToInt32(Request.Cookies["count"]["num"].ToString()) + count).ToString();
					}
					newCookie.Expires = DateTime.Now.AddDays(7);
					Response.Cookies.Add(newCookie);



					HttpCookie newCookie2 = new HttpCookie((Convert.ToInt32(Request.Cookies["count"]["num"].ToString()) + count - 1).ToString());
					newCookie2["id"] = (Convert.ToInt32(Request.Cookies["count"]["num"].ToString()) + count - 1).ToString();
					newCookie2["input"] = dr["data"].ToString();
					newCookie2["time"] = dr["date time"].ToString();
					newCookie2["status"] = "false";
					newCookie2.Expires = DateTime.Now.AddDays(7);
					Response.Cookies.Add(newCookie2);

					count++;
				}
			}

			return  Json("data", JsonRequestBehavior.AllowGet); ;
		}

		private DataTable ConvertToDataTable(ExcelWorksheet oSheet)
		{
			int totalRows = oSheet.Dimension.End.Row;
			int totalCols = oSheet.Dimension.End.Column;
			DataTable dt = new DataTable(oSheet.Name);
			DataRow dr = null;
			for (int i = 1; i <= totalRows; i++)
			{
				if (i > 1) dr = dt.Rows.Add();
				for (int j = 1; j <= totalCols; j++)
				{
					if (i == 1)
						dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());
					else
						dr[j - 1] = oSheet.Cells[i, j].Value.ToString();
				}
			}
			return dt;
		}
	}
}