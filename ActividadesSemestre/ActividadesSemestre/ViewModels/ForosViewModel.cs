using ActividadesSemestre.Models;
using ActividadesSemestre.Views;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace ActividadesSemestre.ViewModels{
    public class ForosViewModel : INotifyPropertyChanged{
        public ForosViewModel(){
            Foros = new ObservableCollection<Foro>();
            var forosCollection = App.ActividadesSemestreBD.GetCollection<BsonDocument>("Foros");
            var mensajesCollection = App.ActividadesSemestreBD.GetCollection<BsonDocument>("MensajesForo");

            foreach (BsonDocument foro in forosCollection.Find(new BsonDocument()).ToList()){
                int idForo = (int)foro["IdForo"];
                var mensajes = mensajesCollection.Find(Builders<BsonDocument>.Filter.Eq("IdForo", idForo)).ToList();
                var mensajesForo = new ObservableCollection<Mensaje>();
                foreach (BsonDocument mensaje in mensajes){
                    int idUsuario = (int)mensaje["IdUsuario"];
                    mensajesForo.Add(new Mensaje{
                        IdForo = idForo,
                        IdUsuario = idUsuario,
                        Fecha = (DateTime)mensaje["Fecha"],
                        Texto = mensaje["Mensaje"].ToString(),
                        NombreUsuario = App.ObtenerRemitente(idUsuario)
                    });
                }
                Foros.Add(new Foro{
                    IdForo = idForo,
                    NombreForo = foro["NombreForo"].ToString(),
                    Mensajes = mensajesForo
                });
            }

            AbrirMensajesForo = new Command(async () =>{
                if (ForoSeleccionado is null)
                    return;

                ForoViewModel foroViewModel = new ForoViewModel{
                    IdForo = ForoSeleccionado.IdForo,
                    NombreForo = ForoSeleccionado.NombreForo,
                    IdUsuario = App.UsuarioAutenticadoId,
                    Mensajes = Foros.Where(x => x.IdForo == ForoSeleccionado.IdForo).FirstOrDefault().Mensajes
                };

                await Application.Current.MainPage.Navigation.PushAsync(new ForoPage(foroViewModel));

                ForoSeleccionado = null;
            });

            NuevoForo = new Command(async () => {
                Entry nombreForo = new Entry();
                Button crearForo = new Button { Text = "Crear Foro" };
                crearForo.Clicked += async (object sender, EventArgs args) => {
                    var document = new BsonDocument{
                        {"IdForo", new Random().Next(1, 20000)},
                        {"NombreForo", nombreForo.Text }
                    };
                    forosCollection.InsertOne(document);
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                };
                Button cancelarCreacion = new Button { Text = "Cancelar" };
                cancelarCreacion.Clicked += async (object sender, EventArgs args) => {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                };
                StackLayout CrearForoPage = new StackLayout(){
                    Children = {
                        new Label{ Text = "Nombre Foro" },
                        nombreForo,
                        crearForo,
                        cancelarCreacion
                    }
                };
                await Application.Current.MainPage.Navigation.PushModalAsync(new ContentPage { Content = CrearForoPage});
            });
        }

        ObservableCollection<Foro> foros;

        public ObservableCollection<Foro> Foros{
            get => foros;
            set{
                foros = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Foros)));
            }
        }

        Foro foroSeleccionado;
        public Foro ForoSeleccionado{
            get => foroSeleccionado;
            set{
                foroSeleccionado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForoSeleccionado)));
            }
        }

        public Command AbrirMensajesForo { get; }
        public Command NuevoForo { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
