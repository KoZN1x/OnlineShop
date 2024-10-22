using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Utility;

namespace OnlineShopOfSportEquipment.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> IndexAsync()
        {
            IEnumerable<Category> objList = await _service.GetAllAsync();
            return View(objList);
        }

        //GET - CREATE
        public IActionResult CreateAsync()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Category category)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(category);
                await _service.SaveAsync();
            }
            return RedirectToAction("Index");
        }

        //GET - EDIT
        public async Task<IActionResult> EditAsync(Guid? id)
        {
            if (id != null)
            { 
                var obj = await _service.FindAsync(id.GetValueOrDefault());
                if (obj != null)
                {
                    return View(obj);
                }
                return NotFound();
            }
            return NotFound();
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(Category category)
        {
            if (ModelState.IsValid)
            {
                _service.Update(category);
                await _service.SaveAsync();
            }
            return RedirectToAction("Index");
        }

        //GET - DELETE
        public async Task<IActionResult> DeleteAsync(Guid? id)
        {
            if (id != null)
            {
                var obj = await _service.FindAsync(id.GetValueOrDefault());
                if (obj != null)
                {
                    return View(obj);
                }
                else return NotFound();
            }
            else return NotFound();
        }

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePostAsync(Guid? id)
        {
            var obj = await _service.FindAsync(id.GetValueOrDefault());
            if (obj != null)
            {
                _service.Remove(obj);
                await _service.SaveAsync();
                return RedirectToAction("Index");
            }
            else return NotFound();
        }
    }
}
