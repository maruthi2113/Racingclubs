using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Runner.Data;
using Runner.Models;
using Runner.Models.ViewModels;

namespace Runner.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }
            var user = await _userManager.FindByEmailAsync(login.Email); 
            
            if(user!=null)
            {
                // if user exists then check passswword
                var passcheck = await _userManager.CheckPasswordAsync(user,login.Password);
                if(passcheck)
                {
                    // to check password
                    var result = await _signInManager.PasswordSignInAsync(user,login.Password,false,false);
                      // to check wether logined or not
                    if (result.Succeeded)
                        return RedirectToAction("Index","Race");

                }
                // for incorrect password
                TempData["Error"] = "Incorrect Details";
                return View(login);
            }
            // when user not found
            TempData["Error"] = "Incorrect Details";
            return View(login);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user!=null)
            {
                TempData["Error"] = "this email address is taken already";
            }

            var newuser = new AppUser
            {
                Email = model.Email,
                 UserName=model.Email,
            };
            var newresponse= await _userManager.CreateAsync(newuser,model.Password);

            if (newresponse.Succeeded)
                await _userManager.AddToRoleAsync(newuser,UserRoles.User);
            return RedirectToAction("Index","Race");
        }

       

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Race");
        }
    }
}
