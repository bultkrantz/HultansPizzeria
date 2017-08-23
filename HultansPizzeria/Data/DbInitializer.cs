using HultansPizzeria.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HultansPizzeria.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var aUser = new ApplicationUser()
            {
                UserName = "test@student.se",
                Email = "test@student.se"
            };

            var r = userManager.CreateAsync(aUser, "Pa$$word1").Result;
            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole);

            var adminUser = new ApplicationUser
            {
                UserName = "admin@test.se",
                Email = "admin@test.se"
            };
            var adminResult = userManager.CreateAsync(adminUser, "Pa$$word1").Result;
            userManager.AddToRoleAsync(adminUser, "Admin");

            if (dbContext.Dishes.ToList().Count == 0)
            {
                var cheese = new Ingredient { Name = "Cheese" };
                var tomatoe = new Ingredient { Name = "Tomatoe" };
                var ham = new Ingredient { Name = "Ham" };

                var capricciosa = new Dish { Name = "Capricciosa", Price = 79, ImageUrl = "../images/pizzas/Capricciosa.jpg" };
                var margaritha = new Dish { Name = "Margaritha", Price = 69, ImageUrl = "../images/pizzas/Margaritha.jpg" };
                var hawaii = new Dish { Name = "Hawaii", Price = 79, ImageUrl = "../images/pizzas/Hawaii.jpg" };

                var capricciosaCheese = new DishIngredient { Ingredient = cheese, Dish = capricciosa };
                var capricciosaTomatoe = new DishIngredient { Ingredient = tomatoe, Dish = capricciosa };
                var capricciosaHam = new DishIngredient { Ingredient = ham, Dish = capricciosa };

                capricciosa.DishIngredients = new List<DishIngredient>();

                capricciosa.DishIngredients.Add(capricciosaCheese);
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaTomatoe);

                dbContext.Ingredients.AddRange(cheese, tomatoe, ham);
                dbContext.DishIngredient.AddRange(capricciosaCheese, capricciosaTomatoe, capricciosaHam);
                dbContext.Dishes.AddRange(capricciosa, margaritha, hawaii);

                dbContext.SaveChanges();
            }
        }
    }
}
