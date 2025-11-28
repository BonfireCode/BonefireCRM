using Bogus;
using BonefireCRM.Infrastructure.Security;
using BonefireCRM.Integration.Tests.Common;

namespace BonefireCRM.Integration.Tests.DataSeeders
{
    internal class AppUserTestsDataSeeder
    {
        internal static ApplicationUser AppUser { get; private set; } = default!;

        internal static async Task PopulateWithTestDataAsync(AppDbContext appDbContext)
        {
            Randomizer.Seed = new Random(TestConstants.DATASEED);

            var appUser = PrepareFakeAppUser();
            await appDbContext.AddAsync(appUser);
            await appDbContext.SaveChangesAsync();

            AppUser = appUser;
        }

        private static ApplicationUser PrepareFakeAppUser()
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
