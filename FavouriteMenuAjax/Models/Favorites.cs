namespace FavouriteMenuAjax.Models
{ 
    public class Favorites 
    {
        public int Id { get; set; }

        public int? EntityId { get; set; }

        public string UserId { get; set; }

    }


     // List for demo purpose !!! jobs, companies, products 
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public bool isInFavourites { get; set; }
    }
}