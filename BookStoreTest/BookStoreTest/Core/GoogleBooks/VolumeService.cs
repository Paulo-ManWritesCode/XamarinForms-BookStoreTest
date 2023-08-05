using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStoreTest.Core.Books.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace BookStoreTest.Core.Books
{
    public class VolumeService
    {
        private HttpClient client;

        public VolumeService()
        {
            client = new HttpClient();

        }

        public async Task<List<Volume>> GetVolumes(int maxResults, int startIndex)
        {
            Uri uri = new Uri($"https://www.googleapis.com/books/v1/volumes?q=Mobile%20Development&maxResults={maxResults}&startIndex={startIndex}");
            HttpResponseMessage response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Invalid Status Code");
            }

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetVolumesResponse>(content).Items;
        }

        public async Task<List<Volume>> GetFavoriteVolumes()
        {
            List<string> favorites = GetFavoriteVolumeIDs();
            return await Task.Run(() =>
            {
                return favorites.Select((id) =>
                {
                    Uri uri = new Uri($"https://www.googleapis.com/books/v1/volumes/{id}");
                    HttpResponseMessage response = client.GetAsync(uri).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Invalid Status Code");
                    }

                    string content = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Volume>(content);
                }).ToList();
            });
        }

        public void AddVolumeIDToFavorites(string id)
        {
            List<string> favorites = GetFavoriteVolumeIDs();

            if (favorites.Contains(id))
            {
                return;
            }

            favorites.Add(id);
            SetFavoriteVolumeIDs(favorites);
        }

        public void RemoveVolumeIDFromFavorites(string id)
        {
            List<string> favorites = GetFavoriteVolumeIDs();

            if (!favorites.Contains(id))
            {
                return;
            }

            favorites.Remove(id);
            SetFavoriteVolumeIDs(favorites);
        }

        public List<string> GetFavoriteVolumeIDs()
        {
            string favoritesJSON = Preferences.Get("favorites", "[]");
            return JsonConvert.DeserializeObject<List<string>>(favoritesJSON);
        }

        public void SetFavoriteVolumeIDs(List<string> favorites)
        {
            string favoritesJSON = JsonConvert.SerializeObject(favorites);
            Preferences.Set("favorites", favoritesJSON);
        }
    }
}

