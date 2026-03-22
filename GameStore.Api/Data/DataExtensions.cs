using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app) // Extension method to apply any pending migrations to the database
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
                             .GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    public static void AddGameStoreDb(this WebApplicationBuilder builder) // Extension method to add the games genres to the database if they don't already exist
    {
        var connectionString = builder.Configuration.GetConnectionString("GameStore") 
                               ?? throw new InvalidOperationException("Connection string 'GameStore' not found."); // Get the connection string from the configuration, throw an exception if it is not found
         builder.Services.AddSqlite<GameStoreContext>
        (
            connectionString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Fighting" },
                        new Genre { Name = "Action-Adventure" },
                        new Genre { Name = "Sandbox" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Puzzle-Platformer" },
                        new Genre { Name = "Platformer" }
                    );

                    context.SaveChanges();
                }
            })
        ); 
    }
}
