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
            return cartItemIngredients;
        }

        public void Update(CartItem cartItem)
        {
            RemoveFromCart(cartItem);
            Cart cart = GetCart();
            cart.lineCollection.Add(cartItem);
            SaveCart(cart);
        }
    }
}
