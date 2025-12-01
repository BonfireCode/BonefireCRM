using Bogus;
using BonefireCRM.Infrastructure.Security;
using BonefireCRM.Integration.Tests.Common;

namespace BonefireCRM.Integration.Tests.DataSeeders
{
    internal class AppUserTestsDataSeeder
    {
        internal async Task<ApplicationUser> PopulateWithTestDataAsync(AppDbContext appDbContext)
        {
            Randomizer.Seed = new Random(TestConstants.DATASEED);

            var appUser = PrepareFakeAppUser();
            await appDbContext.AddAsync(appUser);
            await appDbContext.SaveChangesAsync();

            return appUser;
        }

        private ApplicationUser PrepareFakeAppUser()
        {
            var appUserFaker = new Faker<ApplicationUser>().Rules((f, au) =>
            {
                au.UserName = TestConstants.USERNAME;
                au.Email = TestConstants.USEREMAIL;

                // In real scenarios, hash the password properly
                au.PasswordHash = TestConstants.USERPASSWORD;
            });

            return appUserFaker.Generate();
        }
    }
}
