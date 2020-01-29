using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class GraficasListDto
    {
        public string tipo { get; set; }
        public List<Labels> labels { get; set; }
        public List<data> data { get; set; }
    }

    public class data
    {
        public int semana { get; set; }
        public int año { get; set; }
        public decimal? total { get; set; }
    }

    public class Labels
    {
        public string label { get; set; }
    }
}
