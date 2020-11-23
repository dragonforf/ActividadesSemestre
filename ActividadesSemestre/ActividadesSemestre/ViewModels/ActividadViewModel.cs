using ActividadesSemestre.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ActividadesSemestre.ViewModels
{
    public class ActividadViewModel : INotifyPropertyChanged{

        public ActividadViewModel(){
            var collection = App.ActividadesSemestreBD.GetCollection<BsonDocument>("Actividades");

            GuardarActividad = new Command(async () => {
                if (EsNueva){
                    int id = int.Parse((CorteSeleccionado.ActividadesCorte.Count + 1) + "" + CorteSeleccionado.Id);

                    var actividadCorte = new Actividad{
                        Id = id,
                        Titulo = Titulo,
                        Observaciones = Observaciones,
                        FechaEntrega = FechaEntrega,
                        Nota = Nota,
                        Latitud = Latitud,
                        Longitud = Longitud
                    };

                    CorteSeleccionado.ActividadesCorte.Add(actividadCorte);

                    var document = new BsonDocument{
                      {"Id", actividadCorte.Id},
                      {"Titulo", actividadCorte.Titulo},
                      {"Observaciones", actividadCorte.Observaciones},
                      {"FechaEntrega", actividadCorte.FechaEntrega},
                      {"Nota", actividadCorte.Nota},
                      {"CorteId", CorteSeleccionado.Id },
                      {"latitud", actividadCorte.Latitud },
                      {"longitud", actividadCorte.Longitud }
                    };

                    collection.InsertOne(document);
                }
                else{  //actualizar  
                    var actividad = CorteSeleccionado.ActividadesCorte.Where(x => x.Id == Id).FirstOrDefault();
                    actividad.Titulo = Titulo;
                    actividad.Observaciones = Observaciones;
                    actividad.FechaEntrega = FechaEntrega;
                    actividad.Nota = Nota;
                    actividad.Latitud = Latitud;
                    actividad.Longitud = Longitud;

                    var filter = Builders<BsonDocument>.Filter.Eq("Id", Id);
                    var update = Builders<BsonDocument>.Update.Set("Titulo", Titulo);
                    collection.UpdateOne(filter, update);
                    update = Builders<BsonDocument>.Update.Set("Observaciones", Observaciones);
                    collection.UpdateOne(filter, update);
                    update = Builders<BsonDocument>.Update.Set("FechaEntrega", FechaEntrega);
                    collection.UpdateOne(filter, update);
                    update = Builders<BsonDocument>.Update.Set("Nota", Nota);
                    collection.UpdateOne(filter, update);
                    update = Builders<BsonDocument>.Update.Set("latitud", Latitud);
                    collection.UpdateOne(filter, update);
                    update = Builders<BsonDocument>.Update.Set("longitud", Longitud);
                    collection.UpdateOne(filter, update);
                }
                await Application.Current.MainPage.Navigation.PopAsync();
            });

            EliminarActividad = new Command(async () => {
                var actividad = CorteSeleccionado.ActividadesCorte.Where(x => x.Id == Id).FirstOrDefault();
                CorteSeleccionado.ActividadesCorte.Remove(actividad);

                var deleteFilter = Builders<BsonDocument>.Filter.Eq("Id", Id);
                collection.DeleteOne(deleteFilter);

                await Application.Current.MainPage.Navigation.PopAsync();
            });

            SeleccionarUbicacion = new Command(async () => {
                Position position;
                try { position = new Position(double.Parse(Latitud), double.Parse(Longitud)); }
                catch (Exception) { position = new Position(4.6523053, -74.0802087); }

                var map = new Map(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)));
                map.MapClicked += async (sender, eventArgs) => {
                    var x = eventArgs.Position;
                    var latitud = x.Latitude.ToString();
                    var longitud = x.Longitude.ToString();

                    Latitud = latitud;
                    Longitud = longitud;

                    await Application.Current.MainPage.Navigation.PopAsync();
                };

                await Application.Current.MainPage.Navigation.PushAsync(new ContentPage() { Content = map });
            });
        }

        public int cantidadMensajes { get; set; }

        Corte corteSeleccionado;
        public Corte CorteSeleccionado { 
            get => corteSeleccionado;
            set{
                corteSeleccionado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorteSeleccionado)));
            }
        }

        int id;
        public int Id{
            get => id;
            set{
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        string titulo;
        public string Titulo{
            get => titulo;
            set{
                titulo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Titulo)));
            }
        }

        string observaciones;
        public string Observaciones{
            get => observaciones;
            set{
                observaciones = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Observaciones)));
            }
        }

        DateTime fechaEntrega;
        public DateTime FechaEntrega
        {
            get => fechaEntrega;
            set{
                fechaEntrega = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FechaEntrega)));
            }
        }

        string latitud;
        public string Latitud
        {
            get => latitud;
            set{
                latitud = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Latitud)));
            }
        }

        string longitud;
        public string Longitud{
            get => longitud;
            set{
                longitud = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Longitud)));
            }
        }

        double nota;
        public double Nota{
            get => nota;
            set{
                nota = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nota)));
            }
        }

        bool esNueva;
        public bool EsNueva{
            get => esNueva;
            set{
                esNueva = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EsNueva)));
            }
        }

        public Command GuardarActividad { get; }
        public Command EliminarActividad { get; }
        public Command CargarMensajes { get; }
        public Command SeleccionarUbicacion { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
