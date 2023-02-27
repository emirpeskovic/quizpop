using Microsoft.EntityFrameworkCore;

namespace QuizPop.DAL
{
    public class DatabaseManager
    {
        private readonly string _connectionString;
        
        public DatabaseManager()
        {
            _connectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=quizpop;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
        }

        private DbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuizPopContext>()
                .UseNpgsql(_connectionString);
            return new QuizPopContext(optionsBuilder.Options);
        }

        public void UseContext(Action<DbContext> action)
        {
            action.Invoke(CreateContext());
        }
    }
}
