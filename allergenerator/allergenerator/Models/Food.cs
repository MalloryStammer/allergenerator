using System;
using System.Collections.Generic;

namespace allergenerator.Models
{
    public class Food {

        public int Id { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public List<FoodSensitivity> FoodSensitivities { get; set; }

        public Food() { }
        public Food(string name)
        {
            Name = name;
        }
    }   
}
