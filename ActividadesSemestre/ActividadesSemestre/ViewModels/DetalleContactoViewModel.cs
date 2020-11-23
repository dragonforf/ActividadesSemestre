using ActividadesSemestre.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ActividadesSemestre.ViewModels{
    public class DetalleContactoViewModel : INotifyPropertyChanged{

        public DetalleContactoViewModel(){
            EnviarCorreo = new Command(async () => {
                var destinatarios = new List<string>();
                destinatarios.Add(Contacto.Email);
                await SendEmail(Asunto, Mensaje, destinatarios);
            });
        }

        private async Task SendEmail(string subject, string body, List<string> recipients){
            try{
                var message = new EmailMessage{
                    Subject = subject,
                    Body = body,
                    To = recipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException) { /* Email is not supported on this device*/ }
            catch (Exception) { /*Some other exception occurred*/ }
        }

        Contacto contacto;
        public Contacto Contacto{
            get => contacto;
            set{
                contacto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contacto)));
            }
        }

        string asunto;
        public string Asunto{
            get => asunto;
            set{
                asunto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contacto)));
            }
        }

        string mensaje;
        public string Mensaje{
            get => mensaje;
            set{
                mensaje = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mensaje)));
            }
        }

        public Command EnviarCorreo { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
