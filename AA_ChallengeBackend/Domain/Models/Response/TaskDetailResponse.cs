using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Response
{
    public class TaskOverallResponse
    {
        public int TasksCompleted { get; set; }
        public int TasksPlannedForToday { get; set; }
        public int TasksImportant { get; set; }
        public int TasksDueToday { get; set; }

    }
}
