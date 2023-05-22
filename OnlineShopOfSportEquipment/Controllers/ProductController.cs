using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Utility;
using OnlineShopOfSportEquipment_Models.ViewModels;
using OnlineShopOfSportEquipment_Data.Services.IService;

namespace OnlineShopOfSportEquipment.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _service.GetAll(includeProperties: "Category,TrainingType");
            return View(productList);
        }

        //GET - UPSERT
        public IActionResult Upsert(Guid? id)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategorySelectList = _service.GetAllDropDownList(WC.CategoryName),
                TrainingTypeSelectList = _service.GetAllDropDownList(WC.TrainingTypeName)
            };
            if (id == null)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = _service.Find(id.GetValueOrDefault());
                if (productViewModel.Product == null)
                {
                    return NotFound();
                }
                return View(productViewModel);
            }
        }

        //POST - Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productViewModel.Product!.Id == Guid.Empty)
                {
                    //Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productViewModel.Product!.Image = fileName + extension;
                    await _service.AddAsync(productViewModel.Product);
                    await _service.SaveAsync();
                }
                else
                {
                    //Updating
                    var objFromDb = _service.FirstOrDefault(x => x.Id == productViewModel.Product.Id, isTracking: false);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        var oldFile = Path.Combine(upload, objFromDb!.Image!);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productViewModel.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productViewModel.Product.Image = objFromDb!.Image;
                    }
                    _service.Update(productViewModel.Product);
                    await _service.SaveAsync();
                }
                return RedirectToAction("Index");
            }
            productViewModel.CategorySelectList = _service.GetAllDropDownList(WC.CategoryName);
            productViewModel.TrainingTypeSelectList = _service.GetAllDropDownList(WC.TrainingTypeName);
            return View(productViewModel);
        }

        //GET - DELETE
        public IActionResult Delete(Guid? id)
        {
            if (id != null)
            {
                var product = _service.FirstOrDefault(x => x.Id == id, inclideProperties: "Category,TrainingType");
                if (product != null)
                {
                    return View(product);
                }
                else return NotFound();
            }
            else return NotFound();
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            var product = _service.Find(id.GetValueOrDefault());
            if (product != null)
            {
                string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
                var oldFile = Path.Combine(upload, product.Image!);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
                _service.Remove(product);
                await _service.SaveAsync();
                return RedirectToAction("Index");
            }
            else return NotFound();
        }
    }
}
