using System;
using System.Collections.Generic;

namespace Data.DbModels
{
    public partial class TList
    {
        public TList()
        {
            TTask = new HashSet<TTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<TTask> TTask { get; set; }
    }
}
