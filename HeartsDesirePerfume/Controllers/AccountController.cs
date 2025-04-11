using Entities.IdentityEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CustomerRegister customerRegister)
        {
            if(ModelState.IsValid == false)
            { 
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);

                return View(customerRegister);
            }

            var existingUser = await _userManager.FindByEmailAsync(customerRegister.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered");
                return View(customerRegister);
            }

            ApplicationUser user = new ApplicationUser() { Email = customerRegister.Email, PhoneNumber = customerRegister.Phone, UserName = customerRegister.Email, CustomerName = customerRegister.CustomerName };

            IdentityResult result = await _userManager.CreateAsync(user,customerRegister.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user,false);

                return RedirectToAction("Index","Home");
            }


            else
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
            }
            return View(customerRegister);

        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
         public async Task<IActionResult> Login(CustomerLogin customerLogin,string? ReturnUrl)
         {

            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);

                return View(customerLogin);
            }


            if(customerLogin.KeepMeSignedin == true)
            {
                var keepmesignedin = await _signInManager.PasswordSignInAsync(customerLogin.Email, customerLogin.Password, isPersistent: true, lockoutOnFailure: false);

                if(keepmesignedin.Succeeded)
                {
                    if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    return RedirectToAction("index", "Home");
                }

            }



                var result = await _signInManager.PasswordSignInAsync(customerLogin.Email, customerLogin.Password, isPersistent: false, lockoutOnFailure: false);

                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                    
                    return RedirectToAction("Index", "Home");
                }

            ModelState.AddModelError("Login", "Invalid Email or Password");
            return View(customerLogin);

        }
         

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
           
        }

    }