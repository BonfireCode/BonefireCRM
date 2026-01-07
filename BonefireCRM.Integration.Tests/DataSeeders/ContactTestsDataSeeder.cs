using Bogus;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using BonefireCRM.Integration.Tests.Common;

namespace BonefireCRM.Integration.Tests.DataSeeders
{
    internal class ContactTestsDataSeeder
    {
        internal List<Contact> Contacts { get; private set; } = [];
        internal List<Company> Companies { get; private set; } = [];
        internal List<LifecycleStage> LifecycleStages { get; private set; } = [];

        internal async Task PopulateWithTestDataAsync(CRMContext crmContext, ApplicationUser appUser)
        {
            Randomizer.Seed = new Random(TestConstants.DATASEED);

            var user = PrepareFakeUser(appUser);
            await crmContext.AddAsync(user);
            await crmContext.SaveChangesAsync();

            var lifecycleStage = crmContext.LifecycleStages.First();

            var company = PrepareFakeCompany(user.Id);
            await crmContext.AddAsync(company);
            await crmContext.SaveChangesAsync();

            var contacts = PrepareFakeContacts(user.Id, company.Id, lifecycleStage.Id);
            await crmContext.AddRangeAsync(contacts);
            await crmContext.SaveChangesAsync();

            Companies.Add(company);
            LifecycleStages.Add(lifecycleStage);
            Contacts = contacts;
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

        private Company PrepareFakeCompany(Guid userId)
        {
            var companyFaker = new Faker<Company>().Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Address = f.Address.FullAddress();
                c.PhoneNumber = f.Phone.PhoneNumber();
                c.Industry = f.Random.String();
            });

            return companyFaker.Generate();
        }

        private List<Contact> PrepareFakeContacts(Guid userId, Guid companyId, Guid lifecycleStageId)
        {
            var contactsFaker = new Faker<Contact>().Rules((f, c) =>
            {
                c.FirstName = f.Name.FirstName();
                c.LastName = f.Name.LastName();
                c.Email = f.Internet.Email(c.FirstName, c.LastName);
                c.PhoneNumber = f.Phone.PhoneNumber();
                c.JobRole = f.Name.JobTitle();
                c.LifecycleStageId = lifecycleStageId;
                c.CompanyId = companyId;
                c.UserId = userId;
            });

            return contactsFaker.Generate(3);
        }
    }
}