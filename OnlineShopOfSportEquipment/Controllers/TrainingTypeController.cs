using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OnlineShopOfSportEquipment_Data;
using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Utility;

namespace OnlineShopOfSportEquipment.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class TrainingTypeController : Controller
    {
        private readonly ITrainingTypeService _service;
        public TrainingTypeController(ITrainingTypeService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {

            IEnumerable<TrainingType> trainingTypeList = _service.GetAll();
            return View(trainingTypeList);

        }
        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                _service.Add(trainingType);
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
        public IActionResult Edit(TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                _service.Update(trainingType);
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
