using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopOfSportEquipment_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOfSportEquipment_Data.Services.IService
{
    public interface IOrderDetailService : IService<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
