using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class ListToUpdateDTO
    {   
        [Required(ErrorMessage = "You should fill out a Name.")]
        [MaxLength(50, ErrorMessage = "The Name shouldn't have more than 50 characters.")]
        public string Name { get; set; }
        
        [MaxLength(200, ErrorMessage = "The Description shouldn't have more than 200 characters.")]
        public string Description { get; set; }
    }
}
