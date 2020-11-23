using Newtonsoft.Json;
using Proyecto9noMoviles.Constans;
using Proyecto9noMoviles.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Proyecto9noMoviles
{
    public partial class MainPage : ContentPage
    {
        public Usuario Usuario { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }

        private async  void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    var parametros = new NameValueCollection();
                    parametros.Add("usu_usuario", txtUsuario.Text);
                    parametros.Add("usu_password", txtPassword.Text);

                    var content=  webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/usuario/selectUsuario", WebRequestMethods.Http.Post, parametros);
                    var usuario = JsonConvert.DeserializeObject<List<Usuario>>(Encoding.UTF8.GetString(content));
                    if (usuario.Count() > 0)
                    {
                        await Navigation.PushAsync(new Opciones(usuario.First()));
                    }
                    else 
                    {
                        await DisplayAlert("Error", "EL usuario no se encuentra registrado","ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error has occurred: {ex.Message}", "Ok");
            }

        }

        private async  void btnCliente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cliente());
        }
    }
}
