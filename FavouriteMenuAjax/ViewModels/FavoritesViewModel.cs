using FavouriteMenuAjax.Models;

namespace FavouriteMenuAjax.ViewModels
{

    public class FavoritesViewModel
    {
        public List<Entity> Entities { get; set; }
        public Entity Entity { get; set; }
        public int? Count { get; set; }
        public bool isExisted { get; set; }
    }

    // List for demo purpose !!!
    public class EntityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isInFavourites { get; set; }
    }

}