using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ELibrary.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace ELibrary.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
    serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, "Admin@1234", "admin@library.iub.ac");
                await EnsureRole(serviceProvider, adminID, Constants.AdminRole);

                // allowed user can create and edit contacts that they create
                var studentID = await EnsureUser(serviceProvider, "Student@1234", "student@library.iub.ac");
                await EnsureRole(serviceProvider, studentID, Constants.StudentRole);

                var authorID = await EnsureUser(serviceProvider, "Author@1234", "author@library.iub.ac");
                await EnsureRole(serviceProvider, authorID, Constants.AuthorRole);

                using var context2 = new ELibraryContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ELibraryContext>>());

                SeedDB(context2);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true

                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        public static void SeedDB(ELibraryContext context)
        {
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }
            context.Book.AddRange(
                        new Book

                        {
                            Title = "Java : the complete reference ",
                            Author = "Schildt, Herbert.",
                            Format = "Book",
                            Language = "English",
                            PublishDate = DateTime.Parse("1989-2-12"),
                            Edition = "8th ed",
                            Holding = true,
                            Description = " ",
                            HoldingDate = DateTime.Parse("1989-2-12"),
                            DueDate = DateTime.Parse("1989-2-12"),
                            StudentId = 123
                        },

                        new Book

                        {
                            Title = "Mathematical methods for physics and engineering",
                            Author = "Riley, K. F. 1936",
                            Format = "Book",
                            Language = "English",
                            PublishDate = DateTime.Parse("1980-4-10"),
                            Edition = "3rd ed",
                            Holding = true,
                            Description = " ",
                            HoldingDate = DateTime.Parse("1989-2-12"),
                            DueDate = DateTime.Parse("1989-2-12"),
                            StudentId = 123
                        },

                        new Book

                        {
                            Title = "India's Islamic traditions, 711-1750 ",
                            Author = "Eaton, Richard Maxwell.",
                            Format = "Book",
                            Language = "English",
                            PublishDate = DateTime.Parse("1990-10-19"),
                            Edition = "2nd ed",
                            Holding = true,
                            Description = " ",
                            HoldingDate = DateTime.Parse("1989-2-12"),
                            DueDate = DateTime.Parse("1989-2-12"),
                            StudentId = 123
                        },

                    new Book

                    {
                        Title = "Self and sovereignty : individual and community in South Asian Islam since 1850 ",
                        Author = "Jalal, Ayesha.",
                        Format = "Book",
                        Language = "English",
                        PublishDate = DateTime.Parse("2000-10-19"),
                        Edition = "2nd ed",
                        Holding = true,
                        Description = " ",
                        HoldingDate = DateTime.Parse("2000-2-12"),
                        DueDate = DateTime.Parse("2000-2-12"),
                        StudentId = 123
                    },
                    new Book

                    {
                        Title = "The circle of innovation : you can't shrink your way to greatness  ",
                        Author = "Peters, Thomas J.",
                        Format = "Book",
                        Language = "English",
                        PublishDate = DateTime.Parse("1997-10-19"),
                        Edition = "4th ed",
                        Holding = true,
                        Description = " ",
                        HoldingDate = DateTime.Parse("2000-2-12"),
                        DueDate = DateTime.Parse("2000-2-12"),
                        StudentId = 123
                    },
                    new Book

                    {
                        Title = "Air pollution control engineering ",
                        Author = "De Nevers, Noel, 1932",
                        Format = "Book",
                        Language = "English",
                        PublishDate = DateTime.Parse("1995-11-22"),
                        Edition = "1st ed",
                        Holding = true,
                        Description = " ",
                        HoldingDate = DateTime.Parse("2021-2-12"),
                        DueDate = DateTime.Parse("2021-2-12"),
                        StudentId = 123
                    },
                    new Book

                    {
                        Title = "Painting Indiana III Heritage of Place / Heritage of Place",
                        Author = "Perry, Rachel Berenson.",
                        Format = "eBook",
                        Language = "English",
                        PublishDate = DateTime.Parse("1998-08-22"),
                        Edition = "1st ed",
                        Holding = true,
                        Description = " ",
                        HoldingDate = DateTime.Parse("2021-2-12"),
                        DueDate = DateTime.Parse("2021-2-12"),
                        StudentId = 123
                    },
                    new Book

                    {
                        Title = "Canada : Modern Architectures in History",
                        Author = "Liscombe, R. W., 1946-.",
                        Format = "Book",
                        Language = "English",
                        PublishDate = DateTime.Parse("1946-08-22"),
                        Edition = "1st ed",
                        Holding = true,
                        Description = " ",
                        HoldingDate = DateTime.Parse("2021-2-12"),
                        DueDate = DateTime.Parse("2021-2-12"),
                        StudentId = 123
                    }
                    );
            context.SaveChanges();
        }
    }
}