using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;

namespace ActividadesSemestre.Droid
{
    [Activity(Label = "ActividadesSemestre", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const int RequestLocationId = 0;

        readonly string[] LocationPermissions = {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnStart(){
            base.OnStart();

            if ((int)Build.VERSION.SdkInt >= 23){
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted){
                    RequestPermissions(LocationPermissions, RequestLocationId);
                }
                else{
                    Toast.MakeText(this, "Permisos de ubicación ya han sido concedidos.", ToastLength.Short);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults){
            if (requestCode == RequestLocationId){
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                    Toast.MakeText(this, "Permisos de ubicación concedidos.", ToastLength.Short);
                else
                    Toast.MakeText(this, "Permisos de ubicación denegados.", ToastLength.Short);
            }
            else{
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}