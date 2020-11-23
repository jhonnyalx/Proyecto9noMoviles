using Newtonsoft.Json;
using Proyecto9noMoviles.Constans;
using Proyecto9noMoviles.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Eliminar : ContentPage
    {
        public Usuario Usuario { get; set; }
        public Eliminar(Usuario usuario)
        {
            InitializeComponent();
            Usuario = usuario;
            lblNombreUsuario.Text = "Bienvenido: " + usuario.cli_nombre + " " + usuario.cli_apellido;
            listarReservas();
        }

        public async void listarReservas() {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    var parametros = new NameValueCollection();
                    parametros.Add("res_fecha", DateTime.Now.ToString("yyyy/MM/dd"));
                    parametros.Add("ProUsuario_usu_id", Usuario.usu_id.ToString());

                    var content = webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/reserva/select", WebRequestMethods.Http.Post, parametros);
                    var reserva = JsonConvert.DeserializeObject<List<Reservacion>>(Encoding.UTF8.GetString(content));
                    reservaListView.ItemsSource = new ObservableCollection<Reservacion>(reserva);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error has occurred: {ex.Message}", "Ok");

            }


        }

        private async void reservaListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null)
                return;

            var reserv = e.SelectedItem as Reservacion;
            reservaListView.SelectedItem = null;

            var action=await DisplayAlert("Eliminar", "Desea eliminar el registro seleccionado?", "Si", "No");
            if (action)
            {
                try
                {
                    using (WebClient webClient = new WebClient())
                    {

                        var parametrosCliente = new NameValueCollection();
                        parametrosCliente.Add("res_id", reserv.res_id.ToString());

                        var content = webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/reserva/delete", WebRequestMethods.Http.Post, parametrosCliente);
                        listarReservas();
                        await Navigation.PushAsync(new Opciones(Usuario));
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An unexpected error has occurred: {ex.Message}", "Ok");
                }
            }
            else {
                var a = "no";
            }
        }
    }
}