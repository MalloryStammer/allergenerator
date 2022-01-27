using System;
namespace allergenerator.Models
{
    public class Sensitivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Sensitivity()
        {
        }

        public Sensitivity(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
