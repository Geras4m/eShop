using DC.Store.Core.Model;
using DC.Store.Core.Services.Interfaces;
using DC.Store.Data.Entities;
using DC.Store.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AutoMapper.Mapper;

namespace DC.Store.Web.Controllers
{
    public class ProductsController : Controller
    {
        #region Fields, Constructors
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        #endregion

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var dtoList = await _productService.GetAllAsync(nameof(Category));
            var products = Map<IList<ProductDto>, IEnumerable<ProductViewModel>>(dtoList);

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _productService.GetWithCategoryAsync(id);
            var product = Map<ProductDto, ProductViewModel>(productDto);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categories = Map<IList<CategoryDto>, IEnumerable<CategoryViewModel>>(await _categoryService.GetAllAsync());
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Title");

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,CategoryId")] ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(Map<ProductViewModel, ProductDto>(product));
                await _productService.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            var categories = Map<IList<CategoryDto>, IEnumerable<CategoryViewModel>>(await _categoryService.GetAllAsync());
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Title", product.CategoryId);

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _productService.GetWithCategoryAsync(id);
            var product = Map<ProductDto, ProductViewModel>(productDto);

            if (product == null)
            {
                return NotFound();
            }

            var categories = Map<IList<CategoryDto>, IEnumerable<CategoryViewModel>>(await _categoryService.GetAllAsync());
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Title", product.CategoryId);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,CategoryId")] ProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productService.Modify(Map<ProductViewModel, ProductDto>(product));
                    await _productService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

            var categories = Map<IList<CategoryDto>, IEnumerable<CategoryViewModel>>(await _categoryService.GetAllAsync());
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Title", product.CategoryId);

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _productService.GetWithCategoryAsync(id);
            var product = Map<ProductDto, ProductViewModel>(productDto);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.RemoveAsync(id);
            await _productService.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) =>
            _productService.Any(p => p.Id == id);
    }
}
