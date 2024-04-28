using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Infrastructure._Identity
{
    public static class ApplicationIdentityContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Mariam shindy",
                    Email = "Mariam.sh998@gmail.com",
                    UserName = "Mariam.Shindy",
                    PhoneNumber = "0109222543"
                }; 
                await userManager.CreateAsync(user,"P@ssw0rd");
            }
        }
    }
}
