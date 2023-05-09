using Plugin.Media;
using Proyecto3.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Proyecto3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            TestInternet();
        }

        public async void TestInternet()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Alerta", "No hay Internet disponible!", "OK");
            }
        }

        Plugin.Media.Abstractions.MediaFile Filefoto = null;

        private Byte[] ConvertImageToByteArray()
        {
            if (Filefoto != null)
            {
                using (MemoryStream memory = new MemoryStream()) 
                {
                    Stream stream = Filefoto.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }

            }
            return null;

        }
        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            if (Filefoto == null)
            {
                await DisplayAlert("Advertencia", "Debe tomar una foto", "OK");
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                await DisplayAlert("Advertencia", "El campo del Descripcion es obligatorio.", "OK");
            }
            else if (string.IsNullOrEmpty(txtMonto.Text))
            {
                await DisplayAlert("Advertencia", "El campo de Monto es obligatorio", "OK");
            }
            else if (string.IsNullOrEmpty(txtFecha.Text))
            {
                await DisplayAlert("Advertencia", "El campo de Fecha es obligatorio", "OK");
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text) && string.IsNullOrEmpty(txtFecha.Text) && string.IsNullOrEmpty(txtMonto.Text))
            {
                await DisplayAlert("Advertencia", "No se puede agregar Registro. Rellene los campos.", "OK");
            }
            else
            {
                var recibo = new Models.Recibo
                {
                    id = 0,
                    monto = (double.Parse(txtMonto.Text)),
                    descripcion = txtDescripcion.Text,
                    foto_recibo = ConvertImageToByteArray(),
                    fecha = txtFecha.Text
                };

             
                var result = await App.DBase.ReciboSave(recibo);

                if (result > 0)//se usa como una super clase
                {
                    await DisplayAlert("Aviso", "Recibo Registrado", "OK");
                    Clear();

                }
                else
                {
                    await DisplayAlert("Aviso", "Error al Registrar", "OK");
                }
            }
        }

        private async void btnList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListLugares());
        }

        private async void btnFoto_Clicked(object sender, EventArgs e)
        {
            // Tomar foto
            Filefoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MisFotos",
                Name = "test.jpg",
                SaveToAlbum = true,
            });

            // Si hay foto
            if (Filefoto != null)
            {
                fotoRecibo.Source = ImageSource.FromStream(() =>
                {
                    return Filefoto.GetStream();
                });
            }
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            // Closes the app and pushes the user to the login page
            await SecureStorage.SetAsync("IsLoggedIn", "false");
            await Navigation.PushAsync(new LoginPage());
        }

        private void Clear()
        {
            txtDescripcion.Text = "";
            txtFecha.Text = "";
            txtMonto.Text = "";
            fotoRecibo.Source = null;
        }
    }
}
