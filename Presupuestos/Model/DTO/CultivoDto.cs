using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ListCultivoDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string grupo { get; set; }
        public string codigoGrupo { get; set; }
        public string codigoNav { get; set; }
        public string factorCrecimiento { get; set; }
        public int activo { get; set; }
    }

    public class ListVariedadesDto
    {
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public string grupo { get; set; }
        public string codigoGrupo { get; set; }
    }

    public class ListDetalleLoteDto
    {
        public string Codigo { get; set; }
        public string cultivoId { get; set; }
        public int loteId { get; set; }
        public int detalleId { get; set; }
    }
}
