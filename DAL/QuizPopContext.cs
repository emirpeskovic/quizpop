using Microsoft.EntityFrameworkCore;
using QuizPop.Extensions;
using System.Reflection;

namespace QuizPop.DAL
{
    public class QuizPopContext : DbContext
    {
        public QuizPopContext(DbContextOptions<QuizPopContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entityMethod = typeof(ModelBuilder).GetMethods().FirstOrDefault(m => m is { Name: "Entity", IsGenericMethod: true } && m.GetParameters().Length == 0);
            if (entityMethod == null) return;

            foreach (var entityType in GetEntityTypes())
            {
                entityMethod.MakeGenericMethod(entityType).Invoke(modelBuilder, null);
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName().ToSnakeCase());

                foreach (var property in entityType.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }
            }
        }

        public DbSet<T> Entity<T>() where T : class, IEntity => base.Set<T>();

        private static IEnumerable<Type> GetEntityTypes() => 
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IEntity).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });
    }
}
