using ActividadesSemestre.Models;
using ActividadesSemestre.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace ActividadesSemestre.ViewModels{
    public class CorteViewModel{
        public event PropertyChangedEventHandler PropertyChanged;

        public CorteViewModel(){
            AbrirDetalleActividad = new Command(async () => {
                if (ActividadSeleccionada is null)
                    return;

                var actividadViewModel = new ActividadViewModel{
                    CorteSeleccionado = corteSeleccionado,
                    Id = ActividadSeleccionada.Id,
                    Titulo = ActividadSeleccionada.Titulo,
                    Observaciones = ActividadSeleccionada.Observaciones,
                    FechaEntrega = ActividadSeleccionada.FechaEntrega,
                    Nota = actividadSeleccionada.Nota,
                    Latitud = actividadSeleccionada.Latitud,
                    Longitud = actividadSeleccionada.Longitud
                };

                await Application.Current.MainPage.Navigation.PushAsync(new ActividadPage(actividadViewModel));

                ActividadSeleccionada = null;
            });

            InsertarNuevaActividad = new Command(async () => {
                var actividadViewModel = new ActividadViewModel{
                    CorteSeleccionado = CorteSeleccionado,
                    EsNueva = true,
                    FechaEntrega = DateTime.Today
                };

                await Application.Current.MainPage.Navigation.PushAsync(new ActividadPage(actividadViewModel));

                ActividadSeleccionada = null;
            });
        }

        string nombre;
        public string Nombre{
            get => nombre;
            set{
                nombre = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nombre)));
            }
        }

        int porcentaje;
        public int Porcentaje{
            get => porcentaje;
            set{
                porcentaje = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Porcentaje)));
            }
        }

        string fechaInicio;
        public string FechaInicio{
            get => fechaInicio;
            set{
                fechaInicio = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FechaInicio)));
            }
        }

        string fechaFin;
        public string FechaFin{
            get => fechaFin;
            set{
                fechaFin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FechaFin)));
            }
        }

        Actividad actividadSeleccionada;
        public Actividad ActividadSeleccionada{
            get => actividadSeleccionada;
            set{
                actividadSeleccionada = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActividadSeleccionada)));
            }
        }

        Corte corteSeleccionado;
        public Corte CorteSeleccionado{
            get => corteSeleccionado;
            set{
                corteSeleccionado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorteSeleccionado)));
            }
        }

        ObservableCollection<Actividad> actividades;
        public ObservableCollection<Actividad> Actividades{
            get => actividades;
            set{
                actividades = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Actividades)));
            }
        }

        public Command AbrirDetalleActividad { get; }
        public Command InsertarNuevaActividad { get; }
    }
}
