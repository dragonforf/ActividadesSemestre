using ActividadesSemestre.Models;
using ActividadesSemestre.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace ActividadesSemestre.ViewModels
{
    public class DirectorioViewModel : INotifyPropertyChanged{

        public DirectorioViewModel(){
            AbrirDetalleContacto = new Command(async () => {
                if (ContactoSeleccionado is null)
                    return;

                DetalleContactoViewModel detalleContactoViewModel = new DetalleContactoViewModel { Contacto = ContactoSeleccionado };
                await Application.Current.MainPage.Navigation.PushAsync(new DetalleContactoPage(detalleContactoViewModel));
            });
        }

        Contacto contactoSeleccionado;

        public Contacto ContactoSeleccionado{
            get => contactoSeleccionado;
            set{
                contactoSeleccionado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContactoSeleccionado)));
            }
        }

        ObservableCollection<Contacto> contactos;
        public ObservableCollection<Contacto> Contactos{
            get => contactos;
            set{
                contactos = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contactos)));
            }
        }

        public Command AbrirDetalleContacto { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
