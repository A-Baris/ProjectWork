﻿using Restaurant.BLL.AbstractRepositories;
using Restaurant.BLL.AbstractServices;
using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
    public class CashAccountService : BaseService<CashAccount>, ICashAccountService
    {
        public CashAccountService(IRepository<CashAccount> repository) : base(repository)
        {
        }
    }
}
