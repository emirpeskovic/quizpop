using Microsoft.EntityFrameworkCore;

namespace QuizPop.DAL
{
    public class DatabaseManager
    {
        public DbContext CreateContext() => new QuizPopContext();

        public void UseContext(Action<DbContext> action)
        {
            action.Invoke(new QuizPopContext());
        }
    }
}
