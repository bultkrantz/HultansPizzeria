using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HultansPizzeria.Data;
using Microsoft.AspNetCore.Identity;
using HultansPizzeria.Models;

namespace HultansPizzeria.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var vm = new CheckoutViewModel
            {
                User = user
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            model.User = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                return View("OrderConfirmation", model);
            }
            return View(model);
        }
    }
}