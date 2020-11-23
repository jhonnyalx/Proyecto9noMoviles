using Newtonsoft.Json;
using Proyecto9noMoviles.Constans;
using Proyecto9noMoviles.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto9noMoviles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Centros : ContentPage
    {
        public Usuario Usuario { get; set; }
        public Centros(Usuario usuario)
        {
            InitializeComponent();
            Usuario = usuario;
            lblNombreUsuario.Text = "Bienvenido: " + usuario.cli_nombre + " " + usuario.cli_apellido;

            cargarCentros();
        }


        public async void cargarCentros() {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    var content = await client.GetStringAsync(Global.URL_SERVICE_REST+"/moviles/centro/select");
                    var centro = JsonConvert.DeserializeObject<List<Centro>>(content);
                    centroListView.ItemsSource = new ObservableCollection<Centro>(centro);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error has occurred: {ex.Message}", "Ok");
            }
        }

        private async void centroListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var centro = e.SelectedItem as Centro;
            centroListView.SelectedItem = null;
            await Navigation.PushAsync(new Instalacion(Usuario, centro ));
        }
    }
}