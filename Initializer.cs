using QuizPop.DAL;

namespace QuizPop;

public static class Initializer
{
    public static async Task Initialize(WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        var services = scope.ServiceProvider;
        var databaseManager = services.GetRequiredService<DatabaseManager>();
        await databaseManager.UseContextAsync(context =>
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        });
    }
}