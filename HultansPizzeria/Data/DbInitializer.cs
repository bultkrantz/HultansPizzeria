﻿using HultansPizzeria.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HultansPizzeria.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (dbContext.Users.ToList().Count == 0)
            {
                var aUser = new ApplicationUser()
                {
                    UserName = "test@student.se",
                    Email = "test@student.se"
                };

                var r = await userManager.CreateAsync(aUser, "Pa$$word1");
                var adminRole = new IdentityRole { Name = "Admin" };
                var roleResult = await roleManager.CreateAsync(adminRole);

                var adminUser = new ApplicationUser
                {
                    UserName = "admin@test.se",
                    Email = "admin@test.se",
                    FirstName = "Marcus",
                    LastName = "Hultkrantz",
                    Address = "Kungsgatan 24A",
                    EntryCode = "8888",
                    Floor = "2",
                    ApartmentNumber = "13"
                };
                var adminResult = await userManager.CreateAsync(adminUser, "Pa$$word1");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
             

            if (dbContext.Dishes.ToList().Count == 0)
            {

                //Ingredients
                var cheese = new Ingredient { Name = "Cheese", Price = 5 };
                var tomatoe = new Ingredient { Name = "Tomatoe", Price = 5 };
                var ham = new Ingredient { Name = "Ham", Price = 5 };
                var mincedMeat = new Ingredient { Name = "Minced meat", Price = 5 };
                var shrimp = new Ingredient { Name = "Shrimp", Price = 5 };
                var feta = new Ingredient { Name = "Feta", Price = 5 };
                var bacon = new Ingredient { Name = "Bacon", Price = 5 };
                var chicken = new Ingredient { Name = "Chicken", Price = 5 };
                var pineapple = new Ingredient { Name = "Pineapple", Price = 5 };

                //Categories
                var pizza = new Category { Name = "Pizza" };
                var pasta = new Category { Name = "Pasta" };
                var sallad = new Category { Name = "Sallad" };

                //Pizzas
                var capricciosa = new Dish { Name = "Capricciosa", CategoryId = 1, Price = 79, ImageUrl = "../images/pizzas/Capricciosa.jpg" };
                var margaritha = new Dish { Name = "Margaritha", CategoryId = 1, Price = 69, ImageUrl = "../images/pizzas/Margaritha.jpg" };
                var hawaii = new Dish { Name = "Hawaii", CategoryId = 1, Price = 79, ImageUrl = "../images/pizzas/Hawaii.jpg" };

                //Pastas
                var alfredo = new Dish { Name = "Alfredo", CategoryId = 2, Price = 69, ImageUrl = "../images/pastas/alfredo.jpg" };
                var bolognese = new Dish { Name = "Bolognese", CategoryId = 2, Price = 69, ImageUrl = "../images/pastas/bolognese.jpg" };
                var carbonara = new Dish { Name = "Carbonara", CategoryId = 2, Price = 79, ImageUrl = "../images/pastas/carbonara.jpg" };

                //Sallads
                var chickenSallad = new Dish { Name = "Chicken", CategoryId = 3, Price = 69, ImageUrl = "../images/sallads/chicken.jpg" };
                var shrimpSallad = new Dish { Name = "Shrimp", CategoryId = 3, Price = 69, ImageUrl = "../images/sallads/shrimp.jpg" };
                var vegetarianSallad = new Dish { Name = "Vegetarian", CategoryId = 3, Price = 79, ImageUrl = "../images/sallads/vegetarian.jpg" };

                //DishIngredients
                var capricciosaCheese = new DishIngredient { Ingredient = cheese, Dish = capricciosa };
                var capricciosaTomatoe = new DishIngredient { Ingredient = tomatoe, Dish = capricciosa };
                var capricciosaHam = new DishIngredient { Ingredient = ham, Dish = capricciosa };
                var margarithaCheese = new DishIngredient { Ingredient = cheese, Dish = margaritha };
                var margarithaTomatoe = new DishIngredient { Ingredient = tomatoe, Dish = margaritha };
                var hawaiiCheese = new DishIngredient { Ingredient = cheese, Dish = hawaii };
                var hawaiiTomatoe = new DishIngredient { Ingredient = tomatoe, Dish = hawaii };
                var hawaiiPineapple = new DishIngredient { Ingredient = pineapple, Dish = hawaii };

                var alfredoChicken = new DishIngredient { Ingredient = chicken, Dish = alfredo };
                var bologneseMeat = new DishIngredient { Ingredient = mincedMeat, Dish = bolognese };
                var carbonarabacon = new DishIngredient { Ingredient = bacon, Dish = carbonara };

                var chickenSalladChicken = new DishIngredient { Ingredient = chicken, Dish = chickenSallad };
                var shrimpSalladShrimp = new DishIngredient { Ingredient = shrimp, Dish = shrimpSallad };
                var vegetarianSalladFeta = new DishIngredient { Ingredient = feta, Dish = vegetarianSallad };

                capricciosa.DishIngredients = new List<DishIngredient>();
                margaritha.DishIngredients = new List<DishIngredient>();
                hawaii.DishIngredients = new List<DishIngredient>();
                alfredo.DishIngredients = new List<DishIngredient>();
                bolognese.DishIngredients = new List<DishIngredient>();
                carbonara.DishIngredients = new List<DishIngredient>();
                chickenSallad.DishIngredients = new List<DishIngredient>();
                shrimpSallad.DishIngredients = new List<DishIngredient>();
                vegetarianSallad.DishIngredients = new List<DishIngredient>();

                //Add DishIngredients to dish
                alfredo.DishIngredients.Add(alfredoChicken);
                bolognese.DishIngredients.Add(bologneseMeat);
                carbonara.DishIngredients.Add(carbonarabacon);

                chickenSallad.DishIngredients.Add(chickenSalladChicken);
                shrimpSallad.DishIngredients.Add(shrimpSalladShrimp);
                vegetarianSallad.DishIngredients.Add(vegetarianSalladFeta);

                capricciosa.DishIngredients.Add(capricciosaCheese);
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaTomatoe);
                margaritha.DishIngredients.Add(margarithaCheese);
                margaritha.DishIngredients.Add(margarithaTomatoe);
                hawaii.DishIngredients.Add(hawaiiCheese);
                hawaii.DishIngredients.Add(hawaiiPineapple);
                hawaii.DishIngredients.Add(hawaiiTomatoe);


                //Add seed to inMem database
                await dbContext.Category.AddRangeAsync(pizza, pasta, sallad);

                await dbContext.Ingredients.AddRangeAsync(cheese, tomatoe, ham, mincedMeat, shrimp, chicken, bacon, feta, pineapple);

                await dbContext.DishIngredient.AddRangeAsync(capricciosaCheese, capricciosaTomatoe, capricciosaHam,
                   alfredoChicken, carbonarabacon, bologneseMeat, chickenSalladChicken, shrimpSalladShrimp,
                   vegetarianSalladFeta, hawaiiCheese, hawaiiPineapple, hawaiiTomatoe, margarithaCheese, margarithaTomatoe);

                await dbContext.Dishes.AddRangeAsync(capricciosa, margaritha, hawaii, alfredo, bolognese, carbonara,
                     chickenSallad, shrimpSallad, vegetarianSallad);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
