using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;

namespace TransGuzman_UI.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            string username = user.Username;
            string password = user.Password;

            IActionResult result = null;
            if (!ModelState.IsValid)
            {
                result = View(user);
            } 
            else
            {
                result = RedirectToAction("Index", "Home");
            }

            return result;
        }
    }
}
