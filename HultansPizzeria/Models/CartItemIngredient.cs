using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HultansPizzeria.Models
{
    public class CartItemIngredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public bool AddToDish { get; set; }
    }
}
