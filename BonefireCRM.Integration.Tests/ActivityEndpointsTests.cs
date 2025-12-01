using Bogus;
using BonefireCRM.API.Activity.Endpoints.Assignment;
using BonefireCRM.API.Activity.Endpoints.Call;
using BonefireCRM.API.Activity.Endpoints.Meeting;
using BonefireCRM.API.Contrat.Activity.Assignment;
using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Integration.Tests.Common;
using BonefireCRM.Integration.Tests.DataSeeders;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BonefireCRM.Integration.Tests
{
    public class ActivityEndpointsTests : TestBase<ApiTestFixture>, IAsyncLifetime
    {
        private readonly ApiTestFixture _apiTestFixture;
        private readonly ActivityTestsDataSeeder _activityTestsDataSeeder;

        public ActivityEndpointsTests(ApiTestFixture apiTestFixture)
        {
            _apiTestFixture = apiTestFixture;
            _activityTestsDataSeeder = new ActivityTestsDataSeeder();
        }

        public async ValueTask InitializeAsync()
        {
            await _apiTestFixture.SeedTestDatabaseAsync(_activityTestsDataSeeder.PopulateWithTestDataAsync);
        }

        [Fact]
        public async Task GetAllCalls_NoFilterApplied_ReturnsAllCalls()
        {
            // Arrange
            var request = new GetCallsRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllCallsEndpoint, GetCallsRequest, IEnumerable<GetCallResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(_activityTestsDataSeeder.Calls.Count);
        }

        [Fact]
        public async Task GetAllCalls_OneCriteria_ReturnsAllCallsMatching()
        {
            // Arrange
            var call = _activityTestsDataSeeder.Calls.First();
            var request = new GetCallsRequest
            {
                Id = call.Id,
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllCallsEndpoint, GetCallsRequest, IEnumerable<GetCallResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(1);
            outcome.Result.Single().Id
                .Should().Be(call.Id);
        }

        [Fact]
        public async Task GetAllCalls_NoMatchCriteria_ReturnsNoCalls()
        {
            // Arrange
            var request = new GetCallsRequest
            {
                Notes = "Non-existing description",
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllCallsEndpoint, GetCallsRequest, IEnumerable<GetCallResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public async Task GetCall_ById_ReturnsCall()
        {
            // Arrange
            var call = _activityTestsDataSeeder.Calls.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetCallResponse>($"/api/activity/calls/{call.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull();
            outcome.Result.Id
                .Should().Be(call.Id);
            outcome.Result.ContactId
                .Should().Be(call.ContactId);
            outcome.Result.Notes
                .Should().Be(call.Notes);
        }

        [Fact]
        public async Task GetCall_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingCallId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetCallResponse>($"/api/activity/calls/{nonExistingCallId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls
                .Should().NotContain(c => c.Id == nonExistingCallId);
        }

        [Fact]
        public async Task CreateCall_WithRequest_ReturnsCreatedCall()
        {
            // Arrange
            var contact = _activityTestsDataSeeder.Contacts.First();

            var request = new Faker<CreateCallRequest>().Rules((f, c) =>
            {
                c.ContactId = contact.Id;
                c.CallTime = f.Date.Recent();
                c.Duration = TimeSpan.FromMinutes(f.Random.Int(5, 60));
                c.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateCallEndpoint, CreateCallRequest, CreateCallResponse>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.Created);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls
                .Should().Contain(c => c.Id == outcome.Result.Id);
        }

        [Fact]
        public async Task CreateCall_WithEmptyRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new CreateCallRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateCallEndpoint, CreateCallRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.BadRequest);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls
                .Should().HaveCount(_activityTestsDataSeeder.Calls.Count);
        }

        [Fact]
        public async Task CreateCall_WithInvalidRequest_ReturnsError()
        {
            // Arrange
            var request = new Faker<CreateCallRequest>().Rules((f, c) =>
            {
                c.ContactId = Guid.NewGuid();
                c.CallTime = f.Date.Recent();
                c.Duration = TimeSpan.FromMinutes(f.Random.Int(5, 60));
                c.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateCallEndpoint, CreateCallRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.InternalServerError);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls
                .Should().HaveCount(_activityTestsDataSeeder.Calls.Count);
        }

        [Fact]
        public async Task UpdateCall_WithRequest_ReturnsUpdatedCall()
        {
            // Arrange
            var contact = _activityTestsDataSeeder.Contacts.First();

            var call = _activityTestsDataSeeder.Calls.First();

            var request = new Faker<UpdateCallRequest>().Rules((f, c) =>
            {
                c.ContactId = contact.Id;
                c.CallTime = f.Date.Soon();
                c.Duration = TimeSpan.FromMinutes(f.Random.Int(5, 60));
                c.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateCallRequest, UpdateCallResponse>($"/api/activity/calls/{call.Id}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request)
                .And.NotBeEquivalentTo(call);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls
                .Should().HaveCount(_activityTestsDataSeeder.Calls.Count)
                .And.Contain(c => c.Id == outcome.Result.Id && c.CallTime == outcome.Result.CallTime);
        }

        [Fact]
        public async Task UpdateCall_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingCallId = Guid.NewGuid();

            var request = new Faker<UpdateCallRequest>().Rules((f, c) =>
            {
                c.ContactId = Guid.NewGuid();
                c.CallTime = f.Date.Soon();
                c.Duration = TimeSpan.FromMinutes(f.Random.Int(5, 60));
                c.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateCallRequest, UpdateCallResponse>($"/api/activity/calls/{nonExistingCallId}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls
                .Should().HaveCount(_activityTestsDataSeeder.Calls.Count)
                .And.NotContain(c => c.Id == nonExistingCallId && c.Notes == request.Notes);
        }

        [Fact]
        public async Task DeleteCall_ById_ReturnsNoContentAndDeletes()
        {
            // Arrange
            var call = _activityTestsDataSeeder.Calls.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/activity/calls/{call.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NoContent);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls
                .Should().HaveCount(_activityTestsDataSeeder.Calls.Count - 1)
                .And.NotContain(c => c.Id == call.Id);
        }

        [Fact]
        public async Task DeleteCall_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingCallId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/activity/calls/{nonExistingCallId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingCalls = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Calls.ToListAsync());
            existingCalls.Count
                .Should().Be(_activityTestsDataSeeder.Calls.Count);
            existingCalls
                .Should().NotContain(c => c.Id == nonExistingCallId);
        }

        [Fact]
        public async Task GetAllMeetings_NoFilterApplied_ReturnsAllMeetings()
        {
            // Arrange
            var request = new GetMeetingsRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllMeetingsEndpoint, GetMeetingsRequest, IEnumerable<GetMeetingResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(_activityTestsDataSeeder.Meetings.Count);
        }

        [Fact]
        public async Task GetAllMeetings_OneCriteria_ReturnsAllMeetingsMatching()
        {
            // Arrange
            var meeting = _activityTestsDataSeeder.Meetings.First();
            var request = new GetMeetingsRequest
            {
                Id = meeting.Id,
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllMeetingsEndpoint, GetMeetingsRequest, IEnumerable<GetMeetingResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(1);
            outcome.Result.Single().Id
                .Should().Be(meeting.Id);
        }

        [Fact]
        public async Task GetAllMeetings_NoMatchCriteria_ReturnsNoMeetings()
        {
            // Arrange
            var request = new GetMeetingsRequest
            {
                Subject = "Non-existing subject",
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllMeetingsEndpoint, GetMeetingsRequest, IEnumerable<GetMeetingResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public async Task GetMeeting_ById_ReturnsMeeting()
        {
            // Arrange
            var meeting = _activityTestsDataSeeder.Meetings.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetMeetingResponse>($"/api/activity/meetings/{meeting.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull();
            outcome.Result.Id
                .Should().Be(meeting.Id);
            outcome.Result.ContactId
                .Should().Be(meeting.ContactId);
            outcome.Result.Subject
                .Should().Be(meeting.Subject);
        }

        [Fact]
        public async Task GetMeeting_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingMeetingId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetMeetingResponse>($"/api/activity/meetings/{nonExistingMeetingId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings
                .Should().NotContain(m => m.Id == nonExistingMeetingId);
        }

        [Fact]
        public async Task CreateMeeting_WithRequest_ReturnsCreatedMeeting()
        {
            // Arrange
            var contact = _activityTestsDataSeeder.Contacts.First();

            var request = new Faker<CreateMeetingRequest>().Rules((f, m) =>
            {
                m.ContactId = contact.Id;
                m.StartTime = f.Date.Soon();
                m.EndTime = f.Date.Soon();
                m.Subject = f.Lorem.Sentence();
                m.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateMeetingEndpoint, CreateMeetingRequest, CreateMeetingResponse>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.Created);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings
                .Should().Contain(m => m.Id == outcome.Result.Id);
        }

        [Fact]
        public async Task CreateMeeting_WithEmptyRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new CreateMeetingRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateMeetingEndpoint, CreateMeetingRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.BadRequest);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings
                .Should().HaveCount(_activityTestsDataSeeder.Meetings.Count);
        }

        [Fact]
        public async Task CreateMeeting_WithInvalidRequest_ReturnsError()
        {
            // Arrange
            var request = new Faker<CreateMeetingRequest>().Rules((f, m) =>
            {
                m.ContactId = Guid.NewGuid();
                m.StartTime = f.Date.Soon();
                m.EndTime = f.Date.Soon();
                m.Subject = f.Lorem.Sentence();
                m.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateMeetingEndpoint, CreateMeetingRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.InternalServerError);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings
                .Should().HaveCount(_activityTestsDataSeeder.Meetings.Count);
        }

        [Fact]
        public async Task UpdateMeeting_WithRequest_ReturnsUpdatedMeeting()
        {
            // Arrange
            var contact = _activityTestsDataSeeder.Contacts.First();

            var meeting = _activityTestsDataSeeder.Meetings.First();

            var request = new Faker<UpdateMeetingRequest>().Rules((f, m) =>
            {
                m.ContactId = contact.Id;
                m.StartTime = f.Date.Soon();
                m.EndTime = f.Date.Soon();
                m.Subject = f.Lorem.Sentence();
                m.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateMeetingRequest, UpdateMeetingResponse>($"/api/activity/meetings/{meeting.Id}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request)
                .And.NotBeEquivalentTo(meeting);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings
                .Should().HaveCount(_activityTestsDataSeeder.Meetings.Count)
                .And.Contain(m => m.Id == outcome.Result.Id && m.Subject == outcome.Result.Subject);
        }

        [Fact]
        public async Task UpdateMeeting_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var contact = _activityTestsDataSeeder.Contacts.First();

            var nonExistingMeetingId = Guid.NewGuid();

            var request = new Faker<UpdateMeetingRequest>().Rules((f, m) =>
            {
                m.ContactId = contact.Id;
                m.StartTime = f.Date.Soon();
                m.EndTime = f.Date.Soon();
                m.Subject = f.Lorem.Sentence();
                m.Notes = f.Lorem.Sentences(2);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateMeetingRequest, UpdateMeetingResponse>($"/api/activity/meetings/{nonExistingMeetingId}", request);
                
            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings
                .Should().HaveCount(_activityTestsDataSeeder.Meetings.Count)
                .And.NotContain(m => m.Id == nonExistingMeetingId && m.Subject == request.Subject);
        }

        [Fact]
        public async Task DeleteMeeting_ById_ReturnsNoContentAndDeletes()
        {
            // Arrange
            var meeting = _activityTestsDataSeeder.Meetings.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/activity/meetings/{meeting.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NoContent);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings
                .Should().HaveCount(_activityTestsDataSeeder.Meetings.Count - 1)
                .And.NotContain(m => m.Id == meeting.Id);
        }

        [Fact]
        public async Task DeleteMeeting_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingMeetingId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/activity/meetings/{nonExistingMeetingId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingMeetings = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Meetings.ToListAsync());
            existingMeetings.Count
                .Should().Be(_activityTestsDataSeeder.Meetings.Count);
            existingMeetings
                .Should().NotContain(m => m.Id == nonExistingMeetingId);
        }

        [Fact]
        public async Task GetAllAssignments_NoFilterApplied_ReturnsAllAssignments()
        {
            // Arrange
            var request = new GetAssignmentsRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllAssignmentsEndpoint, GetAssignmentsRequest, IEnumerable<GetAssignmentResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(_activityTestsDataSeeder.Assignments.Count);
        }

        [Fact]
        public async Task GetAllAssignments_OneCriteria_ReturnsAllAssignmentsMatching()
        {
            // Arrange
            var assignment = _activityTestsDataSeeder.Assignments.First();
            var request = new GetAssignmentsRequest
            {
                Id = assignment.Id,
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllAssignmentsEndpoint, GetAssignmentsRequest, IEnumerable<GetAssignmentResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(1);
            outcome.Result.Single().Id
                .Should().Be(assignment.Id);
        }

        [Fact]
        public async Task GetAllAssignments_NoMatchCriteria_ReturnsNoAssignments()
        {
            // Arrange
            var request = new GetAssignmentsRequest
            {
                Subject = "testing",
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllAssignmentsEndpoint, GetAssignmentsRequest, IEnumerable<GetAssignmentResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public async Task GetAssignment_ById_ReturnsAssignment()
        {
            // Arrange
            var assignment = _activityTestsDataSeeder.Assignments.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetAssignmentResponse>($"/api/activity/assignments/{assignment.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull();
            outcome.Result.Id
                .Should().Be(assignment.Id);
            outcome.Result.ContactId
                .Should().Be(assignment.ContactId);
            outcome.Result.Subject
                .Should().Be(assignment.Subject);
        }

        [Fact]
        public async Task GetAssignment_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingAssignmentId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetAssignmentResponse>($"/api/activity/assignments/{nonExistingAssignmentId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments
                .Should().NotContain(a => a.Id == nonExistingAssignmentId);
        }

        [Fact]
        public async Task CreateAssignment_WithRequest_ReturnsCreatedAssignment()
        {
            // Arrange
            var contact = _activityTestsDataSeeder.Contacts.First();

            var assignment = _activityTestsDataSeeder.Assignments.First();

            var request = new Faker<CreateAssignmentRequest>().Rules((f, a) =>
            {
                a.ContactId = contact.Id;
                a.Subject = f.Lorem.Sentence();
                a.Description = f.Lorem.Paragraph();
                a.DueDate = f.Date.Soon();
                a.IsCompleted = f.Random.Bool();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateAssignmentEndpoint, CreateAssignmentRequest, CreateAssignmentResponse>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.Created);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments
                .Should().Contain(a => a.Id == outcome.Result.Id);
        }

        [Fact]
        public async Task CreateAssignment_WithEmptyRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new CreateAssignmentRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateAssignmentEndpoint, CreateAssignmentRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.BadRequest);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments
                .Should().HaveCount(_activityTestsDataSeeder.Assignments.Count);
        }

        [Fact]
        public async Task CreateAssignment_WithInvalidRequest_ReturnsError()
        {
            // Arrange
            var request = new Faker<CreateAssignmentRequest>().Rules((f, a) =>
            {
                a.ContactId = Guid.NewGuid();
                a.Subject = f.Lorem.Sentence();
                a.Description = f.Lorem.Paragraph();
                a.DueDate = f.Date.Soon();
                a.IsCompleted = false;
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateAssignmentEndpoint, CreateAssignmentRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.InternalServerError);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments
                .Should().HaveCount(_activityTestsDataSeeder.Assignments.Count);
        }

        [Fact]
        public async Task UpdateAssignment_WithRequest_ReturnsUpdatedAssignment()
        {
            // Arrange
            var contact = _activityTestsDataSeeder.Contacts.First();

            var assignment = _activityTestsDataSeeder.Assignments.First();

            var request = new Faker<UpdateAssignmentRequest>().Rules((f, a) =>
            {
                a.ContactId = contact.Id;
                a.Subject = f.Lorem.Sentence();
                a.Description = f.Lorem.Paragraph();
                a.DueDate = f.Date.Soon();
                a.IsCompleted = f.Random.Bool();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateAssignmentRequest, UpdateAssignmentResponse>($"/api/activity/assignments/{assignment.Id}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request)
                .And.NotBeEquivalentTo(assignment);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments
                .Should().HaveCount(_activityTestsDataSeeder.Assignments.Count)
                .And.Contain(a => a.Id == outcome.Result.Id && a.Subject == outcome.Result.Subject);
        }

        [Fact]
        public async Task UpdateAssignment_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingAssignmentId = Guid.NewGuid();

            var request = new Faker<UpdateAssignmentRequest>().Rules((f, a) =>
            {
                a.ContactId = Guid.NewGuid();
                a.Subject = f.Lorem.Sentence();
                a.Description = f.Lorem.Paragraph();
                a.DueDate = f.Date.Soon();
                a.IsCompleted = f.Random.Bool();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateAssignmentRequest, UpdateAssignmentResponse>($"/api/activity/assignments/{nonExistingAssignmentId}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments
                .Should().HaveCount(_activityTestsDataSeeder.Assignments.Count)
                .And.NotContain(a => a.Id == nonExistingAssignmentId && a.Subject == request.Subject);
        }

        [Fact]
        public async Task DeleteAssignment_ById_ReturnsNoContentAndDeletes()
        {
            // Arrange
            var assignment = _activityTestsDataSeeder.Assignments.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/activity/assignments/{assignment.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NoContent);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments
                .Should().HaveCount(_activityTestsDataSeeder.Assignments.Count - 1)
                .And.NotContain(a => a.Id == assignment.Id);
        }

        [Fact]
        public async Task DeleteAssignment_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingAssignmentId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/activity/assignments/{nonExistingAssignmentId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingAssignments = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Assignments.ToListAsync());
            existingAssignments.Count
                .Should().Be(_activityTestsDataSeeder.Assignments.Count);
            existingAssignments
                .Should().NotContain(a => a.Id == nonExistingAssignmentId);
        }

        public async ValueTask DisposeAsync()
        {
            await _apiTestFixture.ResetDatabaseAsync();
        }
    }
}
