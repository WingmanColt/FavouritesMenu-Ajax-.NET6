using FavouriteMenuAjax.Models;
using FavouriteMenuAjax.Services;
using FavouriteMenuAjax.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FavouriteMenuAjax.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IFavoritesService _favoriteService;
        public List<Entity> listOfEntities { get; set; }
        public List<Entity> listOfEntities2 { get; set; }


        public HomeController(UserManager<User> userManager, IFavoritesService favoriteService)
        {
            _userManager = userManager;
            _favoriteService = favoriteService;

            listOfEntities = new List<Entity>();
            listOfEntities2 = new List<Entity>();


            listOfEntities.Add(new Entity { Id = 1, Name = "Test 1" });
            listOfEntities.Add(new Entity { Id = 2, Name = "Test 2" });
            listOfEntities.Add(new Entity { Id = 3, Name = "Test 3" });
        }



        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FavoritesViewModel>> UpdateFavourite(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Errors/AccessDenied");
            }


            var existed = await _favoriteService.UpdateFavourite(user, id.ToString()).ConfigureAwait(false);

            if(existed)
                listOfEntities2.Remove(listOfEntities.FirstOrDefault(x => x.Id == id));
            else
                listOfEntities2.Add(listOfEntities.FirstOrDefault(x => x.Id == id));

            var model = new FavoritesViewModel()
            {
                Count = _favoriteService.GetFavouriteByCount(user),
                Entity = listOfEntities.FirstOrDefault(j => j.Id == id),
                isExisted = existed
            };

            return model;
        }

        [Authorize]
        public async Task<ActionResult> RemoveFavourites(string returnUrl)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await _favoriteService.RemoveAllFavourites(user).ConfigureAwait(false);

            if (!String.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Index(HeaderMenuViewModel viewModel)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is not null)
            {
                var chackUserPorpertyForFavourites = _favoriteService.GetFavouriteBy(listOfEntities, user);

                if (chackUserPorpertyForFavourites.Any())
                {
                    viewModel.Favourites = chackUserPorpertyForFavourites;
                    viewModel.FavouritesCount = chackUserPorpertyForFavourites.Count();
                }

                viewModel.Entities = listOfEntities.Select(x => new Entity
                {
                    Id = x.Id,
                    Name = x.Name,
                    isInFavourites = _favoriteService.isInFavourite(user, x.Id.ToString())
                }).ToList();
            }
            return View(viewModel);
        }

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

/*
public async Task<IActionResult> Index(int currentPage = 1, string SearchString = null, string LocationId = null)
{
    var user = await _userManager.GetUserAsync(User);

    var entity = _jobsService.GetAllAsNoTracking()
        .Select(x => new Jobs
        {
            Id = x.Id,
            Name = x.Name,
            WorkType = x.WorkType,
            LocationId = x.LocationId,
            CreatedOn = x.CreatedOn,
            PosterID = x.PosterID,
            RatingVotes = x.RatingVotes,
            Promotion = x.Promotion,
            Adress = x.Adress,
            JobType = x.JobType,
            MinSalary = x.MinSalary,
            MaxSalary = x.MaxSalary,
            SalaryType = x.SalaryType,
            CompanyId = x.CompanyId,
            CategoryId = x.CategoryId,
            TagsId = x.TagsId,
            CompanyLogo = x.CompanyLogo,
            isInFavourites = _favoriteService.isInFavourite(user, x.Id.ToString())
        });
}*/
/*
 [Authorize]
[HttpPost]
public async Task<ActionResult<FavoritesViewModel>> UpdateFavourite(int id)
{
    var user = await _userManager.GetUserAsync(User);
    if (user == null)
    {
        return Redirect("/Identity/Account/Errors/AccessDenied");
    }

    var entity = await _jobsService.GetByIdAsync(id);
    if (entity == null)
    {
        return RedirectToAction("NotFound", "Home");
    }

   var existed = await _favoriteService.UpdateFavourite(user, id.ToString()).ConfigureAwait(false);

    var model = new FavoritesViewModel()
    {
       JobCount = _favoriteService.GetFavouriteByCount(user),
       Job = entity,
       isExisted = existed
    };

    return model;
}
*/