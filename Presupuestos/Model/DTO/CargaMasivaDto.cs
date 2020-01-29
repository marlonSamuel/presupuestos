using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CargaMasivaCreateDto
    {
        public int Id { get; set; }
        public int presupuestoId { get; set; }
        public string codigoTipo { get; set; }
        public List<CargaMasivaDto> detalle { get; set; }
    }

    public class CargaMasivaDto
    {
        public int fincaId { get; set; }
        public string referencia { get; set; }
        public int cantidad { get; set; }
        public decimal? total_referencia { get; set; }
        public int subloteId { get; set; }
        public int unidad_by_p_cs { get; set; }
        public decimal? costo_by_p_cs { get; set; }
    }

    public class CargaMasivaCt
    {
        public int Id { get; set; }
        public int presupuestoId { get; set; }
        public string codigoTipo { get; set; }
        public List<ListCargaMasivaCtDto> detalle { get; set; }
    }

    public class ListCargaMasivaCtDto
    {
        public int? subcentroId { get; set; }
        public int? loteId { get; set; }
        public string codigo_cuenta { get; set; }
        public string cuenta_contable { get; set; }
        public decimal? enero { get; set; }
        public decimal? febrero { get; set; }
        public decimal? marzo { get; set; }
        public decimal? abril { get; set; }
        public decimal? mayo { get; set; }
        public decimal? junio { get; set; }
        public decimal? julio { get; set; }
        public decimal? agosto { get; set; }
        public decimal? septiembre { get; set; }
        public decimal? octubre { get; set; }
        public decimal? noviembre { get; set; }
        public decimal? diciembre { get; set; }
        public decimal? total { get; set; }
        public string detalle { get; set; }
    }
}
