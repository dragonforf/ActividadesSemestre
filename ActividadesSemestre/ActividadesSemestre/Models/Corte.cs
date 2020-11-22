using System;
using System.Collections.ObjectModel;

namespace ActividadesSemestre.Models{
    public class Corte{
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int Porcentaje { get; set; }
        public string Nombre { get; set; }
        public ObservableCollection<Actividad> ActividadesCorte { get; set; }
    }
}
