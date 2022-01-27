using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using allergenerator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace allergenerator.ViewModels
{
    public class AddFoodSensitivityViewModel
    {
        public int FoodId { get; set; }

        [Required(ErrorMessage = "Sensitivity is required")]
        public int SensitivityId { get; set; }

        public Food Food { get; set; }

        public List<SelectListItem> Sensitivities { get; set; }

        public AddFoodSensitivityViewModel(Food theFood, List<Sensitivity> possibleSensitivities)
        {
            Sensitivities = new List<SelectListItem>();

            foreach (var sensitivity in possibleSensitivities)
            {
                Sensitivities.Add(new SelectListItem
                {
                    Value = sensitivity.Id.ToString(),
                    Text = sensitivity.Name
                });
            }

            Food = theFood;
        }

        public AddFoodSensitivityViewModel()
        {
        }
    }
}
