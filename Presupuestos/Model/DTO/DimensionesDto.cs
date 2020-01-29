using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ListDimensionesDto
    {
        public int Id { get; set; }
        public string cuenta_contable { get; set; }
        public string nombre_cuenta { get; set; }
        public string nombre { get; set; }
        public double? factor { get; set; }
        public int grupoId { get; set; }
        public string estado { get; set; }
        public string agrupador { get; set; }
    }
}
