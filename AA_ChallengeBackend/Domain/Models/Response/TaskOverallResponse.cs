using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Response
{
    public class TaskDetailResponse
    {
        public int Id { get; set; }
        public int IdList { get; set; }
        public string Description { get; set; }
        public DateTime? RemindDate { get; set; }
        public DateTime DueDate { get; set; }
        
        public DateTime? MyDayDate { get; set; }
        public bool hasMyDayToday { get; set; }
        public string Notes { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsImportant { get; set; }
        public string ListName { get; set; }
        public string ListDescription { get; set; }
        public string ListUserId { get; set; }

        public bool BDueDate { get; set; }
        public bool BMyDayDate { get; set; }
    }
}
