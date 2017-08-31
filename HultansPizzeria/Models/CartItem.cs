using System;
using System.Collections.Generic;

namespace HultansPizzeria.Models
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public List<CartItemIngredient> Ingredients { get; set; }
    }
}
