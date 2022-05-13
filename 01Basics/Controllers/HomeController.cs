using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _01Basics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public async Task<IActionResult> Auth()
        {
            var grandmaClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , "Bob"),
                new Claim(ClaimTypes.Email, "Bob@gmail.com"),
                new Claim("Grandma.Says" , "Goood 👌!!")
            };

            var grandmaIdentity = new ClaimsIdentity(grandmaClaims,"Grandma Identity");
            


            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , "Bob"),
                new Claim(ClaimTypes.Email, "Bob@gmail.com"),
                new Claim(ClaimTypes.HomePhone, "054605646046")
            };

            var authIdentity = new ClaimsIdentity(authClaims, "Grandma Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity, authIdentity });


            await HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

    }
}
