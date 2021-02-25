using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinEntre2Ages.ViewModels;
using XamarinEntre2Ages.Views;

namespace XamarinEntre2Ages
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
