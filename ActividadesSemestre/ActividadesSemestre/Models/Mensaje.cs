using System;

namespace ActividadesSemestre.Models{
    public class Mensaje{
        public int IdForo { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
    }
}
