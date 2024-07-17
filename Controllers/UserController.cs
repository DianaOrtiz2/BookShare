using System.Reflection.Emit;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata;
using System.Reflection;
using Microsoft.Win32;
using System.Globalization;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookShare.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookShare.Entities;



namespace BookShare.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;


    public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ApplicationDbContext context)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._context = context;
    }

    public IActionResult Registro()
    {
        var model = new RegistroViewModel
        {
            genderOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Male", Text = "Hombre" },
                new SelectListItem { Value = "Female", Text = "Mujer" },
                new SelectListItem { Value = "PreferNotToSay", Text = "Prefiero no decirlo" }
            }
        };
        return View(model);
        
    }  
      
   /*[AllowAnonymous]
    public IActionResult Registro ()
    {
        return View();
    }*/
    

    [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.genderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Hombre" },
                    new SelectListItem { Value = "Female", Text = "Mujer" },
                    new SelectListItem { Value = "PreferNotToSay", Text = "Prefiero no decirlo" }
                    
                };
                return View(model);
            }
            

            var user = new ApplicationUser() 
            {
                Email = model.Email,
                Fullname = model.FullName,
                BirthDate = model.BirthDate,
                gender = model.gender 
            };

            var result = await _userManager.CreateAsync(user, password: model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
            
        }


   [AllowAnonymous]
        public IActionResult Login(string message = null)
        {
            if (message is not null) {
                ViewData["message"] = message;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Nombre de usuario o password incorrecto");
                return View(model);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }

    

}