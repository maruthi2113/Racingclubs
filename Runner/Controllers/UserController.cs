using Microsoft.AspNetCore.Mvc;
using Runner.Models.ViewModels;
using Runner.Repository.Interface;

namespace Runner.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _user;
        public UserController(IUserRepository user)
        {
            _user = user;
        }
        [HttpGet("users")]
        public async  Task<IActionResult> Index()
        {
            var result = await _user.GetAllUsers();
            List<UserViewModel> res = new List<UserViewModel>();
            foreach(var user in result)
            {
                if (user == null)
                    return View(new UserViewModel());
                var userViewModel = new UserViewModel
                {
                    Id=user.Id,
                    UserName=user.UserName,
                    pace=user.Pace,
                    Milage=user.Milage,
                    ProfileImageUrl=user.ProfileImageUrl
                };
                res.Add(userViewModel);
            }
            return View(res);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user=await _user.GetUserById(id);
            var userviewmodel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                pace = user.Pace,
                Milage = user.Milage,
                ProfileImageUrl=user.ProfileImageUrl
            };
            return View(userviewmodel);
        }
    }
}
