﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Shared
{
    interface IAuditable
    {
        int? CreadoPor { get; set; }
        DateTime? CreadoFecha { get; set; }
        int? ActualizadoPor { get; set; }
        DateTime? ActualizadoFecha { get; set; }
    }
}
