using Entities.IdentityEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceContracts.DTO;
using System.Threading.Tasks;
using Entities.Enums;
namespace HeartsDesireLuxury.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
       private readonly RoleManager<ApplicationUserRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationUserRole> roleManager  )
        {
            _userManager = userManager;
            _signInManager = signInManager;
           _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()

        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(CustomerRegister customerRegister)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage);
        //        return View(customerRegister);
        //    }

        //    var existingUser = await _userManager.FindByEmailAsync(customerRegister.Email);
        //    if (existingUser != null)
        //    {
        //        ModelState.AddModelError("Email", "This email is already registered");
        //        return View(customerRegister);
        //    }

        //    // Generate OTP
        //    var otp = new Random().Next(100000, 999999).ToString();

        //    // TODO: Send OTP via SMS or Email (for now, store in TempData)
        //    TempData["OTP"] = otp;
        //    TempData["RegisterData"] = JsonConvert.SerializeObject(customerRegister);

        //    // Send OTP via Fast2SMS
        //    await _smsService.SendOtpAsync(customerRegister.Phone, otp);

        //    return RedirectToAction("VerifyOtp");
        //}

         
        [HttpPost]
        public async Task<IActionResult>Register(CustomerRegister customerRegister)
        {
            if (ModelState.IsValid == false)
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

            var existingPhoneNumber = await _userManager.Users.FirstOrDefaultAsync(temp => temp.PhoneNumber == customerRegister.Phone);

            if (existingPhoneNumber != null)
            {
                ModelState.AddModelError("Phone", "Phone number already exist");
                return View(customerRegister);
            }

            ApplicationUser user = new ApplicationUser() { Email = customerRegister.Email, PhoneNumber = customerRegister.Phone, UserName = customerRegister.Email, CustomerName = customerRegister.CustomerName };

            IdentityResult result = await _userManager.CreateAsync(user, customerRegister.Password);

            if (result.Succeeded)
            {
                //check status of select option

                if(customerRegister.UserType == UserTypeOptions.Admin)
                {
                    //create admin role

                   if(await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationUserRole applicationUserRole = new ApplicationUserRole() { Name= UserTypeOptions.Admin.ToString() };

                       await _roleManager.CreateAsync(applicationUserRole);
                    }

                    //add the new user into admin role

                   await _userManager.AddToRoleAsync(user,UserTypeOptions.Admin.ToString());
                }
                else
                {
                    ApplicationUserRole applicationUserRole = new ApplicationUserRole() { Name = UserTypeOptions.User.ToString() };

                    await _roleManager.CreateAsync(applicationUserRole);
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }
                    await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home"); 
            }


            else
            {
                foreach (IdentityError error in result.Errors)
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
                    ApplicationUser user = await _userManager.FindByEmailAsync(customerLogin.Email);
                    
                    if(user != null)
                    {
                        if(await _userManager.IsInRoleAsync(user,UserTypeOptions.Admin.ToString()))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                    }

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
                    ApplicationUser user = await _userManager.FindByEmailAsync(customerLogin.Email);

                    if (user != null)
                    {
                        if (await _userManager.IsInRoleAsync(user, UserTypeOptions.Admin.ToString()))
                        {
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        }
                    }
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
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