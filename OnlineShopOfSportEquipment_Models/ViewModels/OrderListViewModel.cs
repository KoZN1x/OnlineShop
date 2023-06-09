﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopOfSportEquipment_Models.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderHeader>? OrderHeaderList { get; set; }
        public IEnumerable<SelectListItem>? StatusList { get; set; }
    }
}
