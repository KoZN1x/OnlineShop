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
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _service.GetAll();
            return View(objList);
        }
        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _service.Add(category);
                _service.Save();
            }
            return RedirectToAction("Index");
        }

        //GET - EDIT
        public IActionResult Edit(Guid? id)
        {
            if (id != null)
            {

                var obj = _service.Find(id.GetValueOrDefault());
                if (obj != null)
                {
                    return View(obj);
                }
                else return NotFound();
            }
            else return NotFound();
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _service.Update(category);
                _service.Save();
            }
            return RedirectToAction("Index");
        }

        //GET - DELETE
        public IActionResult Delete(Guid? id)
        {
            if (id != null)
            {
                var obj = _service.Find(id.GetValueOrDefault());
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
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _service.Find(id.GetValueOrDefault());
            if (obj != null)
            {
                _service.Remove(obj);
                _service.Save();
                return RedirectToAction("Index");
            }
            else return NotFound();
        }
    }
}
