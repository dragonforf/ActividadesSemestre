using ActividadesSemestre.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace ActividadesSemestre.ViewModels{
    public class ForoViewModel : INotifyPropertyChanged{

        public ForoViewModel(){
            EnviarMensaje = new Command(() => {
                var collection = App.ActividadesSemestreBD.GetCollection<BsonDocument>("MensajesForo");
                var mensaje = new Mensaje{
                    IdForo = IdForo,
                    Texto = MensajeAEnviar,
                    Fecha = DateTime.Now,
                    IdUsuario = IdUsuario,
                    NombreUsuario = App.ObtenerRemitente(IdUsuario)
                };
                var document = new BsonDocument{
                    {"IdForo", mensaje.IdForo},
                    {"Mensaje", mensaje.Texto},
                    {"Fecha", mensaje.Fecha},
                    {"IdUsuario", mensaje.IdUsuario}
                };

                Mensajes.Add(mensaje);
                collection.InsertOne(document);
                MensajeAEnviar = "";
            });

            RefrescarMensajes = new Command( () => {
                Mensajes.Clear();
                var mensajes = App.ActividadesSemestreBD.GetCollection<BsonDocument>("MensajesForo")
                               .Find(Builders<BsonDocument>.Filter.Eq("IdForo", IdForo)).ToList();
                foreach(BsonDocument mensaje in mensajes){ 
                    int idRemitente = (int)mensaje["IdUsuario"];
                    Mensajes.Add(new Mensaje{
                        IdForo = IdForo,
                        IdUsuario = idRemitente,
                        Fecha = (DateTime)mensaje["Fecha"],
                        Texto = mensaje["Mensaje"].ToString(),
                        NombreUsuario = App.ObtenerRemitente(idRemitente)
                    });
                }
            });
        }

        int idForo;
        public int IdForo{
            get => idForo;
            set{
                idForo= value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IdForo)));
            }
        }

        ObservableCollection<Mensaje> mensajes;
        public ObservableCollection<Mensaje> Mensajes{
            get => mensajes;
            set{ 
                mensajes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mensajes)));
            }
        }

        int idUsuario;
        public int IdUsuario{
            get => idUsuario;
            set{ 
                idUsuario = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IdUsuario)));
            }
        }

        string nombreForo;
        public string NombreForo{
            get => nombreForo;
            set{
                nombreForo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NombreForo)));
            }
        }

        string mensajeAEnviar;
        public string MensajeAEnviar{
            get => mensajeAEnviar;
            set{
                mensajeAEnviar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MensajeAEnviar)));
            }
        }

        public Command EnviarMensaje { get; }

        public Command RefrescarMensajes { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
