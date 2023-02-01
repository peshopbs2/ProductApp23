using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using ProductMVC1.Data;
using ProductMVC1.Services;

namespace ProductMVC1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductsController(IProductService productService, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var items = _productService.GetAllProductsAsync();
            return View(await items);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _categoryService.GetAllCategorysAsync(), "Id", "Title");
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id");
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,Id,Created,Modified,CreatedById,ModifiedById")] Product product, [Bind("Categories")] List<int> categories)
        {
            if (ModelState.IsValid)
            {
                if (categories == null)
                {
                    product.Categories = new HashSet<Category>();
                }
                else
                {
                    product.Categories = categories
                        .Select(categoryId => _categoryService.GetCategoryByIdAsync(categoryId).Result)
                        .ToList();
                }
                await _productService.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id", product.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id", product.ModifiedById);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id", product.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id", product.ModifiedById);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Price,Id,Created,Modified,CreatedById,ModifiedById")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
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
            ViewData["CreatedById"] = new SelectList(_userManager.Users, "Id", "Id", product.CreatedById);
            ViewData["ModifiedById"] = new SelectList(_userManager.Users, "Id", "Id", product.ModifiedById);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            return (await _productService.GetProductByIdAsync(id)) != null;
        }
    }
}
