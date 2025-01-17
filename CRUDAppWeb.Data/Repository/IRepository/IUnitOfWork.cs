﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUDAppWeb.Data.Repository;
using CRUDAppWeb.Data.Repository.IRepository;

namespace CRUDApp.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get;  }
        public ICoverTypeRepository CoverType { get; }

        public void Save();
    }
}
