using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allergenerator.Data;
using allergenerator.Models;
using allergenerator.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace allergenerator.Controllers
{
    public class CategoryController : Controller
    {
        private FoodDbContext context;

        public CategoryController(FoodDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Category> categoryList = context.Categories.ToList();
            return View(categoryList);
        }

        public IActionResult Add()
        
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        public IActionResult ProcessAddCategoryForm(AddCategoryViewModel addCategoryViewModel)
        
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = addCategoryViewModel.Name,
                };

                context.Categories.Add(category);
                context.SaveChanges();
                return Redirect("/Category/Add");
            }

            return View(addCategoryViewModel);
        }

        public IActionResult About(int id)
  
        {
            Category category = context.Categories.Find(id);
            return View(category);
        }

    }
}

