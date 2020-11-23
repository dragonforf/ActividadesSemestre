using ActividadesSemestre.Models;
using ActividadesSemestre.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CortesPage : ContentPage{
        public CortesPage(){
            InitializeComponent();
        }

        private void SeleccionarCorte(object sender, EventArgs e){
            Picker picker = (Picker)sender;
            int corte = picker.Title.ToLower().Contains("primer") ? 1 : (picker.Title.ToLower().Contains("segundo") ? 2 : 3);
            picker.SelectedIndex = 0;
            AbrirDetalleCorte(corte);
        }

        async private void AbrirDetalleCorte(int corteId){
            var cortes = App.ActividadesSemestreBD.GetCollection<BsonDocument>("Cortes");
            var corteSeleccionado = cortes.Find(Builders<BsonDocument>.Filter.Eq("Id", corteId)).FirstOrDefault();
            var actividades = App.ActividadesSemestreBD.GetCollection<BsonDocument>("Actividades").Find(Builders<BsonDocument>.Filter.Eq("CorteId", corteId)).ToList();
            var actividadesCorte=new ObservableCollection<Actividad>();

            foreach (BsonDocument actividad in actividades){
                actividadesCorte.Add(new Actividad{
                    Id = (int)actividad["Id"],
                    Titulo = actividad["Titulo"].ToString(),
                    Observaciones = actividad["Observaciones"].ToString(),
                    Nota = (double)actividad["Nota"],
                    FechaEntrega = (DateTime)actividad["FechaEntrega"],
                    Latitud = actividad["latitud"].ToString(),
                    Longitud = actividad["longitud"].ToString()
                });
            }

            Corte corte = new Corte{
                Id = (int)corteSeleccionado["Id"],
                Nombre = corteSeleccionado["Nombre"].ToString(),
                Porcentaje = (int)corteSeleccionado["Porcentaje"],
                FechaInicio = (DateTime)corteSeleccionado["FechaInicio"],
                FechaFin = (DateTime)corteSeleccionado["FechaFin"],
                ActividadesCorte = actividadesCorte
            };


            await Application.Current.MainPage.Navigation.PushAsync(new CortePage(new CorteViewModel{
                Nombre = corte.Nombre,
                Porcentaje = corte.Porcentaje,
                FechaInicio = corte.FechaInicio.ToString("D", new System.Globalization.CultureInfo("es-CO")),
                FechaFin = corte.FechaFin.ToString("D", new System.Globalization.CultureInfo("es-CO")),
                Actividades = corte.ActividadesCorte,
                CorteSeleccionado = corte
            }));
        }

        async public void VerForos(object sender, EventArgs e){
            await Navigation.PushAsync(new ForosPage(new ForosViewModel()));
        }

        async public void CerrarSesion(object sender, EventArgs e){
            App.UsuarioAutenticadoId = 0;
            await Navigation.PopAsync();
        }
    }
}