using System;
namespace allergenerator.Models
{
    public class FoodSensitivity
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int SensitivityId { get; set; }
        public Sensitivity Sensitivity { get; set; }

        public FoodSensitivity() { }
    }
}