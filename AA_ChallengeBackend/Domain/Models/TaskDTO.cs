using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class TaskDTO
    {   
        public int Id { get; set; }
        public int IdList { get; set; }
        public string Description { get; set; }
        public DateTime? RemindDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? MyDayDate { get; set; }
        public string Notes { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsImportant { get; set; }

    }
}
