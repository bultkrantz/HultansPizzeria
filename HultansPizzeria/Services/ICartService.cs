using HultansPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HultansPizzeria.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(int dishId, string customerId);
        void RemoveFromCart(int dishId, string customerId);
        Task CreateOrderAsync(int customerId);
        Cart GetCart();
        void SaveCart(Cart cart);
    }
}
