﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class TaskToCreateDTO
    {
        [Required(ErrorMessage = "You should associate the task to a existing list.")]
        public int IdList { get; set; }

        [Required(ErrorMessage = "You should fill out a description.")]
        [MaxLength(200, ErrorMessage = "The Description shouldn't have more than 200 characters.")]
        public string Description { get; set; }
        public DateTime? RemindDate { get; set; }
        
        [Required(ErrorMessage = "You should fill out a due date.")]
        public DateTime DueDate { get; set; }

        public DateTime? MyDayDate { get; set; }
        public string Notes { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsImportant { get; set; }
    }
}
