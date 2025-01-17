﻿using DataAccess.IRepositories;
using Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, new();
        Task SaveChanges();
    }
}
