using System;

namespace Model.Domain
{
    public class StudentPerCourse
    {
        public int Id { get; set; }
        public DateTime SuscribedAt { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
