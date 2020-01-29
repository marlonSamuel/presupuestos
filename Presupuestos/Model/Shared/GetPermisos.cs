using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Shared
{
    public class GetPermisos
    {
        public string escritorio { get; set; }
        public string almacen { get; set; }
        public string fincas { get; set; }
        public string mano_obra { get; set; }
        public string agricola { get; set; }
        public string costos_y_gastos { get; set; }
        public string ventas { get; set; }
        public string cosecha { get; set; }
        public string acceso { get; set; }
        public string dimensiones { get; set; }
        public string paises { get; set; }


        private readonly SPUsuario _usuario = new SPUsuario();

        public GetPermisos GetAllPerms(int usuarioId)
        {
            var permisos = new GetPermisos();

            var list = _usuario.GetPermisos(usuarioId);

            if(list.Count > 0)
            {
                foreach(var p in list)
                {
                    //seteamos el codigo para escritorio
                    if(p.codigo_permiso == "P_ESC") permisos.escritorio = p.codigo_permiso;
                    //seteamos el codigo para almacen
                    if(p.codigo_permiso == "P_ALM") permisos.almacen = p.codigo_permiso;
                    //seteamos el codigo para finca
                    if(p.codigo_permiso == "P_FCA") permisos.fincas = p.codigo_permiso;
                    //seteamos el codigo para presupuesto mano de obra
                    if(p.codigo_permiso == "P_MOB") permisos.mano_obra = p.codigo_permiso;
                    //seteamos el codigo para presukpuesto agricola
                    if(p.codigo_permiso == "P_AGR") permisos.agricola = p.codigo_permiso;
                    //seteamos el codigo para presupuesto contabilidad
                    if(p.codigo_permiso == "P_CTA") permisos.costos_y_gastos = p.codigo_permiso;
                    //seteamos el codigo para presupuesto ventas
                    if(p.codigo_permiso == "P_VTA") permisos.ventas = p.codigo_permiso;
                    //seteamos el codigo para presupuesto cosecha
                    if(p.codigo_permiso == "P_CCA") permisos.cosecha = p.codigo_permiso;
                    //Seteamos el codigo para permisos
                    if(p.codigo_permiso == "P_ACS") permisos.acceso = p.codigo_permiso;
                    //seteamos el codigo para dimensiones
                    if (p.codigo_permiso == "P_DIM") permisos.dimensiones = p.codigo_permiso;
                    //Seteamos el codigo para paises
                    if (p.codigo_permiso == "P_PAIS") permisos.paises = p.codigo_permiso;
                }
            }

            return permisos;
        }
    }
}
