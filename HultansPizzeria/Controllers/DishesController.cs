using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HultansPizzeria.Data;
using HultansPizzeria.Models;
using Microsoft.AspNetCore.Authorization;
using HultansPizzeria.ViewModels;
using System.Collections.Generic;

namespace HultansPizzeria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DishesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dishes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.ToListAsync());
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            var vm = new ManageDishViewModel
            {
                Ingredients = _context.Ingredients.ToList()
            };
            return View(vm);
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,Name,Price,ImageUrl,CategoryId")] Dish dish, List<int> ingredients)
        {
            var newIngredients = await _context.Ingredients.Where(i => ingredients.Contains(i.IngredientId)).ToListAsync();
            var newDishIngredients = new List<DishIngredient>();
            newIngredients.ForEach(i => newDishIngredients.Add(new DishIngredient
            {
                IngredientId = i.IngredientId,
                Ingredient = i,
                DishId = dish.DishId,
                Dish = dish
            }));

            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                _context.DishIngredient.AddRange(newDishIngredients);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.Include(d => d.DishIngredients).SingleOrDefaultAsync(m => m.DishId == id);
            var ingredients = await _context.Ingredients.ToListAsync();
            if (dish == null)
            {
                return NotFound();
            }
            var vm = new ManageDishViewModel
            {
                DishId = dish.DishId,
                Name = dish.Name,
                CategoryId = dish.CategoryId,
                Price = dish.Price,
                ImageUrl = dish.ImageUrl,
                DishIngredients = dish.DishIngredients,
                Ingredients = ingredients
            };
            return View(vm);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Price,ImageUrl,CategoryId")] Dish dish, List<int> ingredients)
        {
            var newIngredients = await _context.Ingredients.Where(i => ingredients.Contains(i.IngredientId)).ToListAsync();
            var newDishIngredients = new List<DishIngredient>();
            newIngredients.ForEach(i => newDishIngredients.Add(new DishIngredient
            {
                IngredientId = i.IngredientId,
                Ingredient = i,
                DishId = dish.DishId,
                Dish = dish
            }));

            if (id != dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.RemoveRange(_context.DishIngredient.Where(di => di.DishId == dish.DishId));
                    await _context.SaveChangesAsync();
                    _context.DishIngredient.AddRange(newDishIngredients);
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }
    }
}
