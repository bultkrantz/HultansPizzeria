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
                Name = di.Ingredient.Name,
                Price = di.Ingredient.Price
            }));

            lineCollection.Add(new CartItem()
            {
                CartItemId = Guid.NewGuid(),
                DishId = dish.DishId,
                Name = dish.Name,
                Price = dish.Price,
                Ingredients = ingredients,
                Modified = false
            });

        }

        public virtual void RemoveLine(CartItem cartItem) =>
        lineCollection.RemoveAll(c => c.CartItemId == cartItem.CartItemId);

        public virtual int GetCartLine() => lineCollection.Count();


    }

}
