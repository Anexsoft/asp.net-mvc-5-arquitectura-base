using Model.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Persistence.DatabaseContext.Mapping
{
    public class CourseMapping : EntityTypeConfiguration<Course>
    {
        public CourseMapping()
        {
            Property(m => m.Name).IsRequired().HasMaxLength(100);
        }
    }
}
