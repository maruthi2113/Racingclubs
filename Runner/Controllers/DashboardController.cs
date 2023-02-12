using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Runner.Data;
using Runner.Helpers;
using Runner.Models;
using Runner.Models.ViewModels;
using Runner.Repository.Interface;

namespace Runner.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _repo;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPhotoService _photoService;
        public DashboardController(IDashboardRepository repo,IHttpContextAccessor sc,IPhotoService ps)
        {
            _photoService = ps; 
            _contextAccessor=sc;
            _repo = repo; 
          }
        public async Task<IActionResult> Index()
        {
            var userRaces=await _repo.GetAllUserRaces();
            var userClubs=await _repo.GetAllUserClubs();
            var dashboardviewmodel = new DashboardViewModel
            {
                Races = userRaces,
                Clubs = userClubs,
            };
            return View(dashboardviewmodel);  
        }

        private void MapUserEdit(AppUser user,EditUserDashBoardViewModel model,ImageUploadResult imageresult)
        {
            user.Id=model.Id;
            user.Pace=model.pace;
            user.Milage = model.Milage;
            user.ProfileImageUrl = imageresult.Url.ToString();
            user.City=model.City;
            user.State=model.State;


        }
        public async Task<IActionResult> UserProfile()
        {
            var currentuserId = _contextAccessor.HttpContext.User.GetUserId();
            
               
            var user=await _repo.GetUserById(currentuserId);
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfile(AppUser model)
        {
            return View(model);
        }
        public async Task<IActionResult> EditUserProfile()
        {

            // to get userid string 
            var curuserid=_contextAccessor.HttpContext.User.GetUserId();// user id string
            
            //getting user by string id
            var user=await _repo.GetUserById(curuserid);//user
            if (user == null)
            return View("Error");

            var edituserviewmodel = new EditUserDashBoardViewModel
            {
                Id=curuserid,
                pace=user.Pace,
                Milage=user.Milage,
                ImageProfile=user.ProfileImageUrl,
                City=user.City,
                State=user.State,

            };

            return View(edituserviewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserProfile(EditUserDashBoardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Failed to Edit Profile");
                return View(model);
            }
            var user = await _repo.GetUserByIdNoTracing(model.Id);

            if(user.ProfileImageUrl==""||user.ProfileImageUrl==null)
            {
                var photoresult = await _photoService.AddPhotoAsync(model.Image);
                MapUserEdit(user, model, photoresult);
                _repo.Update(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("","could not delete photo");
                    return View(model);
                }
            }
            var photoresult1 = await _photoService.AddPhotoAsync(model.Image);

            MapUserEdit(user,model,photoresult1);
            _repo.Update(user);
            return RedirectToAction("Index", "Home");

        }
    }
}
