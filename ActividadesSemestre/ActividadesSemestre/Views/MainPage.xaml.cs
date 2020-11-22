using MongoDB.Bson;
using MongoDB.Driver;
using System;
using Xamarin.Forms;

namespace ActividadesSemestre
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Mensaje.Text = "";
        }

        public void IniciarSesion(object sender, EventArgs args){
            if(Usuario.Text != null && Clave.Text != null){
                var Usuarios = App.ActividadesSemestreBD.GetCollection<BsonDocument>("Usuarios");
                var filter = Builders<BsonDocument>.Filter.Eq("usuario", Usuario.Text) & Builders<BsonDocument>.Filter.Eq("clave", Clave.Text);
                BsonDocument usuario = Usuarios.Find(filter).FirstOrDefault();
                if(usuario != null){ 
                    App.UsuarioAutenticadoId = (int) usuario["idUsuario"];
                    Mensaje.Text = "Usuario autenticado correctamente.";
                }
                else{
                    Mensaje.Text = "La autenticación falló.";
                    Clave.Text = "";
                }                
            }
            else
                Mensaje.Text = "Datos inválidos.";
        }

        public void Registrarse(object sender, EventArgs args){
            DisplayAlert("Ups...", "El módulo de registro está en desarrollo...", "VOLVER");    
        }
    }
}
