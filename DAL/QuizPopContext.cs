﻿using Microsoft.EntityFrameworkCore;
using QuizPop.Extensions;
using System.Reflection;

namespace QuizPop.DAL
{
    public class QuizPopContext : DbContext
    {
        public QuizPopContext(DbContextOptions<QuizPopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method to ensure that the default configuration is applied.
            base.OnModelCreating(modelBuilder);

            // Find the Entity method on the ModelBuilder class.
            var entityMethod = typeof(ModelBuilder).GetMethods().FirstOrDefault(m => m is { Name: "Entity", IsGenericMethod: true } && m.GetParameters().Length == 0);
            if (entityMethod == null) return;

            // Call the Entity method for each class that implements the IEntity interface.
            foreach (var entityType in GetEntityTypes())
            {
                entityMethod.MakeGenericMethod(entityType).Invoke(modelBuilder, null);
            }

            // Convert all table and column names to snake case.
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName().ToSnakeCase());

                foreach (var property in entityType.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }
            }
        }

        /// <summary>
        /// Returns a DbSet of the specified type.
        /// </summary>
        /// <typeparam name="T">The specified type that implements <see cref="IEntity"/>.</typeparam>
        /// <returns>The DbSet of the specified type.</returns>
        public DbSet<T> Entity<T>() where T : class, IEntity => base.Set<T>();

        /// <summary>
        /// Finds all classes that implement the <see cref="IEntity"/> interface.
        /// </summary>
        /// <returns>An IEnumerable of Type</returns>
        private static IEnumerable<Type> GetEntityTypes() =>
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IEntity).IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false });
    }
}