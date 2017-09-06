using System;
using System.Threading.Tasks;
using HultansPizzeria.Models;
using Microsoft.AspNetCore.Http;
using HultansPizzeria.Extensions;
using System.Collections.Generic;
using static HultansPizzeria.Models.Cart;
using HultansPizzeria.Data;
using System.Linq;

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
            ingredients.ForEach(i => cartItemIngredients.Add(new CartItemIngredient { IngredientId = i.IngredientId, Name = i.Name }));
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

            var allDishes = _context.Dishes.ToList();
            var cartItemIds = cart.lineCollection.Select(c => c.DishId).ToList();
            var dishes = new List<Dish>();
            cartItemIds.ForEach(i => dishes.Add(allDishes.FirstOrDefault(d => d.DishId == i)));

            var dishIngredientCount = dishes.Sum(d => d.DishIngredients.Count());

            var cartIngredientCount = 0;
            cart.lineCollection.ForEach(c => cartIngredientCount += c.Ingredients.Count);

            for (int i = 0; i < cartIngredientCount; i++)
            {
                if (i + 1 > dishIngredientCount)
                {
                    total += 5;
                }
            }
            return total;
        }
    }
}
