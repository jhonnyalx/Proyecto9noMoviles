using Newtonsoft.Json;
using Proyecto9noMoviles.Constans;
using Proyecto9noMoviles.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto9noMoviles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cliente : ContentPage
    {
        public Cliente()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nombre.Text) &&
                !string.IsNullOrWhiteSpace(apellido.Text) &&
                (!string.IsNullOrWhiteSpace(cedula.Text) && cedula.Text.Trim().Length==10) &&
                !string.IsNullOrWhiteSpace(direccion.Text) &&
                !string.IsNullOrWhiteSpace(celular.Text) &&
                !string.IsNullOrWhiteSpace(telefono.Text) &&
                !string.IsNullOrWhiteSpace(correo.Text) &&
                !string.IsNullOrWhiteSpace(nombreUsuario.Text) &&
                !string.IsNullOrWhiteSpace(password.Text))
            {

                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        Clientes cliente = new Clientes();
                        cliente.cli_nombre = nombre.Text.Trim();
                        cliente.cli_apellido = apellido.Text.Trim();
                        cliente.cli_dni = cedula.Text.Trim();
                        cliente.cli_direccion = direccion.Text;
                        cliente.cli_celular = celular.Text.Trim();
                        cliente.cli_telf_fijo = telefono.Text.Trim();
                        cliente.cli_email = correo.Text.Trim();

                        var parametrosCliente = new NameValueCollection();
                        parametrosCliente.Add("cli_nombre", cliente.cli_nombre);
                        parametrosCliente.Add("cli_apellido", cliente.cli_apellido);
                        parametrosCliente.Add("cli_dni", cliente.cli_dni);
                        parametrosCliente.Add("cli_direccion", cliente.cli_direccion);
                        parametrosCliente.Add("cli_celular", cliente.cli_celular);
                        parametrosCliente.Add("cli_telf_fijo", cliente.cli_telf_fijo);
                        parametrosCliente.Add("cli_email", cliente.cli_email);
                        parametrosCliente.Add("cli_estado", "1");

                        var content = webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/cliente/insert", WebRequestMethods.Http.Post, parametrosCliente);
                        cliente.cli_id = Convert.ToInt32(Encoding.UTF8.GetString(content));


                        Usuario usuario = new Usuario();
                        usuario.usu_usuario = nombreUsuario.Text.Trim();
                        usuario.usu_password = password.Text.Trim();
                        usuario.ProCliente_cli_id = cliente.cli_id;

                        var parametrosUsuario = new NameValueCollection();
                        parametrosUsuario.Add("usu_usuario", usuario.usu_usuario);
                        parametrosUsuario.Add("usu_password", usuario.usu_password);
                        parametrosUsuario.Add("usu_estado", "1");
                        parametrosUsuario.Add("ProCliente_cli_id", usuario.ProCliente_cli_id.ToString());



                        var contentUsuario = webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/usuario/insert", WebRequestMethods.Http.Post, parametrosUsuario);
                        await DisplayAlert("Registro Exitoso", "Gracias por registrarte", "Ok");
                        await Navigation.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An unexpected error has occurred: {ex.Message}", "Ok");
                }

            }
            else 
            {
                await DisplayAlert("Alerta!", "Regitra informacion en todos los campos por favor!!!", "Ok");
            }

            
        }
    }
}