namespace FavouriteMenuAjax.Services
{
    using FavouriteMenuAjax.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class FavoritesService : IFavoritesService
    {
        private readonly UserManager<User> _userManager;
        public FavoritesService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> UpdateFavourite(User user, string entityId)
        {
            string postIdComplate = ',' + entityId;
            bool isExisted = false;

            if (user.Favourites.IndexOf(entityId) > -1)
            {
                user.Favourites = user.Favourites.Replace(postIdComplate, "");
                isExisted = true;
            }
            else
            {
                user.Favourites += postIdComplate;
                isExisted = false;
            }

            await _userManager.UpdateAsync(user); 
            return isExisted;
        }
        public async Task<bool> RemoveAllFavourites(User user)
        {
            user.Favourites = "0";
            await _userManager.UpdateAsync(user); 
            return true;
        }
        /*
                public async Task<bool> RemoveFromFavourite(User user, PostType postType, string postId)
                {
                    if (user is null)
                        return false;

                    string postIdComplate = ',' + postId;

                    switch (postType)
                    {
                        case PostType.Company:
                            {
                                if (user.FavouriteCompanies?.IndexOf(postId) > -1)
                                {
                                    user.FavouriteCompanies = user.FavouriteCompanies.Replace(postIdComplate, "");
                                }
                            }
                            break;
                        case PostType.Contestant:
                            {
                                if (user.FavouriteContestants?.IndexOf(postId) > -1)
                                {
                                    user.FavouriteContestants = user.FavouriteContestants.Replace(postIdComplate, "");
                                }
                            }
                            break;
                        case PostType.Job:
                            {
                                if (user.FavouriteJobs?.IndexOf(postId) > -1)
                                {
                                    user.FavouriteJobs = user.FavouriteJobs.Replace(postIdComplate, "");
                                }
                            }
                            break;
                    }

                    await _userManager.UpdateAsync(user);
                    return true;
                }
        */

        // DEMO
        public List<Entity> GetFavouriteBy(List<Entity> listOfEntities, User user)
        {
            string[] items = user?.Favourites?.Split(',');
            if (items is null)
                return null;

 
            // For DEMO PURPOSE
            return listOfEntities.AsQueryable()
                .Where(x => ((IList)items).Contains(x.Id.ToString()))
                .ToList();
        }


        // USE THIS METHOD !!!!
        /*
        public IAsyncEnumerable<TViewModel> GetFavouriteBy<TViewModel>(User user, PostType postType)
        {
            if (user is null)
                return null;

            string[] items = user?.Favourites?.Split(',');
             if (items is null)
                return null;

       //!!! Your list of content should be here (which item will stay in favourites) !!!
        var entities = _jobsService.GetAllAsNoTracking()
                .Where(x => ((IList)items).Contains(x.Id.ToString()))
                .AsAsyncEnumerable();

         return (IAsyncEnumerable<TViewModel>)entities;
        }*/


        public int? GetFavouriteByCount(User user)
        {
            if (user is null)
                return -1;

           var items = user?.Favourites?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .Skip(1);

           int? count = items?.Count();

         return count;                            
        }
        public bool isInFavourite(User user, string id)
        {
            if (user is null)
                return false;

          if (user.Favourites?.IndexOf(id) > -1)
             {
                 return true;
             }
          return false;
        }

        /* public async Task<bool> isInFavourite(User user, PostType postType, int id)
         {
             if (user is null)
                 return false;

             switch (postType)
             {
                 case PostType.Company:
                     {
                         var items = user?.FavouriteCompanies?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(p => p.Trim())
                             .ToList()
                             .ConvertAll(int.Parse);

                         if (items is null)
                             return false;

                         var entities = items.Any(x => ((IList)items).Contains(id));

                         return entities;
                     }

                 case PostType.Contestant:
                     {
                         var items = user?.FavouriteContestants?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(p => p.Trim())
                             .ToList()
                             .ConvertAll(int.Parse);

                         if (items is null)
                             return false;

                         var entities = items.Any(x => ((IList)items).Contains(id));

                         return entities;
                     }

                 case PostType.Job:
                     {
                         var items = user?.FavouriteJobs?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(p => p.Trim())
                             .ToList()
                             .ConvertAll(int.Parse);

                         if (items is null)
                             return false;

                         var entities = items.Any(x => ((IList)items).Contains(id));
                         return entities;
                     }

             }
             return false;
         }*/
    }
}