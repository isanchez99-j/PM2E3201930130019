using Proyecto3.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Proyecto3.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}