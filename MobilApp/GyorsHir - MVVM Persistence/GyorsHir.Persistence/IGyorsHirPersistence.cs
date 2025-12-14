using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GyorsHir.Persistence
{
    public interface IGyorsHirPersistence
    {

        Task AddFavouriteAsync(FavouriteNewsPortal favourite);
        Task RemoveFavouriteAsync(string feedId);
        Task<IEnumerable<FavouriteNewsPortal>> GetFavouritesAsync();

    }
}
