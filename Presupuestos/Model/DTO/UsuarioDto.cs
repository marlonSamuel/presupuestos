using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CreateUsuarioDto
    {
        public int Id { get; set; }
        public string usuario { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string estado { get; set; }
        public List<int> permisos { get; set; }
        public List<int> paises { get; set; }
        public List<int> presupuestos { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public int? usuarioId { get; set; }
        public string codigo_permiso { get; set; }
        public int? permisoId { get; set; }
    }

    public class UserPais
    {
        public int Id { get; set; }
        public int usuarioId { get; set; }
        public string pais { get; set; }
        public int paisId { get; set; }
    }
}
