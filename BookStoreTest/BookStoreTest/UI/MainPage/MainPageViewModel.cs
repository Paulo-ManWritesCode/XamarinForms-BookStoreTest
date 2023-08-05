using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BookStoreTest.Core.Books;
using BookStoreTest.Core.Books.Models;
using Xamarin.Forms;

namespace BookStoreTest.UI
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private VolumeService volumeService = new VolumeService();
        private Task getVolumesTask = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Volume> volumes = new ObservableCollection<Volume>();
        public ObservableCollection<Volume> Volumes
        {
            set
            {
                if (volumes != value)
                {
                    volumes = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Volumes)));
                    }
                }
            }
            get
            {
                return volumes;
            }
        }

        private bool isFavoritesToggled = false;
        public bool IsFavoritesToggled
        {
            set
            {
                if (isFavoritesToggled != value)
                {
                    isFavoritesToggled = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsFavoritesToggled)));
                    }

                    Volumes.Clear();
                    if (getVolumesTask != null && !getVolumesTask.IsCompleted)
                    {
                        getVolumesTask.ContinueWith(async (task) =>
                        {
                            await task;
                            fetchData();
                        });
                    }
                    else
                    {
                        fetchData();
                    }
                }
            }
            get
            {
                return isFavoritesToggled;
            }
        }

        public ICommand LoadMoreCommand { get; set; }

        public MainPageViewModel()
        {
            LoadMoreCommand = new Command(execute: fetchData);
            fetchData();
        }

        public void fetchData()
        {
            if (getVolumesTask != null && !getVolumesTask.IsCompleted)
            {
                return;
            }

            if (IsFavoritesToggled)
            {
                if (Volumes.Count > 0)
                {
                    return;
                }

                getVolumesTask = volumeService.GetFavoriteVolumes()
                    .ContinueWith(async (task) =>
                    {
                        List<Volume> response = await task;
                        Volumes = new ObservableCollection<Volume>(response);
                    });
            }
            else
            {
                getVolumesTask = volumeService.GetVolumes(20, Volumes.Count)
                    .ContinueWith(async (task) =>
                    {
                        List<Volume> response = await task;
                        response.ForEach((item) =>
                        {
                            Volumes.Add(item);
                        });
                    });
            }
        }
    }
}

