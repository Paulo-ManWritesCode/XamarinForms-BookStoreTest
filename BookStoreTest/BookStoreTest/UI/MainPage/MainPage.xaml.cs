using System.Linq;
using BookStoreTest.Core.Books.Models;
using BookStoreTest.UI.DetailPage;
using Xamarin.Forms;

namespace BookStoreTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Volume volume)
            {
                if (sender is CollectionView collectionView)
                {
                    collectionView.SelectedItem = null;
                }

                await Navigation.PushAsync(new DetailPage(volume));
            }
        }
    }
}

