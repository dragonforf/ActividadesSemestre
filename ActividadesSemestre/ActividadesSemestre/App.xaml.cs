using MongoDB.Driver;
using Xamarin.Forms;

namespace ActividadesSemestre{
    public partial class App : Application{
        public static int UsuarioAutenticadoId;
        public static IMongoDatabase ActividadesSemestreBD;
        
        public App(){
            InitializeComponent();
            UsuarioAutenticadoId = 0;
            MainPage = new NavigationPage(new LoginPage());
            string CadenaConexion = "mongodb://Xamarin1:Xamarin1234@cluster0-shard-00-00.0scgh.mongodb.net:27017,cluster0-shard-00-01.0scgh.mongodb.net:27017,cluster0-shard-00-02.0scgh.mongodb.net:27017/ActividadesSemestre?ssl=true&replicaSet=atlas-1el95h-shard-0&authSource=admin&retryWrites=true&w=majority";
            ActividadesSemestreBD = (new MongoClient(CadenaConexion)).GetDatabase("ActividadesSemestre");
        }

        protected override void OnStart(){}

        protected override void OnSleep(){}

        protected override void OnResume(){}
    }
}
