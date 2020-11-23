using ActividadesSemestre.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForosPage : ContentPage{
        public ForosPage(ForosViewModel forosViewModel){
            BindingContext = forosViewModel;
            InitializeComponent();
        }
    }
}