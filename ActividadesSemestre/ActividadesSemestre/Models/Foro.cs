using System.Collections.ObjectModel;

namespace ActividadesSemestre.Models{
    public class Foro{
        public int IdForo { get; set; }
        public string NombreForo { get; set; }
        public ObservableCollection<Mensaje> Mensajes { get; set; }
    }
}
