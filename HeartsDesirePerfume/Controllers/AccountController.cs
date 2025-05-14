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
        public IActionResult Loginform()
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);

                return View("Loginform");
            }
            var viewModel = new AccountLoginForm();
            return View(viewModel);
        }
    
        [HttpPost]
        public async Task<IActionResult>Register(CustomerRegister customerRegister)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);

                return View("Loginform",customerRegister);
            }

            var existingUser = await _userManager.FindByEmailAsync(customerRegister.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "This email is already registered");
                return View("Loginform",customerRegister);
            }

            var existingPhoneNumber = await _userManager.Users.FirstOrDefaultAsync(temp => temp.PhoneNumber == customerRegister.Phone);

            if (existingPhoneNumber != null)
            {
                ModelState.AddModelError("Phone", "Phone number already exist");
                return View("Loginform",customerRegister);
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
            return View("Index",customerRegister);

        }


        [HttpPost]
         public async Task<IActionResult> Login(CustomerLogin customerLogin,string? ReturnUrl)
         {

            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);

                return View("Loginform",customerLogin);
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

            ModelState.AddModelError("Loginform", "Invalid Email or Password");
            return View("Loginform",customerLogin);

        }
         

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }

        [HttpGet]
        public IActionResult ForgetPassword()

        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(CustomerUpdate customerUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(customerUpdate);
            }

            var user = await _userManager.FindByEmailAsync(customerUpdate.EmailID);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email ID is not registered.");
                return View(customerUpdate);
            }

            // Generate password reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Reset the password using the token
            var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, customerUpdate.Password!);

            if (resetResult.Succeeded)
            {
                ViewBag.Message = "Password updated successfully.";
                return RedirectToAction("index", "Home"); // redirect or show success message
            }
            else
            {
                foreach (var error in resetResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(customerUpdate);
            }
        }


    }
}