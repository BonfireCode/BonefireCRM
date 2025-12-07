using Bogus;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using BonefireCRM.Integration.Tests.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BonefireCRM.Integration.Tests.DataSeeders
{
    internal class UserTestsDataSeeder
    {
        internal List<User> Users { get; private set; } = [];

        internal async Task PopulateWithTestDataAsync(CRMContext crmContext, ApplicationUser appUser)
        {
            Randomizer.Seed = new Random(TestConstants.DATASEED);

            var user = PrepareFakeUser(appUser);
            await crmContext.AddAsync(user);
            await crmContext.SaveChangesAsync();

            Users.Add(user);
        }

        private static User PrepareFakeUser(ApplicationUser appUser)
        {
            var userFaker = new Faker<User>().Rules((f, u) =>
            {
                u.RegisterId = appUser.Id;
                u.UserName = appUser.UserName!;
                u.Email = appUser.Email!;
                u.FirstName = f.Name.FirstName();
                u.LastName = f.Name.LastName();
            });

            return userFaker.Generate();
        }
    }
}
