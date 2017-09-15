using System;
using System.Threading.Tasks;
using HultansPizzeria.Models;
using Microsoft.AspNetCore.Http;
using HultansPizzeria.Extensions;
using System.Collections.Generic;
using static HultansPizzeria.Models.Cart;
using HultansPizzeria.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HultansPizzeria.Services
{
    public class CartService : ICartService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ApplicationDbContext _context;
        public CartService(IHttpContextAccessor httpContext, ApplicationDbContext context)
        {
            _httpContextAccessor = httpContext;
            _context = context;
        }

        public Task AddToCartAsync(int dishId, string customerId)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrderAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCart()
        {
            Cart cart = _httpContextAccessor.HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        public void RemoveFromCart(CartItem cartItem)
        {
            Cart cart = GetCart();
            cart.lineCollection.RemoveAll(c => c.CartItemId == cartItem.CartItemId);
            SaveCart(cart);
        }

        public void SaveCart(Cart cart) => _httpContextAccessor.HttpContext.Session.SetJson("Cart", cart);

        public int CartTotal()
        {
            Cart cart = _httpContextAccessor.HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart.lineCollection.Count;
        }

        public List<CartItemIngredient> GetIngredients()
        {
            var cartItemIngredients = new List<CartItemIngredient>();
            var ingredients = _context.Ingredients.ToList();
            ingredients.ForEach(i => cartItemIngredients.Add(new CartItemIngredient { IngredientId = i.IngredientId, Name = i.Name, Price = i.Price }));
            return cartItemIngredients.OrderBy(i => i.Name).ToList();
        }

        public void Update(CartItem cartItem)
        {
            RemoveFromCart(cartItem);
            Cart cart = GetCart();
            cart.lineCollection.Add(cartItem);
            SaveCart(cart);
        }

        public int CalculateTotal()
        {
            Cart cart = GetCart();
            var total = 0;

            cart.lineCollection.ForEach(c => total += c.Price);
            var allDishes = _context.Dishes.Include(d => d.DishIngredients).ToList();
            var allCartItems = cart.lineCollection.ToList();

            foreach (var cartItem in allCartItems)
            {
                var originalIngredients = allDishes.FirstOrDefault(d => cartItem.DishId == d.DishId).DishIngredients.ToList();
                cartItem.Ingredients.ForEach(i => total = originalIngredients.Select(oi => oi.IngredientId).Contains(i.IngredientId) ? total : total += i.Price);
            }

            return total;
        }

        public int CalculateDishTotal(int dishId)
        {
            var total = 0;

            var cartItem = GetCart().lineCollection.FirstOrDefault(ci => ci.DishId == dishId);
            total = cartItem.Price;
            var originalIngredients = _context.Dishes.Include(d => d.DishIngredients).FirstOrDefault(d => d.DishId == dishId).DishIngredients.ToList();
            cartItem.Ingredients.ForEach(i => total = originalIngredients.Select(oi => oi.IngredientId).Contains(i.IngredientId) ? total : total += i.Price);
      
            return total;
        }

        public bool DishIsModified(int dishId)
        {
            var cartItem = GetCart().lineCollection.FirstOrDefault(ci => ci.DishId == dishId);
            var originalIngredients = _context.Dishes.Include(d => d.DishIngredients).FirstOrDefault(d => d.DishId == dishId).DishIngredients.ToList();
            return originalIngredients.Count != cartItem.Ingredients.Count;
        }
    }
}
