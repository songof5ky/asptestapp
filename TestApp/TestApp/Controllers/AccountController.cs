using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestApp.Models;
using TestApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TestApp.Controllers
{
    public class AccountController : Controller
    {
        private TestContext db;
        public AccountController(TestContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.username == model.username && u.password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.username);  
                   
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.username == model.username);
                if (user == null)
                {
                    
                    db.Users.Add(new User { username = model.username, password = model.Password ,first_name=model.first_name,last_name=model.last_name});
                    await db.SaveChangesAsync();

                    await Authenticate(model.username);  

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
             
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
             
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
             
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public async Task<IActionResult> UserList()
        {
            return View(await db.Users.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            
            if (ModelState.IsValid)
            {
                User user1 = await db.Users.FirstOrDefaultAsync(u => u.username == User.Identity.Name);
                if (user1 != null)
                {
                    ProfileModel user = new ProfileModel();
                    user.username = user1.username;
                    user.first_name = user1.first_name;
                    user.last_name = user1.last_name;
                    user.Password = user1.password;
                  
                    
                        return View(user);

                     
                }
                return NotFound();
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(ProfileModel user)
        {
            User user1 = await db.Users.FirstOrDefaultAsync(p => p.username== User.Identity.Name);
            if (user.first_name != null)
                user1.first_name = user.first_name;
            if (user.last_name != null)
                user1.last_name = user.last_name;

            if (user.Password != null)
                user1.password = user.Password;
            db.Users.Update(user1);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}