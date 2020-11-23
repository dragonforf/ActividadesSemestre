using ActividadesSemestre.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForoPage : ContentPage{
        public ForoPage(ForoViewModel foroViewModel){
            BindingContext = foroViewModel;
            InitializeComponent();
        }
    }
}