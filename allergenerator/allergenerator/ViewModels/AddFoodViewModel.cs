using System;
using System.Collections.Generic;
using allergenerator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace allergenerator.ViewModels
{
    public class AddFoodViewModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<Sensitivity> Sensitivities { get; set; }

        public AddFoodViewModel() { }
        public AddFoodViewModel(List<Category> listPotentialCategories, List<Sensitivity> sensitivities)
        {
            Categories = new List<SelectListItem>();
            Sensitivities = sensitivities;

            foreach (Category category in listPotentialCategories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                });
            }
        }
    }
}