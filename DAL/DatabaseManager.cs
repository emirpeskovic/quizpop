using Microsoft.EntityFrameworkCore;

namespace QuizPop.DAL
{
    public class DatabaseManager
    {
        private readonly string _connectionString;
        
        public DatabaseManager()
        {
            _connectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=quizpop;Pooling=true;";
        }

        private DbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuizPopContext>()
                .UseNpgsql(_connectionString);
            return new QuizPopContext(optionsBuilder.Options);
        }

        public void UseContext(Action<DbContext> action)
        {
            using var context = CreateContext();
            action.Invoke(context);
        }

        public T? GetEntity<T>(Func<T, bool> match) where T : class, IEntity
        {
            using var context = (QuizPopContext)CreateContext();
            return context.Entity<T>().FirstOrDefault(match);
        }
    }
}
