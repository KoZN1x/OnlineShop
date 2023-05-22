using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Create(TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(trainingType);
                await _service.SaveAsync();
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
        public async Task<IActionResult> Edit(TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                _service.Update(trainingType);
                await _service.SaveAsync();
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
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            var obj = _service.Find(id.GetValueOrDefault());
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
