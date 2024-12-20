﻿using Core.Entities;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAccessoryRepository
    {
        Task<Accessories> GetAccessoryByIdAsync(int id);
        //Task<Category> GetByIdAsync(int id);
        Task<int> CountAsync();
        Task<string> GetAccessoryNameAsync(int id);
        Task<IReadOnlyList<Accessories>> GetAccessoriesAsync();
        Task<IReadOnlyList<Accessories>> GetAccessoriesWithSpecificationsAsync(AccessoriesSpecParams accessoriesParams);
        Task<Accessories> AddAccessory(Accessories accessory);
        

        Task RemoveAccessory(int id);
        Task<Accessories> EditAccessory(int id , Accessories accessory);
    }
}
