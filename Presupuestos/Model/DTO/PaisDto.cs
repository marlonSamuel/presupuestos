using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ListPaisDto
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public int monedaId { get; set; }
        public string moneda { get; set; }
        public string simbolo_moneda { get; set; }
        public string estado { get; set; }
    }

    public class ListTipoCambioDto
    {
        public int Id { get; set; }
        public DateTime? fecha { get; set; }
        public int paisId { get; set; }
        public string pais { get; set; }
        public string moneda { get; set; }
        public decimal? tipo_cambio { get; set; }
    }
}
