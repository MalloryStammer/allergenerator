using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using allergenerator.Models;
using allergenerator.ViewModels;
using allergenerator.Data;
using Microsoft.EntityFrameworkCore;

namespace allergenerator.Controllers
{
    public class HomeController : Controller
    {
        private FoodDbContext context;

        public HomeController(FoodDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Food> foods = context.Foods.Include(j => j.Category).ToList();

            return View(foods);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }


        [HttpGet("/Add")]
        public IActionResult Addfood()
        {
   
            List<Category> categories = context.Categories.ToList();
            List<Sensitivity> sensitivities = context.Sensitivities.ToList();

            AddFoodViewModel addFoodViewModel = new AddFoodViewModel(categories, sensitivities);
            return View(addFoodViewModel);
        }




        public IActionResult ProcessAddFoodForm(AddFoodViewModel addFoodViewModel, string[] selectedSensitivities)
        {
            if (ModelState.IsValid)
            {
                Food food = new Food
                {
                    Name = addFoodViewModel.Name,
                    CategoryId = addFoodViewModel.CategoryId,
                };

                foreach (string sensitivity in selectedSensitivities)
                {
                    FoodSensitivity foodSensitivity = new FoodSensitivity
                    {
                        Food = food,
                        FoodId = food.Id,
                        SensitivityId = Int32.Parse(sensitivity)
                    };

                    context.FoodSensitivities.Add(foodSensitivity);
                }


                context.Foods.Add(food);
                context.SaveChanges();
                return Redirect("Index");
            }

            return RedirectToAction("AddFood", addFoodViewModel);
        }

        public IActionResult Detail(int id)
        {
            Food theFood = context.Foods
                .Include(j => j.Category)
                .Single(j => j.Id == id);

            List<FoodSensitivity> foodSensitivities = context.FoodSensitivities
                .Where(js => js.FoodId == id)
                .Include(js => js.Sensitivity)
                .ToList();

            FoodDetailViewModel viewModel = new FoodDetailViewModel(theFood, foodSensitivities);
            return View(viewModel);
        }
    }
}


