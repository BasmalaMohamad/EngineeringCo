﻿using AutoMapper;
using Core.Entities;
using Core.Entities.Consts;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructrue.Data
{
    public class AccessoriesRepository : IAccessoryRepository
    {
        private readonly StoreContext _storeContext;
        private readonly IMapper _mapper;
        public AccessoriesRepository(StoreContext storeContext, IMapper mapper)
        {
            _storeContext = storeContext;
            _mapper = mapper;
        }

        public async Task<Accessories> AddAccessory(Accessories accessory)
        {
            _storeContext.Accessories.Add(accessory);
            await _storeContext.SaveChangesAsync();
            return accessory;
        }
      
        public async Task<int> CountAsync()
        {
            return await _storeContext.Accessories.Include(p => p.Category)
               .CountAsync();
        }

        public async Task<Accessories> EditAccessory(int id , Accessories updatedaccessory)
        {
            var accessory = _storeContext.Accessories.Find(id);
            accessory.Name=updatedaccessory.Name;
            accessory.Size = updatedaccessory.Size;
            accessory.Category.Name = updatedaccessory.Category.Name;
            accessory.Category.Id=updatedaccessory.Category.Id;
            accessory.ImageURL=updatedaccessory.ImageURL;
            accessory.PumpName=updatedaccessory.PumpName;
            accessory.Model=updatedaccessory.Model;
            accessory.Construction=updatedaccessory.Construction;


            await _storeContext.SaveChangesAsync();
            return accessory;
        }
       
        public async Task<IReadOnlyList<Accessories>> GetAccessoriesAsync()
        {
            return await _storeContext.Accessories.Include(p => p.Category)
                            .ToListAsync();
        }

        public async Task<IReadOnlyList<Accessories>> GetAccessoriesWithSpecificationsAsync(AccessoriesSpecParams accessoriesParams)
        {
            IQueryable<Accessories> query = _storeContext.Accessories.Include(p => p.Category);
            query = query.Where(p =>
              (string.IsNullOrEmpty(accessoriesParams.SearchValue)
                  ||
                  p.Name.ToLower().Contains(accessoriesParams.SearchValue.ToLower()) ||
                   p.PumpName.ToLower().Contains(accessoriesParams.SearchValue.ToLower()) ||
                   p.Model.ToLower().Contains(accessoriesParams.SearchValue.ToLower()) ||
                   p.Construction.ToLower().Contains(accessoriesParams.SearchValue.ToLower()))
                  && (!accessoriesParams.CategoryId.HasValue || p.Category.Id == accessoriesParams.CategoryId)
                  && (string.IsNullOrEmpty(accessoriesParams.Model) || p.Model.ToLower() == accessoriesParams.Model.ToLower())
                  && (!accessoriesParams.SizeFrom.HasValue || p.Size >= accessoriesParams.SizeFrom)
                  && (!accessoriesParams.SizeTo.HasValue || p.Size <= accessoriesParams.SizeTo)
                  && (string.IsNullOrEmpty(accessoriesParams.Construction) || p.Construction == accessoriesParams.Construction)
                  && (string.IsNullOrEmpty(accessoriesParams.PumpName) || p.PumpName == accessoriesParams.PumpName)
                  && (string.IsNullOrEmpty(accessoriesParams.Name) || p.Name == accessoriesParams.Name)

                // Use ToString() for enum comparison
                );

            if (accessoriesParams.sortBy.ToLower() == "name")
                accessoriesParams.sortBy = AccesSortByOptions.Name;
            
            if (accessoriesParams.sortBy.ToLower() == "size")
                accessoriesParams.sortBy = AccesSortByOptions.Size;
            if (accessoriesParams.sortBy.ToLower() == "model")
                accessoriesParams.sortBy = AccesSortByOptions.Model;

            query = query.OrderBy($"{accessoriesParams.sortBy} {accessoriesParams.sortDirection}");

            var products = await query.Skip(accessoriesParams.PageSize * (accessoriesParams.PageIndex - 1))
                                  .Take(accessoriesParams.PageSize)
                                  .ToListAsync();


            return products;
        }

        public async Task<Accessories> GetAccessoryByIdAsync(int id)
        {
            return await _storeContext.Accessories.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> GetAccessoryNameAsync(int id)
        {
            var name = await _storeContext.Accessories.Where(p => p.Id == id)
                            .Include(p => p.Category)
                            .Select(p => p.PumpName)
                            .FirstOrDefaultAsync();

            return name;
        }

        public async Task RemoveAccessory(int id)
        {
            var accessory = _storeContext.Accessories.Find(id);
            if(accessory == null)
            {
                return;
            }
            _storeContext.Accessories.Remove(accessory);
            _storeContext.SaveChanges();

        }
       
        public async Task<bool> SaveAsync()
        {
            var saved = await _storeContext.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
