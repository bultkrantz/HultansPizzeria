using System.Collections.Generic;

namespace HultansPizzeria.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
    }
}
