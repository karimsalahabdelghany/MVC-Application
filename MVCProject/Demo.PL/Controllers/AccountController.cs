using System.Threading.Tasks;
using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
			_signInManager = signInManager;
		}
        //Register
        // BaseUrl/Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        #region Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) //server side validation
            {
                var User = new ApplicationUser
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,

                };
                var Result = await _userManager.CreateAsync(User, model.Password);

                if (Result.Succeeded)
                {
                   return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
			return View(model);
			
            


        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public async Task< IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
				var User = await _userManager.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					//login
					var Result = await _userManager.CheckPasswordAsync(User, model.Password);
					if (Result)
					{
						var LoginResult = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (LoginResult.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}


					}
					else
					{
						ModelState.AddModelError(string.Empty, "Password isn't correct");
					}

				}
				else
					ModelState.AddModelError(string.Empty, "Password Does't Exsits");
			}
            return View(model);
        }
        #endregion
        #region signout
        public async new Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
        #region forgetpassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
               
                var User =await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    //Valid for only one time for this user
                    var token =await _userManager.GeneratePasswordResetTokenAsync(User);
                    //https://localhost:44352/Account/ResetPassword?email=karimabdelghany73@gmail.com?token=bhvbuyfyrdtesdklmkhjgygfytdreweq
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = User.Email,Token = token },Request.Scheme);
                    
                        
                    
                    //send Virfication message [email] to email
                    var email = new Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = ResetPasswordLink
					};
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
				}
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Does't Exiest");
                }
            }
            return View("ForgetPassword",model);
            
        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        #endregion
        #region Reset Password
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"]= token;
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;
                var user = await _userManager.FindByEmailAsync(email);

               var Result =  await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                
                else
                    foreach(var error in Result.Errors)
                        ModelState.AddModelError(string.Empty,error.Description );
			}
            return View(model);
            
            
        }
        #endregion
    }


    //P@ssw0rd
}
