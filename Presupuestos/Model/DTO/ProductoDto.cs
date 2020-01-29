using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ProductoDto
    {
        public int productoId { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public int categoriaId { get; set; }
        public string categoria { get; set; }
        public string cultivoId { get; set; }
        public string cultivo { get; set; }
        public List<DetalleDto> detalle { get; set; }
    }

    public class DetalleDto
    {
        public int Id { get; set; }
        public int productoId { get; set; }
        public string presentacionId { get; set; }
    }
}
