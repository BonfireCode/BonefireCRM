using AutoFixture;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Activity.Assignment;
using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Services;
using BonefireCRM.SourceGenerator;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace BonefireCRM.Domain.Tests
{
    public class ActivityServiceTests
    {
        private readonly IBaseRepository<Call> _callRepository;
        private readonly IBaseRepository<Meeting> _meetingRepository;
        private readonly IBaseRepository<Assignment> _assignmentRepository;
        private readonly IFixture _fixture;
        private readonly ActivityService _activityService;

        public ActivityServiceTests()
        {
            _callRepository = Substitute.For<IBaseRepository<Call>>();
            _meetingRepository = Substitute.For<IBaseRepository<Meeting>>();
            _assignmentRepository = Substitute.For<IBaseRepository<Assignment>>();
            _fixture = new Fixture();

            _activityService = new ActivityService(_callRepository, _meetingRepository, _assignmentRepository);
        }

        [Fact]
        public async Task GetCallAsync_NoCallFound_ReturnNoneAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _callRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _activityService.GetCallAsync(id, CancellationToken.None);

            await _callRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);

            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task GetCallAsync_CallFound_ReturnCallAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var call = _fixture.Build<Call>()
                .With(c => c.Id, id)
                .Create();

            _callRepository.GetByIdAsync(id, CancellationToken.None)
                .Returns(call);

            var expected = _fixture.Build<GetCallDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.CallTime, call.CallTime)
                .With(dto => dto.Duration, call.Duration)
                .Create();

            //Act
            var result = await _activityService.GetCallAsync(id, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).GetByIdAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.CallTime.Should().Be(expected.CallTime);
                dto.Duration.Should().Be(expected.Duration);
            });
        }

        [Fact]
        public async Task GetAllCalls_NoCallsFound_ReturnEmptyEnumerable()
        {
            // Arrange
            var getAllCallsDTO = _fixture.Build<GetAllCallsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .With(dto => dto.PageNumber, DefaultValues.PAGENUMBER)
                .With(dto => dto.PageSize, DefaultValues.PAGESIZE)
                .Create();

            var filterExpression = CallQueryExpressions.Filter(getAllCallsDTO);
            var sortExpression = CallQueryExpressions.Sort(getAllCallsDTO.SortBy);
            var skip = (getAllCallsDTO.PageNumber - 1) * getAllCallsDTO.PageSize;
            var take = getAllCallsDTO.PageSize;
            _callRepository.GetAll(
                    Arg.Is<Expression<Func<Call, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Call, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllCallsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([]);

            // Act
            var result = _activityService.GetAllCalls(getAllCallsDTO, CancellationToken.None);

            // Assert
            _callRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Call, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Call, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllCallsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCalls_OneCallFound_ReturnEnumerableWithCall()
        {
            // Arrange
            var getAllCallsDTO = _fixture.Build<GetAllCallsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .Create();

            var call = _fixture.Create<Call>();

            var filterExpression = CallQueryExpressions.Filter(getAllCallsDTO);
            var sortExpression = CallQueryExpressions.Sort(getAllCallsDTO.SortBy);
            var skip = (getAllCallsDTO.PageNumber - 1) * getAllCallsDTO.PageSize;
            var take = getAllCallsDTO.PageSize;
            _callRepository.GetAll(
                    Arg.Is<Expression<Func<Call, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Call, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllCallsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([call]);

            var expectedCall = _fixture.Build<GetCallDTO>()
                .With(dto => dto.Id, call.Id)
                .Create();

            // Act
            var result = _activityService.GetAllCalls(getAllCallsDTO, CancellationToken.None);

            // Assert
            _callRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Call, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Call, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllCallsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().HaveCount(1);
            var element = result.Single();
            element.Id.Should().Be(expectedCall.Id);
        }

        [Fact]
        public async Task DeleteCallAsync_RepositoryDeletesTrue_ReturnTrueAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _callRepository.DeleteAsync(id, CancellationToken.None)
                .Returns(true);

            //Act
            var result = await _activityService.DeleteCallAsync(id, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).DeleteAsync(id, CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateCallAsync_CreateSucceeds_ReturnCreatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            var createdCall = _fixture.Build<Call>()
                .With(c => c.Id, id)
                .Create();

            _callRepository.AddAsync(Arg.Any<Call>(), CancellationToken.None)
                .Returns(createdCall);

            var createDto = _fixture.Create<CreateCallDTO>();

            var expected = _fixture.Build<CreatedCallDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.CallTime, createdCall.CallTime)
                .With(dto => dto.Duration, createdCall.Duration)
                .Create();

            //Act
            var result = await _activityService.CreateCallAsync(createDto, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).AddAsync(Arg.Any<Call>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.CallTime.Should().Be(expected.CallTime);
                dto.Duration.Should().Be(expected.Duration);
            });
        }

        [Fact]
        public async Task CreateCallAsync_CreateFails_ReturnFailAsync()
        {
            // Arange
            _callRepository.AddAsync(Arg.Any<Call>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var createDto = _fixture.Create<CreateCallDTO>();

            //Act
            var result = await _activityService.CreateCallAsync(createDto, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).AddAsync(Arg.Any<Call>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateCallAsync_UpdateSucceeds_ReturnUpdatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var updatedCall = _fixture.Build<Call>()
                .With(c => c.Id, id)
                .Create();

            _callRepository.UpdateAsync(Arg.Any<Call>(), CancellationToken.None)
                .Returns(updatedCall);

            var updateDto = _fixture.Create<UpdateCallDTO>();

            var expected = _fixture.Build<UpdatedCallDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.CallTime, updatedCall.CallTime)
                .With(dto => dto.Duration, updatedCall.Duration)
                .Create();
            //Act
            var result = await _activityService.UpdateCallAsync(updateDto, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).UpdateAsync(Arg.Any<Call>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.CallTime.Should().Be(expected.CallTime);
                dto.Duration.Should().Be(expected.Duration);
            });
        }

        [Fact]
        public async Task UpdateCallAsync_UpdateFails_ReturnFailAsync()
        {
            // Arange
            _callRepository.UpdateAsync(Arg.Any<Call>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var updateDto = _fixture.Create<UpdateCallDTO>();

            //Act
            var result = await _activityService.UpdateCallAsync(updateDto, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).UpdateAsync(Arg.Any<Call>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task GetMeetingAsync_NoMeetingFound_ReturnNoneAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _meetingRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _activityService.GetMeetingAsync(id, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);

            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task GetMeetingAsync_MeetingFound_ReturnMeetingAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var meeting = _fixture.Build<Meeting>()
                .With(m => m.Id, id)
                .Create();

            _meetingRepository.GetByIdAsync(id, CancellationToken.None)
                .Returns(meeting);

            var expected = _fixture.Build<GetMeetingDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, meeting.Subject)
                .Create();

            //Act
            var result = await _activityService.GetMeetingAsync(id, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).GetByIdAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task GetAllMeetings_NoMeetingsFound_ReturnEmptyEnumerable()
        {
            // Arrange
            var getAllMeetingsDTO = _fixture.Build<GetAllMeetingsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .With(dto => dto.PageNumber, DefaultValues.PAGENUMBER)
                .With(dto => dto.PageSize, DefaultValues.PAGESIZE)
                .Create();

            var filterExpression = MeetingQueryExpressions.Filter(getAllMeetingsDTO);
            var sortExpression = MeetingQueryExpressions.Sort(getAllMeetingsDTO.SortBy);
            var skip = (getAllMeetingsDTO.PageNumber - 1) * getAllMeetingsDTO.PageSize;
            var take = getAllMeetingsDTO.PageSize;
            _meetingRepository.GetAll(
                    Arg.Is<Expression<Func<Meeting, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Meeting, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllMeetingsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([]);

            // Act
            var result = _activityService.GetAllMeetings(getAllMeetingsDTO, CancellationToken.None);

            // Assert
            _meetingRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Meeting, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Meeting, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllMeetingsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllMeetings_OneMeetingFound_ReturnEnumerableWithMeeting()
        {
            // Arrange
            var getAllMeetingsDTO = _fixture.Build<GetAllMeetingsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .Create();

            var meeting = _fixture.Create<Meeting>();

            var filterExpression = MeetingQueryExpressions.Filter(getAllMeetingsDTO);
            var sortExpression = MeetingQueryExpressions.Sort(getAllMeetingsDTO.SortBy);
            var skip = (getAllMeetingsDTO.PageNumber - 1) * getAllMeetingsDTO.PageSize;
            var take = getAllMeetingsDTO.PageSize;
            _meetingRepository.GetAll(
                    Arg.Is<Expression<Func<Meeting, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Meeting, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllMeetingsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([meeting]);

            var expectedMeeting = _fixture.Build<GetMeetingDTO>()
                .With(dto => dto.Id, meeting.Id)
                .Create();

            // Act
            var result = _activityService.GetAllMeetings(getAllMeetingsDTO, CancellationToken.None);

            // Assert
            _meetingRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Meeting, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Meeting, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllMeetingsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().HaveCount(1);
            var element = result.Single();
            element.Id.Should().Be(expectedMeeting.Id);
        }

        [Fact]
        public async Task DeleteMeetingAsync_RepositoryDeletesTrue_ReturnTrueAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _meetingRepository.DeleteAsync(id, CancellationToken.None)
                .Returns(true);

            //Act
            var result = await _activityService.DeleteMeetingAsync(id, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).DeleteAsync(id, CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateMeetingAsync_CreateSucceeds_ReturnCreatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            var createdMeeting = _fixture.Build<Meeting>()
                .With(m => m.Id, id)
                .Create();

            _meetingRepository.AddAsync(Arg.Any<Meeting>(), CancellationToken.None)
                .Returns(createdMeeting);

            var createDto = _fixture.Create<CreateMeetingDTO>();

            var expected = _fixture.Build<CreatedMeetingDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, createdMeeting.Subject)
                .Create();

            //Act
            var result = await _activityService.CreateMeetingAsync(createDto, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).AddAsync(Arg.Any<Meeting>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task CreateMeetingAsync_CreateFails_ReturnFailAsync()
        {
            // Arange
            _meetingRepository.AddAsync(Arg.Any<Meeting>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var createDto = _fixture.Create<CreateMeetingDTO>();

            //Act
            var result = await _activityService.CreateMeetingAsync(createDto, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).AddAsync(Arg.Any<Meeting>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateMeetingAsync_UpdateSucceeds_ReturnUpdatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var updatedMeeting = _fixture.Build<Meeting>()
                .With(m => m.Id, id)
                .Create();

            _meetingRepository.UpdateAsync(Arg.Any<Meeting>(), CancellationToken.None)
                .Returns(updatedMeeting);

            var updateDto = _fixture.Create<UpdateMeetingDTO>();

            var expected = _fixture.Build<UpdatedMeetingDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, updatedMeeting.Subject)
                .Create();

            //Act
            var result = await _activityService.UpdateMeetingAsync(updateDto, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).UpdateAsync(Arg.Any<Meeting>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task UpdateMeetingAsync_UpdateFails_ReturnFailAsync()
        {
            // Arange
            _meetingRepository.UpdateAsync(Arg.Any<Meeting>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var updateDto = _fixture.Create<UpdateMeetingDTO>();

            //Act
            var result = await _activityService.UpdateMeetingAsync(updateDto, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).UpdateAsync(Arg.Any<Meeting>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task GetAssignmentAsync_NoAssignmentFound_ReturnNoneAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _assignmentRepository.GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _activityService.GetAssignmentAsync(id, CancellationToken.None);

            //Assert
            await _assignmentRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None);

            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task GetAssignmentAsync_AssignmentFound_ReturnAssignmentAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var assignment = _fixture.Build<Assignment>()
                .With(t => t.Id, id)
                .Create();

            _assignmentRepository.GetByIdAsync(id, CancellationToken.None)
                .Returns(assignment);

            var expected = _fixture.Build<GetAssignmentDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, assignment.Subject)
                .Create();

            //Act
            var result = await _activityService.GetAssignmentAsync(id, CancellationToken.None);

            //Assert
            await _assignmentRepository.Received(1).GetByIdAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task GetAllAssignments_NoAssignmentsFound_ReturnEmptyEnumerable()
        {
            // Arrange
            var getAllAssignmentsDTO = _fixture.Build<GetAllAssignmentsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .With(dto => dto.PageNumber, DefaultValues.PAGENUMBER)
                .With(dto => dto.PageSize, DefaultValues.PAGESIZE)
                .Create();

            var filterExpression = AssignmentQueryExpressions.Filter(getAllAssignmentsDTO);
            var sortExpression = AssignmentQueryExpressions.Sort(getAllAssignmentsDTO.SortBy);
            var skip = (getAllAssignmentsDTO.PageNumber - 1) * getAllAssignmentsDTO.PageSize;
            var take = getAllAssignmentsDTO.PageSize;
            _assignmentRepository.GetAll(
                    Arg.Is<Expression<Func<Assignment, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Assignment, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllAssignmentsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([]);

            // Act
            var result = _activityService.GetAllAssignments(getAllAssignmentsDTO, CancellationToken.None);

            // Assert
            _assignmentRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Assignment, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Assignment, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllAssignmentsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAssignments_OneAssignmentFound_ReturnEnumerableWithAssignment()
        {
            // Arrange
            var getAllAssignmentsDTO = _fixture.Build<GetAllAssignmentsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .Create();

            var assignment = _fixture.Create<Assignment>();

            var filterExpression = AssignmentQueryExpressions.Filter(getAllAssignmentsDTO);
            var sortExpression = AssignmentQueryExpressions.Sort(getAllAssignmentsDTO.SortBy);
            var skip = (getAllAssignmentsDTO.PageNumber - 1) * getAllAssignmentsDTO.PageSize;
            var take = getAllAssignmentsDTO.PageSize;
            _assignmentRepository.GetAll(
                    Arg.Is<Expression<Func<Assignment, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Assignment, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllAssignmentsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([assignment]);

            var expectedTask = _fixture.Build<GetAssignmentDTO>()
                .With(dto => dto.Id, assignment.Id)
                .Create();

            // Act
            var result = _activityService.GetAllAssignments(getAllAssignmentsDTO, CancellationToken.None);

            // Assert
            _assignmentRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Assignment, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Assignment, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllAssignmentsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().HaveCount(1);
            var element = result.Single();
            element.Id.Should().Be(expectedTask.Id);
        }

        [Fact]
        public async Task DeleteAssignmentAsync_RepositoryDeletesTrue_ReturnTrueAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _assignmentRepository.DeleteAsync(id, CancellationToken.None)
                .Returns(true);

            //Act
            var result = await _activityService.DeleteAssignmentAsync(id, CancellationToken.None);

            //Assert
            await _assignmentRepository.Received(1).DeleteAsync(id, CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateAssignmentAsync_CreateSucceeds_ReturnCreatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            var createdTask = _fixture.Build<Assignment>()
                .With(t => t.Id, id)
                .Create();

            _assignmentRepository.AddAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .Returns(createdTask);

            var createDto = _fixture.Create<CreateAssignmentDTO>();

            var expected = _fixture.Build<CreatedAssignmentDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, createdTask.Subject)
                .Create();

            //Act
            var result = await _activityService.CreateAssignmentAsync(createDto, CancellationToken.None);

            await _assignmentRepository.Received(1).AddAsync(Arg.Any<Assignment>(), CancellationToken.None);

            //Assert
            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task CreateAssignmentAsync_CreateFails_ReturnFailAsync()
        {
            // Arange
            _assignmentRepository.AddAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var createDto = _fixture.Create<CreateAssignmentDTO>();

            //Act
            var result = await _activityService.CreateAssignmentAsync(createDto, CancellationToken.None);

            //Assert
            await _assignmentRepository.Received(1).AddAsync(Arg.Any<Assignment>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateAssignmentAsync_UpdateSucceeds_ReturnUpdatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var updatedTask = _fixture.Build<Assignment>()
                .With(t => t.Id, id)
                .Create();

            _assignmentRepository.UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .Returns(updatedTask);

            var updateDto = _fixture.Create<UpdateAssignmentDTO>();

            var expected = _fixture.Build<UpdatedAssignmentDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, updatedTask.Subject)
                .Create();

            //Act
            var result = await _activityService.UpdateAssignmentAsync(updateDto, CancellationToken.None);

            //Assert
            await _assignmentRepository.Received(1).UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task UpdateAssignmentAsync_UpdateFails_ReturnFailAsync()
        {
            // Arange
            _assignmentRepository.UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var updateDto = _fixture.Create<UpdateAssignmentDTO>();

            //Act
            var result = await _activityService.UpdateAssignmentAsync(updateDto, CancellationToken.None);

            //Assert
            await _assignmentRepository.Received(1).UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }
    }
}
