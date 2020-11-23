using Newtonsoft.Json;
using Proyecto9noMoviles.Constans;
using Proyecto9noMoviles.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto9noMoviles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Instalacion : ContentPage
    {
        public Usuario Usuario { get; set; }
        public Centro Centro { get; set; }
        public Instalacion(Usuario usuario, Centro centro)
        {
            InitializeComponent();
            Usuario = usuario;
            Centro = centro;
            lblNombreUsuario.Text = "Bienvenido: " + usuario.cli_nombre + " " + usuario.cli_apellido;
            lblCentro.Text = centro.cen_descripcion;

            obtenerInstalaciones(centro.cen_id);
        }


        private async void obtenerInstalaciones(int id)
        {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    var parametros = new NameValueCollection();
                    parametros.Add("ProCentroActivo_cen_id", id.ToString());

                    var content = webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/instalaciones/select", WebRequestMethods.Http.Post, parametros);
                    var instalacion = JsonConvert.DeserializeObject<List<Instalaciones>>(Encoding.UTF8.GetString(content));
                    instalacionListView.ItemsSource = new ObservableCollection<Instalaciones>(instalacion);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error has occurred: {ex.Message}", "Ok");
            }
        }

        private async  void instalacionListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var instalacion = e.SelectedItem as Instalaciones;
            instalacionListView.SelectedItem = null;
            await Navigation.PushAsync(new Reservas(Usuario, instalacion, Centro));
        }
    }


}