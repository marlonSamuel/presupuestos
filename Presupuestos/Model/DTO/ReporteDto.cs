using Model.BI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ReporteAgricolaDto
    {
        public string nombre_categoria { get; set; }
        public decimal? total_categoria { get; set; }
        public List<SP_GroupByGrupo> grupos { get; set; }
        public decimal? total { get; set; }
    }

    public class reporteHeader
    {
        public string nombre_presupuesto { get; set; }
        public int? semana { get; set; }
        public int? mes { get; set; }
        public int? año { get; set; }
        public decimal? total { get; set; }
    }
}
