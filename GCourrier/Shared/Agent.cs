using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCourrier.Shared
{
    public class Agent
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        //public DateTime DateOfBrith { get; set; }
        public Power Power { get; set; }
        public int DepartmentId { get; set; }
        public string? PhotoPath { get; set; }
        public Department? Department { get; set; }
    }
}
