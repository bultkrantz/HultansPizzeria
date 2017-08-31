using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HultansPizzeria.Services;
using HultansPizzeria.Data;
using Microsoft.EntityFrameworkCore;
using HultansPizzeria.Models;
using System.Collections.Generic;

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

        public IActionResult Add(int dishId, int? quantity)
        {
            //TODO: Fix quantity in html
            quantity = 1;
            try
            {
                Cart cart = _cartService.GetCart();
                var dish = _context.Dishes.Include(d => d.DishIngredients)
                              .ThenInclude(di => di.Ingredient).FirstOrDefault(d => d.DishId == dishId);
                cart.AddItem(dish, 1);
                _cartService.SaveCart(cart);
                TempData["success"] = $"{quantity}st {dish.Name} lades till i varukorgen";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Home", null);

        }


        public List<CartItemIngredient> GetIngredients(Guid CartItemId)
        {
            return _cartService.GetCart().lineCollection.Find(c => c.CartItemId == CartItemId).Ingredients ?? new List<CartItemIngredient>();
        }

        public IActionResult Remove(Guid cartItemId)
        {
            try
            {
                Cart cart = _cartService.GetCart();
                var cartItem = cart.lineCollection.FirstOrDefault(c => c.CartItemId == cartItemId);
                _cartService.RemoveFromCart(cartItem);
                TempData["Success"] = $"{cartItem.Name} togs bort från varukorgen";
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction("Index", "Home", null);
        }

        public IActionResult Edit(Guid cartItemId)
        {
            Cart cart = _cartService.GetCart();
            var cartItem = cart.lineCollection.FirstOrDefault(c => c.CartItemId == cartItemId);            
            return PartialView("_Cart", cartItem);
        }

        public IActionResult SaveModification(Guid cartItemId, List<int> ingredients)
        {
            Cart cart = _cartService.GetCart();
            var cartItem = cart.lineCollection.FirstOrDefault(c => c.CartItemId == cartItemId);
            var newIngredients = _cartService.GetIngredients().Where(i => ingredients.Contains(i.IngredientId)).ToList();
            cartItem.Ingredients = newIngredients;
            _cartService.Update(cartItem);
            return RedirectToAction("Index", "Home", null);
        }
    }
}