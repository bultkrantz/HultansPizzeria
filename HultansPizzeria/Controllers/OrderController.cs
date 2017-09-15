using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HultansPizzeria.Data;
using Microsoft.AspNetCore.Identity;
using HultansPizzeria.Models;
using HultansPizzeria.Services;

namespace HultansPizzeria.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICartService cartService)
        {
            _context = context;
            _userManager = userManager;
            _cartService = cartService;
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

            var vm = new OrderConfirmationViewModel
            {
                Address = model.Address,
                ApartmentNumber = model.ApartmentNumber,
                Floor = model.Floor,
                EntryCode = model.EntryCode,
                DeliveryMethod = model.DeliveryMethod,
                PaymentMethod = model.PaymentMethod,
                Cart = _cartService.GetCart()
            };
            
            if (ModelState.IsValid)
            {
                return View("OrderConfirmation", vm);
            }
            return View(model);
        }
    }
}