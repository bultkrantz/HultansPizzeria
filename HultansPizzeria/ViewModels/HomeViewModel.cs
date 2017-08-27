using HultansPizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HultansPizzeria.ViewModels
{
    public class HomeViewModel
    {
        public List<Dish> Dishes { get; set; }
        public Cart Cart { get; set; }
    }
}
