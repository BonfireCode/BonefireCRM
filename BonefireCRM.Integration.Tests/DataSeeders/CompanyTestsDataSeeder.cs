using Bogus;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using BonefireCRM.Integration.Tests.Common;

namespace BonefireCRM.Integration.Tests.DataSeeders
{
    internal class CompanyTestsDataSeeder
    {
        internal List<Company> Companies { get; private set; } = [];

        internal async Task PopulateWithTestDataAsync(CRMContext crmContext, ApplicationUser appUser)
        {
            Randomizer.Seed = new Random(TestConstants.DATASEED);

            var companies = PrepareFakeCompanies();
            await crmContext.AddRangeAsync(companies);
            await crmContext.SaveChangesAsync();

            Companies = companies;
        }

        private List<Company> PrepareFakeCompanies()
        {
            var companiesFaker = new Faker<Company>().Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Industry = f.Commerce.Department();
                c.Address = f.Address.FullAddress();
                c.PhoneNumber = f.Phone.PhoneNumber();
            });

            return companiesFaker.Generate(3);
        }
    }
}
