﻿using QuizPop.DAL;

namespace QuizPop;

public static class Initializer
{
    public static void Initialize(WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        var services = scope.ServiceProvider;
        var databaseManager = services.GetRequiredService<DatabaseManager>();
        databaseManager.UseContext(context =>
        {
            context.Database.EnsureCreated();
        });
    }
}