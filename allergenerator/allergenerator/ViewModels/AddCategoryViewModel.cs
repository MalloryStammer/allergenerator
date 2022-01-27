using System;
using System.ComponentModel.DataAnnotations;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace allergenerator.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Category is required")]
        public string Name { get; set; }
    }
}
