namespace FavouriteMenuAjax.ViewModels
{
    using FavouriteMenuAjax.Models;
    using System.Collections.Generic;


    public class HeaderMenuViewModel
    {

        public List<Entity> Favourites { get; set; }
        public List<Entity> Entities { get; set; }

        public int? FavouritesCount { get; set; }
    }
}