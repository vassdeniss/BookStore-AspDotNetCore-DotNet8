using BookStore.Common;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Threading.Tasks;

namespace BookStore.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider services = scopedServices.ServiceProvider;

            RoleManager<IdentityRole<Guid>> roleManager 
                = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (!await roleManager.RoleExistsAsync(Roles.Customer))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Customer));
                    await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Company));
                    await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
                    await roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Employee));
                }
            })
            .GetAwaiter()
            .GetResult();

            return app;
        }
    }
}
