using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Runner.Data;
using Runner.Helpers;
using Runner.Models;
using Runner.Models.ViewModels;
using Runner.Repository.Interface;

namespace Runner.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _repo;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _contextAccessor; 
        public ClubController(IClubRepository repo, IPhotoService photoService,IHttpContextAccessor a)
        {
           _contextAccessor = a;
            _repo = repo;
            _photoService = photoService;   
        }
        public async Task<IActionResult> Index()
        {
            
            return View( await _repo.GetAll());
        }
        public async Task<IActionResult> ByPerson(string id)
        {
            var result= await  _repo.GetByPerson(id);
            return View(result);
        }
        public async Task<IActionResult> FindByName(string s)
        {
            ViewData["byname"]=s;   
           var result= await  _repo.GetClubsByName(s);
            return View(result);
            
        }
        public async Task<IActionResult> Empty()
        {
            return View();
        }
        public async Task<IActionResult> FindByCity(string s)
        {
            ViewData["cityname"]= s;
            var result = await _repo.GetClubByCity(s);
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
            var curUserId = _contextAccessor.HttpContext?.User.GetUserId();
            var createclubViewModel = new CreateClubViewModel
            {
                AppUserId = curUserId,
            };
            return View(createclubViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Name = clubVM.Name,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId=clubVM.AppUserId,
                    Address = new Address
                    {
                        street=clubVM.Address.street,
                        City=clubVM.Address.City,
                        State=clubVM.Address.State,
                        Country=clubVM.Address.Country
                    }

                };

                _repo.Add(club);
              return   RedirectToAction("Index");
              
            }
            ModelState.AddModelError("","Photo Upload failed");
            return View(clubVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            
            if (result == null)
            return NotFound();
            var clubVm = new EditClubViewModel
            { Name = result.Name,
            Description=result.Description,
            AddressId=result.AddressId,
            Address=result.Address,
            URL=result.Image,
            ClubCategory=result.ClubCategory,
            };
            return View(clubVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EditClubViewModel clubVm)
        {
            if(!ModelState.IsValid)
            {
                return View("EditVM",clubVm);
            }
            var userClub=await _repo.GetByIdAsyncNoTracking(id);
            if(userClub!=null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("","Cloud not delete the model");
                    return View(clubVm);
                }
                var photoresult = await _photoService.AddPhotoAsync(clubVm.Image);
                var club = new Club
                {
                    Id = id,
                    Name = clubVm.Name,
                    Image = photoresult.Url.ToString(),
                    Description = clubVm.Description,
                    AddressId = clubVm.AddressId,
                    Address = clubVm.Address,
                    ClubCategory = clubVm.ClubCategory
                };
                _repo.Update(club);
                return RedirectToAction("Index");
            }
            return View(clubVm);
        }
       

        public async Task<IActionResult> Delete(int id)
        {
            var result = await  _repo.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return View(result);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var result =await  _repo.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            _repo.Delete(result);
            return RedirectToAction("Index");
        }
    }
}
