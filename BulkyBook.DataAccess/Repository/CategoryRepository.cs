﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Interfaces;
using BulkyBook.Models;
using System;
using System.Linq;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository: Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Category category)
        {
            var categoryDb = _db.Categories.FirstOrDefault(c => c.Id == category.Id);

            if(categoryDb != null)
            {
                categoryDb.Name = category.Name;
                _db.SaveChanges();
            }
        }
    }
}