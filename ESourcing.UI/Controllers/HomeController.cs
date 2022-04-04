using ESourcing.Core.Entities;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESourcing.UI.Controllers
{
    public class HomeController : Controller
    {
        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel,string? returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(loginModel.Email);
                if(user != null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (result.Succeeded)
                       return LocalRedirect(returnUrl);
                    else
                    {
                        ModelState.AddModelError(string.Empty, "EMail address or Password is not valid.");
                    }
                }
                
            }
            return View(loginModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(AppUserViewModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                AppUser usr= new AppUser();
                usr.FirstName = signUpModel.FirstName;
                usr.Email = signUpModel.Email;
                usr.LastName = signUpModel.LastName;
                usr.PhoneNumber = signUpModel.PhoneNumber;
                usr.UserName = signUpModel.UserName;
                if(signUpModel.UserSelectTypeId ==1)
                {
                    usr.IsBuyer = true;
                    usr.IsSeller = false;
                }
                else
                {
                    usr.IsBuyer = false;
                    usr.IsSeller = true;
                }
                var result = await _userManager.CreateAsync(usr, signUpModel.Password);
                if(result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach(IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(signUpModel);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
