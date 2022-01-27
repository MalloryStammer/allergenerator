using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allergenerator.Data;
using allergenerator.Models;
using allergenerator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allergenerator.Controllers
{
    public class SearchController : Controller
    {
        private FoodDbContext context;

        public SearchController(FoodDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ListController.ColumnChoices;
            return View();
        }

        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Food> foods;
            List<FoodDetailViewModel> displayFoods = new List<FoodDetailViewModel>();

            if (string.IsNullOrEmpty(searchTerm))
            {
                foods = context.Foods
                   .Include(j => j.Category)
                   .ToList();

                foreach (var food in foods)
                {
                    List<FoodSensitivity> foodSensitivities = context.FoodSensitivities
                        .Where(js => js.FoodId == food.Id)
                        .Include(js => js.Sensitivity)
                        .ToList();

                    FoodDetailViewModel newDisplayFood = new FoodDetailViewModel(food, foodSensitivities);
                    displayFoods.Add(newDisplayFood);
                }
            }
            else
            {
                if (searchType == "category")
                {
                    foods = context.Foods
                        .Include(j => j.Category)
                        .Where(j => j.Category.Name == searchTerm)
                        .ToList();

                    foreach (Food food in foods)
                    {
                        List<FoodSensitivity> foodSensitivities = context.FoodSensitivities
                        .Where(js => js.FoodId == food.Id)
                        .Include(js => js.Sensitivity)
                        .ToList();

                        FoodDetailViewModel newDisplayFood = new FoodDetailViewModel(food, foodSensitivities);
                        displayFoods.Add(newDisplayFood);
                    }

                }
                else if (searchType == "sensitivity")
                {
                    List<FoodSensitivity> foodSensitivities = context.FoodSensitivities
                        .Where(j => j.Sensitivity.Name == searchTerm)
                        .Include(j => j.Food)
                        .ToList();

                    foreach (var food in foodSensitivities)
                    {
                        Food foundFood = context.Foods
                            .Include(j => j.Category)
                            .Single(j => j.Id == food.FoodId);

                        List<FoodSensitivity> displaySensitivities = context.FoodSensitivities
                            .Where(js => js.FoodId == foundFood.Id)
                            .Include(js => js.Sensitivity)
                            .ToList();

                        FoodDetailViewModel newDisplayFood = new FoodDetailViewModel(foundFood, displaySensitivities);
                        displayFoods.Add(newDisplayFood);
                    }
                }
            }

            ViewBag.columns = ListController.ColumnChoices;
            ViewBag.title = "Foods with " + ListController.ColumnChoices[searchType] + ": " + searchTerm;
            ViewBag.foods = displayFoods;

            return View("Index");
        }
    }
}