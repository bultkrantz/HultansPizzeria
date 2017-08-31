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

        public IActionResult Add(int dishId)
        {
            try
            {
                Cart cart = _cartService.GetCart();
                var dish = _context.Dishes.Include(d => d.DishIngredients)
                              .ThenInclude(di => di.Ingredient).FirstOrDefault(d => d.DishId == dishId);
                cart.AddItem(dish, 1);
                _cartService.SaveCart(cart);
            }
            catch (Exception)
            {
                throw;
            }
            return PartialView("_CartPartial");
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
            }
            catch (Exception)
            {
                throw;
            }

            return PartialView("_CartPartial");
        }

        public IActionResult Edit(Guid cartItemId)
        {
            Cart cart = _cartService.GetCart();
            var cartItem = cart.lineCollection.FirstOrDefault(c => c.CartItemId == cartItemId);            
            return PartialView("_CartModalPartial", cartItem);
        }

        public IActionResult SaveModification(Guid cartItemId, List<int> ingredients)
        {
            Cart cart = _cartService.GetCart();
            var cartItem = cart.lineCollection.FirstOrDefault(c => c.CartItemId == cartItemId);
            var newIngredients = _cartService.GetIngredients().Where(i => ingredients.Contains(i.IngredientId)).ToList();
            cartItem.Ingredients = newIngredients;
            _cartService.Update(cartItem);
            return PartialView("_CartPartial");
        }
    }
}