using HultansPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HultansPizzeria.Models.Cart;

namespace HultansPizzeria.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(int dishId, string customerId);
        void RemoveFromCart(CartItem cartItem);
        Task CreateOrderAsync(int customerId);
        Cart GetCart();
        void SaveCart(Cart cart);
        int CartTotal();
        List<CartItemIngredient> GetIngredients();
        void Update(CartItem cartItem);
        int CalculateTotal();
        int CalculateDishTotal(int dishId);
        bool DishIsModified(int dishId);
        void ClearCart();
    }
}
