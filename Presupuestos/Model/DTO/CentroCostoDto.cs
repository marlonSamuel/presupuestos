using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CentroCostoDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string Correlativo { get; set; }
        public string Region { get; set; }
        public string CodigoNAV { get; set; }
        public string CorrelativoTD { get; set; }
        public string estado { get; set; }
        public int paisId { get; set; }
    }

    public class CreateSubcentroDto
    {
        public int Id { get; set; }
        public string codigo { get; set; }
        public string fincaId { get; set; }
        public string nombre { get; set; }
        public string codigo_nav { get; set; }
        public List<int> actividades { get; set; }
    }

    public class ListSubCentroDto
    {
        public int Id { get; set; }
        public string codigo { get; set; }
        public string fincaId { get; set; }
        public string nombre { get; set; }
        public string codigo_nav { get; set; }
        public string tipo_valor { get; set; }
        public string estado { get; set; }
    }
}
