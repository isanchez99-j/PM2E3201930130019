using Proyecto3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarLugar : ContentPage
    {
        private readonly Recibo lugar;

        public EditarLugar(Recibo recibo)
        {
            InitializeComponent();

            this.lugar = recibo;

            txtDescripcion.Text = recibo.descripcion;
            txtMonto.Text = recibo.monto.ToString();
            txtFecha.Text = recibo.fecha;
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            lugar.descripcion = txtDescripcion.Text;
            lugar.monto = double.Parse(txtMonto.Text);
            lugar.fecha = txtFecha.Text;

            // Save the changes to the database
            var result = await App.DBase.ReciboSave(lugar);

            Debug.WriteLine(result);
         
            await DisplayAlert("Aviso", "Registro Actualizado", "OK");
            await Navigation.PopAsync();
        }
    }
}