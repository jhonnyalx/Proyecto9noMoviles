using System;
using System.Collections.Generic;
using System.Text;

namespace Proyecto9noMoviles.Model
{
    public class Usuario
    {
    public int usu_id{ get; set; }
    public string usu_usuario { get; set; }
    public string usu_password { get; set; }
    public int usu_estado { get; set; }
    public int ProCliente_cli_id { get; set; }
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
