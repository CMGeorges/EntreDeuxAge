using System.ComponentModel;
using Xamarin.Forms;
using XamarinEntre2Ages.ViewModels;

namespace XamarinEntre2Ages.Views
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