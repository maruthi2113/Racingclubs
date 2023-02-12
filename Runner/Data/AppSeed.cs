using Microsoft.AspNetCore.Identity;
using Runner.Data.Enum;
using Runner.Models;

namespace Runner.Data
{
    public class AppSeed
    {

        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Name = "Running Club 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.Formula1,
                            Address = new Address()
                            {
                                street = "crunch street",
                                City = "Benguluru",
                                State = "Karataka",
                                Country="India"
                            }
                         },
                        new Club()
                        {
                            Name = "Running Club 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first cinema",
                            ClubCategory = ClubCategory.FormulaE,
                            Address = new Address()
                            {
                                street = "Dubai Merina",
                                City = "Dubai",
                                State = "Dubai",
                                Country="UAE"
                            }
                        },
                        new Club()
                        {
                           Name = "Running Club 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.Formula2,
                            Address = new Address()
                            {
                                street = "Limo Street",
                                City = "Qatar City",
                                State = "Qatar",
                                Country="Qatar"
                            }
                        },
                        new Club()
                        {
                            Name = "Running Club 3",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            ClubCategory = ClubCategory.Roadclub,
                            Address = new Address()
                            {
                                street = "Billionear Street",
                                City = "New York City",
                                State = "New York",
                                Country="U.S.A"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<Race>()
                    {
                        new Race()
                        {
                            Name = "Running Race 1",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            RaceCategory = RaceCategory.IndianGrandprix,
                            Address = new Address()
                            {
                                 street = "Billionear Street",
                                City = "New York City",
                                State = "New York",
                                Country="U.S.A"
                            }
                        },
                        new Race()
                        {
                            Name = "Running Race 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            RaceCategory = RaceCategory.DubaiGrandPrix,
                            AddressId = 5,
                            Address = new Address()
                            {
                                street = "Limo Street",
                                City = "Qatar City",
                                State = "Qatar",
                                Country="Qatar"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "Admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "Mr.Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            street = "Billioner street",
                            City = "Florida city",
                            State = "Florida",
                            Country="USA"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "Mr Chal",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            street = "duabi merina",
                            City = "Dubai city",
                            State = "Dubai",
                            Country="UAE"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
