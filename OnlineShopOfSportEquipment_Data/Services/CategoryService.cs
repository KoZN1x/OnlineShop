using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOfSportEquipment_Data.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Category category)
        {
            var itemFromDb = _context.Categories!.FirstOrDefault(x => x.Id == category.Id);
            if (itemFromDb != null)
            {
                itemFromDb.Name = category.Name;
            }
        }
    }
}
