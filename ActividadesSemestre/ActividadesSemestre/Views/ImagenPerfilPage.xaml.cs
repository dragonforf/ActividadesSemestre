using MongoDB.Bson;
using MongoDB.Driver;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActividadesSemestre.Views{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImagenPerfilPage : ContentPage{
        Image image;
        IMongoCollection<BsonDocument> collection;
        public ImagenPerfilPage(){
            collection = App.ActividadesSemestreBD.GetCollection<BsonDocument>("Usuarios");
            var filter = Builders<BsonDocument>.Filter.Eq("idUsuario", App.UsuarioAutenticadoId);
            var usuario = collection.Find(filter).FirstOrDefault();
            InitializeComponent();
            image = new Image();
            try{
                string base64 = (string)usuario["imagenPerfil"];
                byte[] bytes = Convert.FromBase64String(base64);
                image.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
            catch(KeyNotFoundException){
                image.Source = "defaultProfileIcon.jpg";
            }
            Contenedor.Children.Add(image);
        }
        async public void EditarImagenPerfil(object sender, EventArgs e){
            await CrossMedia.Current.Initialize();
            var theImage = await CrossMedia.Current.PickPhotoAsync();

            if (theImage == null){
                await DisplayAlert("Error", "No se pudo obtener la imagen", "OK");
            }
            else{
                image.Source = ImageSource.FromStream(() => theImage.GetStream());
                var base64 = ImageToBase64(theImage);
                var filter = Builders<BsonDocument>.Filter.Eq("idUsuario", App.UsuarioAutenticadoId);
                var update = Builders<BsonDocument>.Update.Set("imagenPerfil", base64);
                collection.UpdateOne(filter, update);
            }
        }

        string ImageToBase64(MediaFile theImage){
            using (var memoryStream = new MemoryStream()){
                theImage.GetStream().CopyTo(memoryStream);
                theImage.Dispose();
                string base64String = Convert.ToBase64String(memoryStream.ToArray());
                return base64String;
            }
        }
    }
}