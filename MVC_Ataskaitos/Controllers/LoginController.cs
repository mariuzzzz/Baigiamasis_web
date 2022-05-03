using MVC_Ataskaitos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ClassDataLibrary.Logic.Ataskaitos;

namespace MVC_Ataskaitos.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(string login, string password)
        {
            userModel users = new userModel();

            var user = LoadUsers(login, password);
            if(user == null || user.Count() == 0)
            {
                users.errorMessage = "Wrong credentials";
                return View("Index", users);
            }
            else
            {
                Session["ID"] = users.ID;
                Session["type"] = users.type;
                foreach (var row in user)
                {
                    //TempData["mydata"] = row.type;
                    Session["type"] = row.type;
                }
                return RedirectToAction("ViewData", "Home");

            }
            //return View();
        }
    }
}