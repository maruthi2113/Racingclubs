using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Runner.Data;
using Runner.Helpers;
using Runner.Models;
using Runner.Models.ViewModels;
using Runner.Repository.Implements;
using Runner.Repository.Interface;

namespace Runner.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _repo;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor;
        public RaceController(IRaceRepository repo, IPhotoService photoService, IHttpContextAccessor contextAccessor)
        {
            _repo = repo;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }

        public  async Task<IActionResult> Index()
        {
           
            return View( await _repo.GetAll());
        }
        public async Task<IActionResult> ByPerson(string s)
        {
            var result = await _repo.GetByPerson(s);
            return View(result);
        }
        public async Task<IActionResult> FindByName(string s)
        {
            ViewData["byname"] = s;
           var result=await _repo.GetRacesByName(s);
            return View(result);
;       }
        public async Task<IActionResult> FindByCity(string s)
        {
            ViewData["cityname"] = s;
            var result = await _repo.GetRacesByCity(s);
            return View(result);

        }
        public async Task<IActionResult> Detail(int id)
        {

            var result = await _repo.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return View(result);
        }
        public IActionResult Create()
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var createraceViewModel = new CreateRaceViewModel
            {
                AppUserId = curUserId,
            };
            return View(createraceViewModel);
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Name = raceVM.Name,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId=raceVM.AppUserId,
                    Address = new Address
                    {
                        street = raceVM.Address.street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                        Country = raceVM.Address.Country
                    }

                };

                _repo.Add(race);
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Photo Upload failed");
            return View(raceVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var result = await _repo.GetByIdAsync(id);

            if (result == null)
                return NotFound();
            var raceVm = new EditRaceViewModel
            {
                Name = result.Name,
                Description = result.Description,
                AddressId = result.AddressId,
                Address = result.Address,
                URL = result.Image,
                RaceCategory = result.RaceCategory,
            };
            return View(raceVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVm)
        {
            if (!ModelState.IsValid)
            {
                return View("EditVM", raceVm);
            }
            var userClub = await _repo.GetByIdAsyncNoTracking(id);
            if (userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Cloud not delete the model");
                    return View(raceVm);
                }
                var photoresult = await _photoService.AddPhotoAsync(raceVm.Image);
                var race = new Race
                {
                    Id = id,
                    Name = raceVm.Name,
                    Image = photoresult.Url.ToString(),
                    Description = raceVm.Description,
                    AddressId = raceVm.AddressId,
                    Address = raceVm.Address,
                    RaceCategory = raceVm.RaceCategory
                };
                _repo.Update(race);
                return RedirectToAction("Index");
            }
            return View(raceVm);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            _repo.Delete(result);
            return RedirectToAction("Index");
        }
    }
}
