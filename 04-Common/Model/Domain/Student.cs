using Common;
using Common.CustomFilters;
using Model.Helper;
using System;
using System.Collections.Generic;

namespace Model.Domain
{
    public class Student : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public Enums.Gender Genre { get; set; }
        public Enums.Status CurrentStatus { get; set; }
        public DateTime? LastVisit { get; set; }

        public ICollection<StudentPerCourse> StudentPerCourses { get; set; }

        public bool Deleted { get; set; } 
    }
}
