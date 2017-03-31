using Model.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Persistence.DatabaseContext.Mapping
{
    public class StudentMapping : EntityTypeConfiguration<Student>
    {
        public StudentMapping()
        {
            Property(m => m.Name).IsRequired().HasMaxLength(50);
            Property(m => m.Email).IsRequired().HasMaxLength(100);
        }
    }
}
