﻿using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Category> CreateAsync(Category obj)
        {
            await _db.Category.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj = await _db.Category.FirstOrDefaultAsync(x => x.Id == id);

            if (obj != null)
            {
                _db.Category.Remove(obj);
                return (await _db.SaveChangesAsync() > 0);
            }

            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var obj = await _db.Category.FirstOrDefaultAsync(x => x.Id == id);

            if (obj != null)
            {
                return obj;
            }   
            return new Category();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _db.Category.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            var objFromDb = await _db.Category.FirstOrDefaultAsync(x => x.Id == obj.Id);

            if (objFromDb != null) 
            {
                objFromDb.Name = obj.Name;
                await _db.SaveChangesAsync();
                return objFromDb;
            }
            return new Category();  
        }
    }
}
