namespace Blog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "your email address"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ryan@ponderingcode.com",
                    Email = "ryan@ponderingcode.com",
                    FirstName = "Ryan",
                    LastName = "Fleming",
                    DisplayName = "Ryan Fleming",
                }, "CoderFoundry");
            }

            var userId = userManager.FindByEmail("ryan@ponderingcode.com").Id;
            userManager.AddToRole(userId, "Admin");
        }
    }
}