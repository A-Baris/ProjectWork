﻿using Restaurant.BLL.AbstractServices;
using Restaurant.BLL.Repositories;
using Restaurant.DAL.Context;
using Restaurant.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Services
{
    public class AccountingTransactionService : BaseRepository<AccountingTransaction>, IAccountingTransactionService
    {
        public AccountingTransactionService(ProjectContext context) : base(context)
        {
        }
    }
}
