using Microsoft.EntityFrameworkCore;

namespace _02IdentityExample.Data
{
    public static class IdenityDbContextExtensions {
        public static void RemoveIdentityPrefixTableName(this ModelBuilder builder) {
            var entityTypes = builder.Model.GetEntityTypes();
            foreach (var entityType in entityTypes)
                builder.Entity(entityType.ClrType)
                       .ToTable(entityType.GetTableName().Replace("AspNet", ""));
        }
    }
}

