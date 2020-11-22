using ActividadesSemestre.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CortePage : ContentPage{
        public CortePage(CorteViewModel corteViewModel){
            BindingContext = corteViewModel;
            InitializeComponent();
        }
    }
}