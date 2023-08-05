using System.ComponentModel;
using BookStoreTest.Core.Books;
using BookStoreTest.Core.Books.Models;

namespace BookStoreTest.UI
{
    public class DetailPageViewModel : INotifyPropertyChanged
    {
        private VolumeService volumeService = new VolumeService();

        public event PropertyChangedEventHandler PropertyChanged;

        private Volume volume = null;
        public Volume Volume
        {
            set
            {
                if (volume != value)
                {
                    volume = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Volume)));
                    }

                    IsFavoriteToggled = volumeService
                        .GetFavoriteVolumeIDs()
                        .Contains(volume.ID);
                }
            }
            get
            {
                return volume;
            }
        }

        private bool isFavoriteToggled = false;
        public bool IsFavoriteToggled
        {
            set
            {
                if (isFavoriteToggled != value)
                {
                    isFavoriteToggled = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsFavoriteToggled)));
                    }

                    if (isFavoriteToggled)
                    {
                        volumeService.AddVolumeIDToFavorites(Volume.ID);
                    }
                    else
                    {
                        volumeService.RemoveVolumeIDFromFavorites(Volume.ID);
                    }
                }
            }
            get
            {
                return isFavoriteToggled;
            }
        }

        public DetailPageViewModel()
        {
        }
    }
}
