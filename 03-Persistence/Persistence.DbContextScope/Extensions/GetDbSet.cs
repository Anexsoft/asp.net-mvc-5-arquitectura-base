using Persistence.DatabaseContext;
using System.Data.Entity;

namespace Persistence.DbContextScope.Extensions
{
    public static class GetDbSet
    {
        public static DbSet<T> GetEntity<T>(this IDbContextReadOnlyScope value) where T : class
        {
            return value.DbContexts.Get<ApplicationDbContext>().Set<T>();
        }

        public static DbSet<T> GetEntity<T>(this IDbContextScope value) where T : class
        {
            return value.DbContexts.Get<ApplicationDbContext>().Set<T>();
        }
    }
}
