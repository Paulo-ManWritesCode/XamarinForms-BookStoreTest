using System;
using BookStoreTest.Core.Books.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BookStoreTest.UI.DetailPage
{
    public partial class DetailPage : ContentPage
    {
        private Volume volume;

        public DetailPage(Volume volume)
        {
            this.volume = volume;

            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is DetailPageViewModel viewModel)
            {
                viewModel.Volume = volume;
            }
        }

        public async void BuyButton_Clicked(Object sender, EventArgs e)
        {
            if (BindingContext is DetailPageViewModel viewModel)
            {
                await Browser.OpenAsync(
                    viewModel.Volume.SaleInfo.BuyLink,
                    BrowserLaunchMode.SystemPreferred);
            }
        }
    }
}

