using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allergenerator.Data;
using allergenerator.Models;
using allergenerator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace allergenerator.Controllers
{
    public class SensitivityController : Controller
    {
        private FoodDbContext context;

        public SensitivityController(FoodDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Sensitivity> sensitivities = context.Sensitivities.ToList();
            return View(sensitivities);
        }

        public IActionResult Add()
        {
            Sensitivity sensitivitiy = new Sensitivity();
            return View(sensitivitiy);
        }

        [HttpPost]
        public IActionResult Add(Sensitivity sensitivitiy)
        {
            if (ModelState.IsValid)
            {
                context.Sensitivities.Add(sensitivitiy);
                context.SaveChanges();
                return Redirect("/Sensitivity/Add");
            }

            return View("Add", sensitivitiy);
        }

        public IActionResult AddFood(int id)
        {
            Food theFood = context.Foods.Find(id);
            List<Sensitivity> possibleSensitivities = context.Sensitivities.ToList();
            AddFoodSensitivityViewModel viewModel = new AddFoodSensitivityViewModel(theFood, possibleSensitivities);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddFood(AddFoodSensitivityViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                int foodId = viewModel.FoodId;
                int sensitivitiyId = viewModel.SensitivityId;

                List<FoodSensitivity> existingItems = context.FoodSensitivities
                    .Where(js => js.FoodId == foodId)
                    .Where(js => js.SensitivityId == sensitivitiyId)
                    .ToList();

                if (existingItems.Count == 0)
                {
                    FoodSensitivity foodSensitivity = new FoodSensitivity
                    {
                        FoodId = foodId,
                        SensitivityId = sensitivitiyId
                    };
                    context.FoodSensitivities.Add(foodSensitivity);
                    context.SaveChanges();
                }

                return Redirect("/Home/Detail/" + foodId);
            }

            return View(viewModel);
        }

        public IActionResult About(int id)
        {
            List<FoodSensitivity> foodSensitivities = context.FoodSensitivities
                .Where(js => js.SensitivityId == id)
                .Include(js => js.Food)
                .Include(js => js.Sensitivity)
                .ToList();

            return View(foodSensitivities);
        }

        public IActionResult Learn()
        {
            return View();
        }

    }
}