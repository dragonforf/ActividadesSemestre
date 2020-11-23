using ActividadesSemestre.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleContactoPage : ContentPage{
        public DetalleContactoPage(DetalleContactoViewModel detalleContactoViewModel){
            BindingContext = detalleContactoViewModel;
            InitializeComponent();
        }
    }
}