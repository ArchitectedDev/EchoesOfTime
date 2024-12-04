namespace EchoesOfTime.Database;

public static class DatabaseInitializer
{
    public static void Initialize(TimeTrackerDbContext context)
    {
        // Ensure the database and schema are created
        context.Database.EnsureCreated();
    }
}