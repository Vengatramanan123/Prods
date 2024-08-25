using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prods.Data;
using Prods.Models;

namespace Prods.Controllers
{

    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _user;
        public LoginController(ApplicationDbContext user)
        {
            _user = user;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginSignup loginSignup)
        {
            if (ModelState.IsValid)
            {
                var user = _user.LoginSignups.FirstOrDefault(u => u.Email == loginSignup.Email && u.Password == loginSignup.Password);
                if(user != null)
                {
                    HttpContext.Session.SetString("Username",user.UserName);
                    HttpContext.Session.SetString("Role", user.Role);
                    HttpContext.Session.SetInt32("Id", user.UserId);
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(loginSignup);
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(LoginSignup loginSignup)
        {
            if (ModelState.IsValid)
            {
                
                var existingUser = _user.LoginSignups.FirstOrDefault(u => u.Email == loginSignup.Email);
                if (existingUser == null)
                {
                    
                    _user.LoginSignups.Add(new LoginSignup
                    {
                        Email = loginSignup.Email,
                        Password = loginSignup.Password,
                        UserName = loginSignup.UserName,
                        PhoneNumber = loginSignup.PhoneNumber,
                        Role = loginSignup.Role
                    });
                    _user.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(loginSignup);
        }
    }
}
