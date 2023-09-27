﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Bill:BaseEntity    
    {
        public int CashAccountId { get; set; }
        public int TableOfRestaurantId { get; set; }

        public CashAccount CashAccount { get; set; }
        public TableOfRestaurant TableOfRestaurant { get; set; }
    }
}
