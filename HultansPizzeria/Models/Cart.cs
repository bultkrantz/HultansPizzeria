using HultansPizzeria.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HultansPizzeria.Models
{
    public class Cart
    {
        public List<CartItem> lineCollection = new List<CartItem>();

        public virtual void AddItem(Dish dish, int quantity)
        {
            Cart cart = this;
            var ingredients = new List<CartItemIngredient>();
            dish.DishIngredients.ForEach(di => ingredients.Add(new CartItemIngredient
            {
                IngredientId = di.IngredientId,
                Name = di.Ingredient.Name
            }));

            lineCollection.Add(new CartItem()
            {
                CartItemId = Guid.NewGuid(),
                DishId = dish.DishId,
                Name = dish.Name,
                Price = dish.Price,
                Ingredients = ingredients
            });
           
        }

        public virtual void RemoveLine(Dish dish) =>
       lineCollection.RemoveAll(c => c.DishId == dish.DishId);

        public virtual int GetCartLine() => lineCollection.Count();

        public class CartItem
        {
            public Guid CartItemId { get; set; }
            public int DishId { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public List<CartItemIngredient> Ingredients { get; set; }
        }

        public class CartItemIngredient
        {
            public int IngredientId { get; set; }
            public string Name { get; set; }
        }
    }
   
}
