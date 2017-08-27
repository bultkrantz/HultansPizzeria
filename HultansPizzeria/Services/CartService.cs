using System;
using System.Threading.Tasks;
using HultansPizzeria.Models;
using Microsoft.AspNetCore.Http;
using HultansPizzeria.Extensions;

namespace HultansPizzeria.Services
{
    public class CartService : ICartService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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

        public void RemoveFromCart(int dishId, string customerId)
        {
            throw new NotImplementedException();
        }

        public void SaveCart(Cart cart) => _httpContextAccessor.HttpContext.Session.SetJson("Cart", cart);
    }
}
