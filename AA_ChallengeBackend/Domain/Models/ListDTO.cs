using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
