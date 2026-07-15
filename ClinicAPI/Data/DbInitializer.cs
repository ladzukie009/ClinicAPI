using BCrypt.Net;
using ClinicAPI.Models;

namespace ClinicAPI.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        // If an admin already exists, do nothing
        if (context.Users.Any())
            return;

        var admin = new User
        {
            Username = "admin",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
        };

        context.Users.Add(admin);
        context.SaveChanges();
    }
}