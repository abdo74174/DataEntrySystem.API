using DataEntrySystem.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataEntrySystem.API.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            try
            {
                // Ensure the database exists (this could fail if tables exist but schema is different)
                // context.Database.EnsureCreated();

                // Seed Users
                if (!context.Users.Any())
                {
                    var users = new List<User>
                    {
                        new User { Username = "admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" },
                        new User { Username = "manager", PasswordHash = BCrypt.Net.BCrypt.HashPassword("manager123"), Role = "Admin" },
                        new User { Username = "user1", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = "User" },
                        new User { Username = "user2", PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"), Role = "User" },
                        new User { Username = "guest", PasswordHash = BCrypt.Net.BCrypt.HashPassword("guest123"), Role = "User" }
                    };

                    context.Users.AddRange(users);
                    context.SaveChanges();
                }

                var admin = context.Users.FirstOrDefault(u => u.Role == "Admin");
                if (admin == null)
                {
                    // If no admin exists but users do, create one
                    admin = new User { Username = "temp_admin", PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), Role = "Admin" };
                    context.Users.Add(admin);
                    context.SaveChanges();
                }

                // Seed initial Revenues if none exist
                if (!context.Revenues.Any())
                {
                    context.Revenues.AddRange(new List<Revenue>
                    {
                        new Revenue { ClientName = "Global Corp", OperationType = "Consulting", ContractPrice = 10000, OfferPrice = 9000, PaidAmount = 5000, Date = DateTime.Now.AddDays(-10), CreatedByUserId = admin.Id },
                        new Revenue { ClientName = "Tech Solutions", OperationType = "Development", ContractPrice = 15000, OfferPrice = 14000, PaidAmount = 14000, Date = DateTime.Now.AddDays(-5), CreatedByUserId = admin.Id },
                        new Revenue { ClientName = "Soft Systems", OperationType = "Support", ContractPrice = 5000, OfferPrice = 4500, PaidAmount = 2000, Date = DateTime.Now.AddDays(-2), CreatedByUserId = admin.Id }
                    });
                }

                // Seed initial Expenses if none exist
                if (!context.Expenses.Any())
                {
                    context.Expenses.AddRange(new List<Expense>
                    {
                        new Expense { ExpenseType = "Office Rent", Amount = 2000, Notes = "Monthly rent for DC office", Date = DateTime.Now.AddDays(-15), CreatedByUserId = admin.Id },
                        new Expense { ExpenseType = "Cloud Services", Amount = 500, Notes = "Azure subscription", Date = DateTime.Now.AddDays(-10), CreatedByUserId = admin.Id },
                        new Expense { ExpenseType = "Salaries", Amount = 8000, Notes = "Monthly payroll", Date = DateTime.Now.AddDays(-1), CreatedByUserId = admin.Id }
                    });
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Rethrow with more context
                throw new Exception("Error during DbInitializer.Seed: " + ex.Message, ex);
            }
        }
    }
}
