using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HultansPizzeria.Data;

namespace HultansPizzeria.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
            public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
     
        public IActionResult Index()
        {
            return View();
        }

     public IActionResult Ingredients()
        {
            return View(_context.Ingredients.ToList());
        }

        public IActionResult Users()
        {
            return View(_context.Users.ToList());
        }
    }
}