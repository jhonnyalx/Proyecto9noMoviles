using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto9noMoviles.Model
{
    public class Instalaciones
    {
        public int ins_id { get; set; }
        public string ins_descripcion { get; set; }
        public int  ins_estado { get; set; }
        public float ins_precio { get; set; }
        public string ins_url { get; set; }
        public int ProCentroActivo_cen_id { get; set; }
        public int ins_horaInicio { get; set; }
        public int ins_horaFin { get; set; }
    }
}
