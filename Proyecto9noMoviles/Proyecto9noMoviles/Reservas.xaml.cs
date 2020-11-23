
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
    public partial class Reservas : ContentPage
    {
        public Usuario Usuario { get; set; }
        public Centro Centro { get; set; }
        public Instalaciones Instalaciones { get; set; }
        public DateTime PropertyMinimumDate { get; set; }
        public DateTime PropertyMaximumDate { get; set; }
        public Reservas(Usuario usuario, Instalaciones instalaciones, Centro centro )
        {
            InitializeComponent();
            Usuario = usuario;
            Centro = centro;
            Instalaciones = instalaciones;
            imgEstadio.Source = instalaciones.ins_url;
            lblNombreUsuario.Text = "Bienvenidos: " + usuario.cli_nombre + " " + usuario.cli_apellido;
            lblCentro.Text = instalaciones.ins_descripcion + " Horario de atencion: " + instalaciones.ins_horaInicio + ":00-" + instalaciones.ins_horaFin+":00";

            PropertyMinimumDate = DateTime.Now;
            PropertyMaximumDate = DateTime.Now.AddDays(20);
        }

        private async void btnFecha_Clicked(object sender, EventArgs e)
        {
            

            if (!string.IsNullOrWhiteSpace(txtHoraI.Text) &&
               !string.IsNullOrWhiteSpace(txtHoraF.Text))
            {

                Reservacion r = new Reservacion();
                r.res_fecha = txtFecha.Date;
                r.res_horaInicio = Convert.ToInt32(txtHoraI.Text);
                r.res_horaFin = Convert.ToInt32(txtHoraF.Text);
                r.ProUsuario_usu_id = Usuario.usu_id;
                r.ProInstalaciones_ins_id = Instalaciones.ins_id;


                if (r.res_fecha.Day >= DateTime.Now.Day && r.res_fecha <= DateTime.Now.AddDays(30))
                {
                    if (r.res_horaInicio < r.res_horaFin)
                    {
                        if (r.res_horaInicio >= Instalaciones.ins_horaInicio & r.res_horaFin <= Instalaciones.ins_horaFin)
                        {
                            verificarExistencia(r);
                        }
                        else
                        {
                            await DisplayAlert("Alerta!", "Las horas indicadas no estan disponibles", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alerta!", "La hora fin es menor a la hora de inicio", "Ok");

                    }
                }
                else
                {
                    await DisplayAlert("Alerta!", "La Fecha indicada no esta disponible", "Ok");
                }

            }
            else {
                await DisplayAlert("Alerta!", "Por favor ingresa informacion", "Ok");
            }



            
            
        }

        public async  void  verificarExistencia(Reservacion r) {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    var parametros = new NameValueCollection();
                    parametros.Add("res_horaInicio", r.res_horaInicio.ToString());
                    parametros.Add("res_horaFin", r.res_horaFin.ToString());
                    parametros.Add("res_fecha", r.res_fecha.ToString("yyyy/MM/dd"));
                    parametros.Add("ProInstalaciones_ins_id", r.ProInstalaciones_ins_id.ToString());
                    parametros.Add("ProUsuario_usu_id", r.ProUsuario_usu_id.ToString());

                    var content = webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/reserva/selectDisponible", WebRequestMethods.Http.Post, parametros);
                    var usuario = JsonConvert.DeserializeObject<List<Usuario>>(Encoding.UTF8.GetString(content));
                    if (usuario.Count() > 0)
                    {
                        await DisplayAlert("No disponible","Las instalaciones para "+Instalaciones.ins_descripcion+" no estan disponibles", "Ok");
                    }
                    else
                    {
                        var contentReserva = webClient.UploadValues(Global.URL_SERVICE_REST + "/moviles/reserva/insert", WebRequestMethods.Http.Post, parametros);
                        r.res_id = Convert.ToInt32(Encoding.UTF8.GetString(contentReserva));
                        await DisplayAlert("Reservada", "Estimad@ "+ Usuario.cli_nombre+" se ha reservado un espacio deportivo de "+ Instalaciones.ins_descripcion + " en "+ Centro.cen_descripcion+" direccion "+
                        Centro.cen_direccion + " el dia:" + r.res_fecha.ToString("yyyy/MM/dd") + " de " + r.res_horaInicio + ":00 a " + r.res_horaFin + ":00", "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An unexpected error has occurred: {ex.Message}", "Ok");
                
            }

        }
    }
}