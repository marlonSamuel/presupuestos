using Model;
using Model.BI;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presupuesto.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        private readonly SPPresupuesto presupuesto = new SPPresupuesto();
        private readonly Reportes reporte = new Reportes();
        public ActionResult Index()
        {
            return View();
        }

        public void ReporteAgricola(int presupuestoId)
        {
            var header = reporte.GetHeader(presupuestoId);
            var data = reporte.GetAgriculaGroup(presupuestoId);

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte Agricola");

            ws.Cells["A1"].Value = header.nombre_presupuesto;
            ws.Cells["B1"].Value = "semana "+header.semana + " del "+header.año;

            ws.Cells["A2"].Value = "total: "+header.total;
            ws.Cells["B2"].Value = "Semana uno";

            ws.Cells["A3"].Value = "Fecha de Impresion";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Nombre";
            ws.Cells["B6"].Value = "Total";

            int rowStart = 7;


            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public void ExcelPresupuesto()
        {
            var presupuestos = new List<SPPresupuesto>();

            for (int i = 0; i < 5; i++)
            {
                presupuestos = new List<SPPresupuesto>();
                var p = new SPPresupuesto
                {
                    semana = i,
                    año = 2018,
                    paisId = 3,
                    total_estimado = 2000 + i
                };

                presupuestos.Add(p);
            }

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte");

            ws.Cells["A1"].Value = "Presupuestos";
            ws.Cells["B1"].Value = "Semana 01";

            ws.Cells["A2"].Value = "Presupuestos";
            ws.Cells["B2"].Value = "Semana uno";

            ws.Cells["A3"].Value = "Fecha de Imprimesion";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Semana";
            ws.Cells["B6"].Value = "Año";
            ws.Cells["C6"].Value = "Pais";
            ws.Cells["D6"].Value = "Total";

            int rowStart = 7;
            foreach (var item in presupuestos)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.semana;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.año;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.paisId;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.total_estimado;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
    }

}