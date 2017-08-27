using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HultansPizzeria.Services;
using HultansPizzeria.Data;
using HultansPizzeria.Models;
using HultansPizzeria.Extensions;

namespace HultansPizzeria.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private ICartService _cartService;

        public CartController(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IActionResult ReloadCart()
        {
            return ViewComponent("Cart");
        }

        public IActionResult Add(int dishId, int? quantity)
        {
            //TODO: Fix quantity in html
            quantity = 1;
            try
            {
                var cart = _cartService.GetCart();
                var dish = _context.Dishes.Find(dishId);
                cart.AddItem(dish, quantity.Value);
                _cartService.SaveCart(cart);
                TempData["success"] = $"{quantity}st {dish.Name} lades till i varukorgen";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Home", null);

        }

        public void Remove(int dishId, string customerId)
        {
            //var user = GetUser(customerId);
            //var customer = GetCustomer(user.CustomerId);
            try
            {
                var cart = _cartService.GetCart();
                var dish = _context.Dishes.Find(dishId);
                cart.RemoveLine(dish);
                _cartService.SaveCart(cart);
                TempData["success"] = $"{dish.Name} borttagen från varukorgen";
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}