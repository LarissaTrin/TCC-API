using Project.Domain;

namespace Project.Persistence.Context
{
    public class DataInitializer
{
    public static void Initialize(DataContext context)
    {
        if (!context.ProjectRole.Any())
        {
            context.ProjectRole.AddRange(
                new ProjectRole { Id = 0, Name = "SuperAdmin" },
                new ProjectRole { Id = 1, Name = "Admin" },
                new ProjectRole { Id = 2, Name = "Leader" },
                new ProjectRole { Id = 3, Name = "User" }
            );
            context.SaveChanges();
        }
    }
}

}