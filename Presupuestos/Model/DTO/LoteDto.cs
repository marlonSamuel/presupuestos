using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CreateSubLoteDto
    {
        public int Id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public int loteId { get; set; }
        public string descripcion { get; set; }
        public decimal? metros { get; set; }
        public decimal? ancho { get; set; }
        public decimal? altura { get; set; }
        public decimal? densidad { get; set; }
        public string cultivoId { get; set; }
        public string variedadId { get; set; }
        public decimal? factor_manzana { get; set; }
        public string techado { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }
        public string region { get; set; }
       //public List<string> variedades { get; set; }
    }

    public class ListLoteDto
    {
        public int Id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string fincaId { get; set; }
        public string centroCosto { get; set; }
        public string descripcion { get; set; }
        public decimal? metros { get; set; }
        public decimal? ancho { get; set; }
        public decimal? altura { get; set; }
        public decimal? densidad { get; set; }
        public string estado { get; set; }
        public string lote { get; set; }
        public int loteId { get; set; }
        public string techado { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }
        public decimal? factor_manzana { get; set; }
        public string cultivog { get; set; }
        public string variedad { get; set; }
        public string cultivo { get; set; }
    }

    public class DetalleLoteDto
    {
        public int detalleId { get; set; }
        public string cultivoId { get; set; }
        public string variedadId { get; set; }
    }

    public class ListActividadDto
    {
        public int Id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal? costo { get; set; }
        public int unidadId { get; set; }
        public string unidadMedida { get; set; }
        public string Descripcion { get; set; }
        public string estado { get; set; }
        public int grupoId { get; set; }
        public string grupo { get; set; }

    }

    public class ListActividadFincaDto
    {
        public int detalleId { get; set; }
        public int fincaId { get; set; }
        public int? actividadId { get; set; }
        public string nombre { get; set; }
        public decimal? costo { get; set; }
        public string unidad_medida { get; set; }
        public string codigo { get; set; }
    }
}
