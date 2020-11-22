using ActividadesSemestre.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActividadPage : ContentPage{
        public ActividadPage(ActividadViewModel actividadViewModel){
            BindingContext = actividadViewModel;
            InitializeComponent();
        }
    }
}