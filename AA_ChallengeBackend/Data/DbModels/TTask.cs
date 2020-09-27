using System;
using System.Collections.Generic;

namespace Data.DbModels
{
    public partial class TTask
    {
        public int Id { get; set; }
        public int? IdList { get; set; }
        public string Description { get; set; }
        public DateTime RemindDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime MyDayDate { get; set; }
        public string Notes { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }

        public virtual TList IdListNavigation { get; set; }
    }
}
