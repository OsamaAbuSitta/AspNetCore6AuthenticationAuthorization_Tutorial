using _02IdentityExample.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02IdentityExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ApplicationDbContext applicationDbContext,
                    UserManager<IdentityUser> userManager,
                    SignInManager<IdentityUser> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            
            if (user == null)
                throw new Exception();

            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, password);

            if (!checkPasswordResult)
                throw new Exception("Invalid Credintional 💀💀");


            await _signInManager.SignInAsync(user, isPersistent: false);

            


            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            //var user = await _userManager.FindByNameAsync(username);
            var createUserResult = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = username
            }, password);

            if (!createUserResult.Succeeded)
                throw new Exception(string.Join(",", createUserResult.Errors));

            return View();
        }
    }
}
