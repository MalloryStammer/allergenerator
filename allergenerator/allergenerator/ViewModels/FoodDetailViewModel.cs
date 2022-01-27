using System;
using System.Collections.Generic;
using allergenerator.Models;

namespace allergenerator.ViewModels
{
    public class FoodDetailViewModel
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string SensitivityText { get; set; }

        public FoodDetailViewModel(Food theFood, List<FoodSensitivity> foodSensitivities)
        {
            FoodId = theFood.Id;
            Name = theFood.Name;
            CategoryName = theFood.Category.Name;

            SensitivityText = "";
            for (int i = foodSensitivities.Count - 1; i >= 0; i--)
            {
                SensitivityText += foodSensitivities[i].Sensitivity.Name;
                if (i < foodSensitivities.Count - 1)
                {
                    SensitivityText += ", ";
                }
            }
        }
    }
}
