using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductMVC1.Data;
using ProductMVC1.Services;

namespace ProductMVC1.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoriesController(ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            
            return View(await _categoryService.GetAllCategorysAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Id,Created,Modified,CreatedById,ModifiedById")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id", category.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id", category.ModifiedById);
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id", category.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id", category.ModifiedById);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Id,Created,Modified,CreatedById,ModifiedById")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateAsync(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoryExists(category.Id))
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
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id", category.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id", category.ModifiedById);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryService.GetCategoryByIdAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExists(int id)
        {
          return (await _categoryService.GetCategoryByIdAsync(id) != null);
        }
    }
}
