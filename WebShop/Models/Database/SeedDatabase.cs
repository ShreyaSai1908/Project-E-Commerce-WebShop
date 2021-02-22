using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.Identity;

namespace WebShop.Models.Database
{
    public class SeedDatabase
    {
        public static IHost CreateDatabaseIfNotExist(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<WebDbContext>();
                    context.Database.Migrate();

                    if (!context.Roles.Any())// are there any roles? If not then this is a new database.
                    {
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();

                        roleManager.CreateAsync(new IdentityRole("Member")).Wait();

                        var userManager = services.GetRequiredService<UserManager<UserIdentity>>();

                        UserIdentity superAdmin = new UserIdentity() { UserName = "WebShopAdmin" };

                        userManager.CreateAsync(superAdmin, "Qwerty@123456").Wait();

                        superAdmin = userManager.FindByNameAsync("WebShopAdmin").Result;

                        userManager.AddToRoleAsync(superAdmin, "Admin").Wait();


                    }

                    return host;
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
    }
}
