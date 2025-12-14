using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GyorsHir.Persistence.TextJSON
{
    public class GyorsHirTextJSONPersistence : IGyorsHirPersistence
    {

        #region Properties

        private string _FavouritesFilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "favourites.json");

        #endregion

        #region Private Methods

        private async Task SaveFavouritesAsync(List<FavouriteNewsPortal> favourites)
        {
            try
            {
                string json = JsonConvert.SerializeObject(favourites);
                await File.WriteAllTextAsync(_FavouritesFilePath, json);
            }
            catch { }
        }
        private async Task<List<FavouriteNewsPortal>> LoadFavouritesAsync()
        {
            try
            {
                string filePath = _FavouritesFilePath;
                List<FavouriteNewsPortal>? favourites = null;
                if (File.Exists(filePath))
                {
                    string json = await File.ReadAllTextAsync(filePath);
                    favourites = JsonConvert.DeserializeObject<List<FavouriteNewsPortal>>(json);
                }
                return favourites ?? new List<FavouriteNewsPortal>();
            }
            catch
            {
                return new List<FavouriteNewsPortal>();
            }
        }

        #endregion

        #region Public Methods

        public async Task AddFavouriteAsync(FavouriteNewsPortal favourite)
        {
            List<FavouriteNewsPortal> favourites = await LoadFavouritesAsync();
            favourites.Add(favourite);
            await SaveFavouritesAsync(favourites);
        }
        public async Task RemoveFavouriteAsync(string feedId)
        {
            List<FavouriteNewsPortal> favourites = await LoadFavouritesAsync();
            favourites.RemoveAll(f => f.FeedId == feedId);
            await SaveFavouritesAsync(favourites);
        }
        public async Task<IEnumerable<FavouriteNewsPortal>> GetFavouritesAsync() => await LoadFavouritesAsync();

        #endregion

    }
}
