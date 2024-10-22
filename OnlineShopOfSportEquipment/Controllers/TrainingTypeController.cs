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

        public async Task<IActionResult> IndexAsync()
        {

            IEnumerable<TrainingType> trainingTypeList = await _service.GetAllAsync();
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
        public async Task<IActionResult> CreateAsync(TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(trainingType);
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
                else return NotFound();
            }
            else return NotFound();
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(TrainingType trainingType)
        {
            if (ModelState.IsValid)
            {
                _service.Update(trainingType);
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
