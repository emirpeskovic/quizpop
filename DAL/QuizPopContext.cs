using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QuizPop.Extensions;
using System.Reflection;

namespace QuizPop.DAL
{
    public class QuizPopContext : DbContext
    {

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityMethod = typeof(ModelBuilder).GetMethod("Entity");
            if (entityMethod == null) return;

            foreach (var entityType in GetEntityTypes())
            {
                Console.WriteLine("YO");
                entityMethod.MakeGenericMethod(entityType).Invoke(modelBuilder, null);
            }

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName().ToSnakeCase());

                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("YourConnectionStringHere",
                npgsqlOptionsAction: options =>
                {
                    options.SetPostgresVersion(new Version(13, 4));
                });
        }

        public DbSet<T> Entity<T>() where T : class, IEntity
        {
            return base.Set<T>();
        }

        private static IEnumerable<Type> GetEntityTypes()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IEntity).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        }
    }
}
