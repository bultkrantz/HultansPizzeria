using System.Collections.Generic;
using System.Linq;

namespace HultansPizzeria.Models
{
    public class Cart
    {
        private List<CartItem> _lineCollection = new List<CartItem>();

        public virtual void AddItem(Dish dish, int quantity)
        {
            CartItem item = _lineCollection.Where(p => p.Dish.DishId == dish.DishId).FirstOrDefault();

            if (item == null)
            {
                _lineCollection.Add(new CartItem
                {
                    Dish = dish,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Dish dish) =>
       _lineCollection.RemoveAll(c => c.Dish.DishId == dish.DishId);

        public virtual int GetCartLine() => _lineCollection.Count();

        public class CartItem
        {
            public int CartLineID { get; set; }
            public Dish Dish { get; set; }
            public int Quantity { get; set; }
        }
    }
}
