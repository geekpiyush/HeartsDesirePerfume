using Entities.IdentityEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTO;
using System.Threading.Tasks;

namespace HeartsDesireLuxury.Controllers
{
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
                await _signInManager.SignInAsync(user, true);

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


    }
}
