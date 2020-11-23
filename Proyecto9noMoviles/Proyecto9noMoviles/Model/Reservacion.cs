using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto9noMoviles.Model
{
    public class Reservacion
    {
    public int res_id { get; set; }
    public int res_horaInicio { get; set; }
    public int res_horaFin { get; set; }
    public int ProInstalaciones_ins_id { get; set; }
    public int ProUsuario_usu_id { get; set; }
    public DateTime res_fecha { get; set; }

    public int cen_id { get; set; }
    public string cen_descripcion { get; set; }
    public string cen_direccion { get; set; }

   public int ins_id { get; set; }
   public string ins_descripcion { get; set; }
   public int ins_estado { get; set; }
   public float ins_precio { get; set; }
   public string ins_url { get; set; }
   public int ProCentroActivo_cen_id { get; set; }
   public int ins_horaInicio { get; set; }
   public int ins_horaFin { get; set; }
   public string ins_mensajeFinal { get; set; }

    }
}
