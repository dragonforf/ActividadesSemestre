using ActividadesSemestre.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DirectorioPage : ContentPage{
        public DirectorioPage(DirectorioViewModel directorioViewModel){
            BindingContext = directorioViewModel;
            InitializeComponent();
        }
    }
}