using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ListPresupuestoDto
    {
        public int Id { get; set; }
        
        public string fincaId { get; set; }
        public string centro_costo { get; set; }
        public int tipoId { get; set; }
        public string tipo { get; set; }
        public int? año { get; set; }
        public int? mes { get; set; }
        public int? quincena { get; set; }
        public int? semana { get; set; }
        public decimal? total_estimado { get; set; }
        public DateTime? fecha { get; set; }
        public string estado { get; set; }
        
    }

    public class CalcularTotales
    {
        public decimal? total_lote { get; set; }
        public decimal? total_ccosto { get; set; }
        public decimal? total { get; set; }
    }

    public class ListTipoPresupuestoDto
    {
        public int Id { get; set; }
        public int? tipoId { get; set; }
        public string nombre { get; set; }
        public string icon { get; set; }
        public string clase { get; set; }
        public string url { get; set; }
        public int usuarioId { get; set; }
    }

    public class CreateDetalleManoObraDto
    {
        public int Id { get; set; }

        public int presupuestoId { get; set; }
        public int fincaId { get; set; }
        public string referencia { get; set; }
        public string referencia_producto { get; set; }
        public int? cantidad { get; set; }
        public int loteId { get; set; }
        public decimal? total_lote { get; set; }
        public decimal? total_referencia { get; set; }
        public decimal? total_ccosto { get; set; }
        public decimal? total { get; set; }
        public string estado { get; set; }
        public int unidad_by_p_cs { get; set; }
        public decimal? costo_by_p_cs{ get; set; }
    }

    public class ListDetallDto
    {
        public int id { get; set; }
        public int fincaId { get; set; }
        public int presupuestoId { get; set; }
        public string ccosto { get; set; }
        public string referencia { get; set; }
        public string nombre_referencia { get; set; }
        public decimal? costo { get; set; }
        public int loteId { get; set; }
        public string lote { get; set; }
        public int? cantidad { get; set; }
        public decimal? total_referencia { get; set; }
        public decimal? total { get; set; }
        public decimal? total_lote { get; set; }
        public string unidad_medida { get; set; }
        public string estado { get; set; }
        public string categoria { get; set; }
        public string grupo { get; set; }
        public string nombre_categoria { get; set; }
        public string nombre_grupo { get; set; }
        public string tipo { get; set; }
        public string cultivo { get; set; }
        public int unidad_by_p_cs { get; set; }
        public decimal? costo_by_p_cs { get; set; }
    }

    public class ListDimensionActividad
    {
        public int dimensionId { get; set; }
        public int grupoId { get; set; }
        public int detalleId { get; set; }
        public double? factor { get; set; }
    }
    
}
