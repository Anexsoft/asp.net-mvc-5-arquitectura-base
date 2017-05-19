using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Custom
{
    public class StudentForGridView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string CurrentStatus { get; set; }
        public int NumberOfCourses { get; set; }
        public string CreatedBy { get; set; }
    }
}
