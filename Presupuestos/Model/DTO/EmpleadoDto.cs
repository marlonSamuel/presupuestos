using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class EmpleadoDto
    {
        public int empleadoId { get; set; }
        public string codigo { get; set; }
        public string fincaId { get; set; }
        public string centro_costo { get; set;}
        public int tipoId { get; set; }
        public string tipo { get; set; }
        public int departamentoId { get; set; }
        public string departamento { get; set; }
        public int cargoId { get; set; }
        public string cargo { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string dpi { get; set; }
        public string telefono { get; set; }
        public decimal? salario { get; set; }
        public string foto { get; set; }
        public int? tipoPila { get; set; }
        public int? codPila { get; set; }
        public string estado { get; set; }
    }
}
