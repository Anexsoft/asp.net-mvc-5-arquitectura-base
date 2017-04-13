using Common.CustomFilters;
using Model.Helper;
using System.Collections.Generic;

namespace Model.Domain
{
    public class Course : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<StudentPerCourse> StudentPerCourses { get; set; }

        public bool Deleted { get; set; }
    }
}
