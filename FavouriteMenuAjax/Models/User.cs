namespace FavouriteMenuAjax.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {     
        // Favourites 
        public string Favourites { get; set; } = "0"; /// string 1,2,3 
    }
}
