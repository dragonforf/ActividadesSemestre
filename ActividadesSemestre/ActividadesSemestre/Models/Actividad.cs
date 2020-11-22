using System;

namespace ActividadesSemestre.Models{
    public class Actividad{
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaEntrega { get; set; }
        public double Nota { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
    }
}
