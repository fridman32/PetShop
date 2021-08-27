using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProjectMVC.Models
{
    public class CreateNewAnimal
    {
        //[Required(ErrorMessage = "Please enter a name.")]
        //[AllLettersValidation(ErrorMessage = "Only letters allowed.")]
        public string Name { get; set; }
        //[Range(1, 100)]
        public int Age { get; set; }
        public IFormFile Picture { get; set; }
        //[RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        //[Required]
        //[DataType(DataType.MultilineText)]
        //[StringLength(20)]
        public string Description { get; set; }
        //[Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
