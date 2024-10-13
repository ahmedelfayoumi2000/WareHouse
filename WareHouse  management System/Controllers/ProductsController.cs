using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WareHouse__management_System.Data;
using WareHouse__management_System.Models;
using WareHouse__management_System.View_Model;

namespace WareHouse__management_System.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult Search(string BookName)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewBag.BookName = BookName;
        //        List<Book> books = context.Books.Where(d => d.Name.Contains(BookName)).ToList();

        //        return View("index", books);

        //    }
        //    else
        //    {

        //        return RedirectToAction("Index");
        //    }
        //}

        // GET: Products
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Products/SearchForm
        [Authorize]
        public async Task<IActionResult> SearchForm()
        {
            return View();
        }

        // GET: Products/SearchResult
        public async Task<IActionResult> SearchResult(string SearchProduct)
        {

            return View("Index", await _context.Products.Where(p => p.Name.StartsWith(SearchProduct)).ToListAsync());
        }

        // GET: Products/Details/
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            var model = new ProductViewModel
            {
                Categories = _context.Categories.Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()

                }).ToList()
            };
            return View(model);
        }

        // POST: Products/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (_context.Products.Any(b => b.Name == product.Name))
                {
                    ModelState.AddModelError("Name", "This product already exists in the WareHouse.");
                    return View("Create", product);
                }
                else
                {

                    //product.Id = Guid.NewGuid();
                    var newProduct = new Product()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Cost = product.Cost,
                        Price = product.Price,
                        Count = product.Count,
                        ImageURL = product.ImageURL,
                        CategoryId = Guid.Parse(product.CategoryId),
                    };
                    _context.Products.Add(newProduct);
                    //_context.Add(newProduct);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(product);
        }

        // GET: Products/Edit/
        [Authorize]
        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _context.Products.Include(p => p.category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var newProduct = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Cost = product.Cost,
                Price = product.Price,
                Count = product.Count,
                ImageURL = product.ImageURL,
                CategoryId = product.CategoryId.ToString(),
                Categories  = _context.Categories.Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()

                }).ToList()
            };
            return View(newProduct);
        }

        // POST: Products/Edit/
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
           

            if (ModelState.IsValid)
            {
                try
                {
                    var prod = await _context.Products.FindAsync(product.Id);
                    if (prod == null)
                    {
                        return NotFound();
                    }
                   
                    prod.Name = product.Name;
                    prod.Description = product.Description;
                    prod.Cost = product.Cost;
                    prod.Price = product.Price;
                    prod.Count = product.Count;
                    prod.ImageURL = product.ImageURL;
                    prod.CategoryId = Guid.Parse(product.CategoryId);


                    //_context.Products.Update(prod);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
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
            product.Categories = _context.Categories.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()

            }).ToList();
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Product Is Not Found.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
