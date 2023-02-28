using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace QuizPop.DAL;

public class DatabaseManager
{
    /// <summary>
    ///     Our connection string.
    /// </summary>
    private readonly string _connectionString;

    public DatabaseManager()
    {
        _connectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=quizpop;Pooling=true;";
    }

    /// <summary>
    ///     Creates a new context for the database.
    /// </summary>
    /// <returns>QuizPopContext as DbContext</returns>
    private DbContext CreateContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<QuizPopContext>()
            .UseNpgsql(_connectionString);
        return new QuizPopContext(optionsBuilder.Options);
    }

    /// <summary>
    ///     Uses the context to perform an action.
    ///     Will rollback changes if an exception is thrown.
    /// </summary>
    /// <param name="action">User-defined action</param>
    public void UseContext(Action<DbContext> action)
    {
        using var context = CreateContext();
        try
        {
            action.Invoke(context);
            context.SaveChanges();
        }
        catch (DbException)
        {
            if (context.Database.CurrentTransaction != null)
                context.Database.RollbackTransaction();
        }
    }

    /// <summary>
    ///     Uses the context asynchronously to perform an action.
    ///     Will rollback changes if an exception is thrown.
    /// </summary>
    /// <param name="action">User-defined action</param>
    public async Task UseContextAsync(Action<DbContext> action)
    {
        await using var context = CreateContext();
        try
        {
            action.Invoke(context);
            await context.SaveChangesAsync();
        }
        catch (DbException)
        {
            if (context.Database.CurrentTransaction != null)
                await context.Database.RollbackTransactionAsync();
        }
    }

    /// <summary>
    ///     Returns a single entity from the database.
    /// </summary>
    /// <param name="match">If defined, tries to find an entity with the matched predicate.</param>
    /// <typeparam name="T">The entity that implements IEntity</typeparam>
    /// <returns>The entity</returns>
    public T? GetOne<T>(Func<T, bool>? match = null) where T : class, IEntity
    {
        using var context = (QuizPopContext)CreateContext();
        return context.Entity<T>().FirstOrDefault(match ?? (_ => true));
    }
}