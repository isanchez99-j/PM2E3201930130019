using Proyecto3.Models;
using System;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListLugares : ContentPage
    {
        public ListLugares()
        {
            InitializeComponent();
        }

        // Gets the places from the db and sets the
        // list to this
        private async void Cargar_Recibos()
        {
            // Get the lists from the db
            var recibos = await App.DBase.TodosRecibos();
            Lista.ItemsSource = recibos;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Lista.ItemsSource = await App.DBase.TodosRecibos();
        }

        // Long press to delete
        private async void Eliminar_Clicked(object sender, EventArgs e)
        { 
            bool answer = await DisplayAlert("Confirmacion", "¿Quiere eliminar el registro?", "Si", "No");
           
            if (answer == true)
            {
                var idRecibo = (Recibo)(sender as MenuItem).CommandParameter;
                var result = await App.DBase.DeleteRecibo(idRecibo);

                if (result == 1)
                {
                    await DisplayAlert("Aviso", "Registro Eliminado", "OK");
                    Cargar_Recibos();
                }
                else
                {
                    await DisplayAlert("Aviso", "Revisa", "OK");
                }
            };
        }


        private async void Actualizar_Clicked(object sender, EventArgs e)
        {
            var recibo = (Recibo)(sender as MenuItem).CommandParameter;
            await Navigation.PushAsync(new EditarLugar(recibo));
        }

        private void Lista_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}