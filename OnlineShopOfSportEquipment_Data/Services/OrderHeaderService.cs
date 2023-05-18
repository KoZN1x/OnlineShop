﻿using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopOfSportEquipment_Data.Services.IService;
using OnlineShopOfSportEquipment_Models;
using OnlineShopOfSportEquipment_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOfSportEquipment_Data.Services
{
    public class OrderHeaderService : Service<OrderHeader>, IOrderHeaderService
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders!.Update(orderHeader);
        }
    }
}
