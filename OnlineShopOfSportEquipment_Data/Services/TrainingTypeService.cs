using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOfSportEquipment_Data.Services
{
    public class TrainingTypeService : Service<TrainingType>, ITrainingTypeService
    {
        private readonly ApplicationDbContext _context;
        public TrainingTypeService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(TrainingType trainingType)
        {
            var itemFromDb = _context.TrainingTypes!.FirstOrDefault(x => x.Id == trainingType.Id);
            if (itemFromDb != null)
            {
                itemFromDb.Name = trainingType.Name;
            }
        }
    }
}
