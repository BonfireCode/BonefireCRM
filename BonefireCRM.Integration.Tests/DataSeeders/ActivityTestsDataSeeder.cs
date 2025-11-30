using Bogus;
using Bogus.DataSets;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using BonefireCRM.Integration.Tests.Common;

namespace BonefireCRM.Integration.Tests.DataSeeders
{
    internal class ActivityTestsDataSeeder
    {
        internal List<Call> Calls { get; private set; } = [];
        internal List<Meeting> Meetings { get; private set; } = [];
        internal List<Assignment> Assignments { get; private set; } = [];
        internal List<Contact> Contacts { get; private set; } = [];

        internal async Task PopulateWithTestDataAsync(CRMContext crmContext, ApplicationUser appUser)
        {
            Randomizer.Seed = new Random(TestConstants.DATASEED);

            var user = PrepareFakeUser(appUser);
            await crmContext.AddAsync(user);
            await crmContext.SaveChangesAsync();

            var lifecycleStage = crmContext.LifecycleStages.First();

            var contact = PrepareFakeContact(user.Id, lifecycleStage.Id);
            await crmContext.AddAsync(contact);
            await crmContext.SaveChangesAsync();

            var calls = PrepareFakeCalls(user.Id, contact.Id);
            await crmContext.AddRangeAsync(calls);
            await crmContext.SaveChangesAsync();

            var meetings = PrepareFakeMeetings(user.Id, contact.Id);
            await crmContext.AddRangeAsync(meetings);
            await crmContext.SaveChangesAsync();

            var assignments = PrepareFakeAssignments(user.Id, contact.Id);
            await crmContext.AddRangeAsync(assignments);
            await crmContext.SaveChangesAsync();

            Calls = calls;
            Meetings = meetings;
            Assignments = assignments;
            Contacts.Add(contact);
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

        private Contact PrepareFakeContact(Guid userId, Guid lifecycleStageId)
        {
            var contactsFaker = new Faker<Contact>().Rules((f, c) =>
            {
                c.FirstName = f.Name.FirstName();
                c.LastName = f.Name.LastName();
                c.Email = f.Internet.Email(c.FirstName, c.LastName);
                c.PhoneNumber = f.Phone.PhoneNumber();
                c.JobRole = f.Name.JobTitle();
                c.LifecycleStageId = lifecycleStageId;
                c.UserId = userId;
            });

            return contactsFaker.Generate();
        }

        private List<Call> PrepareFakeCalls(Guid userId, Guid contactId)
        {
            var callsFaker = new Faker<Call>().Rules((f, c) =>
            {
                c.UserId = userId;
                c.ContactId = contactId;
                c.CallTime = f.Date.Recent();
                c.Duration = TimeSpan.FromMinutes(f.Random.Int(5, 60));
                c.Notes = f.Lorem.Sentences(2);
            });

            return callsFaker.Generate(3);
        }

        private List<Meeting> PrepareFakeMeetings(Guid userId, Guid contactId)
        {
            var meetingsFaker = new Faker<Meeting>().Rules((f, m) =>
            {
                m.UserId = userId;
                m.ContactId = contactId;
                m.StartTime = f.Date.Soon();
                m.EndTime = f.Date.Soon();
                m.Subject = f.Lorem.Sentence();
                m.Notes = f.Lorem.Sentences(2);
            });

            return meetingsFaker.Generate(3);
        }

        private List<Assignment> PrepareFakeAssignments(Guid userId, Guid contactId)
        {
            var assignmentsFaker = new Faker<Assignment>().Rules((f, a) =>
            {
                a.UserId = userId;
                a.ContactId = contactId;
                a.Subject = f.Lorem.Sentence();
                a.Description = f.Lorem.Paragraph();
                a.DueDate = f.Date.Soon();
                a.IsCompleted = f.Random.Bool();
            });

            return assignmentsFaker.Generate(3);
        }
    }
}
