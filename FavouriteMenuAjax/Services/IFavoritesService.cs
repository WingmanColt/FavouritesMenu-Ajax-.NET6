namespace FavouriteMenuAjax.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FavouriteMenuAjax.Models;

    public interface IFavoritesService
    {
        Task<bool> UpdateFavourite(User user, string postId);
        Task<bool> RemoveAllFavourites(User user);

        // IAsyncEnumerable<TViewModel> GetFavouriteBy<TViewModel>(User user);
       List<Entity> GetFavouriteBy(List<Entity> listOfEntities, User user);
        int? GetFavouriteByCount(User user);

        bool isInFavourite(User user, string id);
    }
}
