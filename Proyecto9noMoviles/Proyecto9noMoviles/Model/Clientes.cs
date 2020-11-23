using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto9noMoviles.Model
{
    public class Clientes
    {
        public int cli_id { get; set; }
        public string cli_nombre { get; set; }
        public string cli_apellido { get; set; }
        public string cli_dni { get; set; }
        public string cli_direccion { get; set; }
        public string cli_celular { get; set; }
        public string cli_telf_fijo { get; set; }
        public string cli_email { get; set; }
        public int cli_estado { get; set; }

    }
}
