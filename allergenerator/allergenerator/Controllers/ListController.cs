using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allergenerator.Models;
using allergenerator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using allergenerator.Data;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace allergenerator.Controllers
{
    public class ListController : Controller
    {
        internal static Dictionary<string, string> ColumnChoices = new Dictionary<string, string>()
        {
            {"all", "All"},
            {"category", "Category"},
            {"sensitivity", "Sensitivity"}
        };

        internal static List<string> TableChoices = new List<string>()
        {
            "category",
            "sensitivity"
        };

        private FoodDbContext context;

        public ListController(FoodDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.columns = ColumnChoices;
            ViewBag.tablechoices = TableChoices;
            ViewBag.categories = context.Categories.ToList();
            ViewBag.sensitivities = context.Sensitivities.ToList();
            return View();
        }

        public IActionResult Foods(string column, string value)
        {
            List<Food> foods = new List<Food>();
            List<FoodDetailViewModel> displayFoods = new List<FoodDetailViewModel>();

            if (column.ToLower().Equals("all"))
            {
                foods = context.Foods
                    .Include(j => j.Category)
                    .ToList();

                foreach (var food in foods)
                {
                    List<FoodSensitivity> foodSensitvities = context.FoodSensitivities
                        .Where(js => js.FoodId == food.Id)
                        .Include(js => js.Sensitivity)
                        .ToList();

                    FoodDetailViewModel newDisplayFood = new FoodDetailViewModel(food, foodSensitvities);
                    displayFoods.Add(newDisplayFood);
                }

                ViewBag.title = "All Foods";
            }
            else
            {
                if (column == "category")
                {
                    foods = context.Foods
                        .Include(j => j.Category)
                        .Where(j => j.Category.Name == value)
                        .ToList();

                    foreach (Food food in foods)
                    {
                        List<FoodSensitivity> foodSensitivities = context.FoodSensitivities
                        .Where(js => js.FoodId == food.Id)
                        .ToList();

                        FoodDetailViewModel newDisplayFood = new FoodDetailViewModel(food, foodSensitivities);
                        displayFoods.Add(newDisplayFood);
                    }
                }
                else if (column == "sensitivity")
                {
                    List<FoodSensitivity> foodSensitivities = context.FoodSensitivities
                        .Where(j => j.Sensitivity.Name == value)
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
                ViewBag.title = "Foods with " + ColumnChoices[column] + ": " + value;
            }
            ViewBag.foods = displayFoods;

            return View();
        }
    }
}
