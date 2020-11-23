using Proyecto9noMoviles.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto9noMoviles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    
    public partial class Opciones : ContentPage
    {
        public Usuario Usuario { get; set; }
        public Opciones(Usuario usuario)
        {
            InitializeComponent();
            Usuario = usuario;
            lblNombreUsuario.Text = "Bienvenido: " + usuario.cli_nombre + " " + usuario.cli_apellido;
        }

        private async void opcionCrear_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Centros(Usuario));
        }

        private async void opcionEliminar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Eliminar(Usuario));
        }
    }
}