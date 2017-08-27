﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HultansPizzeria.Models;
using HultansPizzeria.Data;
using Microsoft.EntityFrameworkCore;
using HultansPizzeria.Services;

namespace HultansPizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ICartService _cartService;

        public HomeController(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient).ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
