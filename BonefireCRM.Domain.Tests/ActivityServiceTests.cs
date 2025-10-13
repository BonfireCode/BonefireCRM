using AutoFixture;
using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Task;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BonefireCRM.Domain.Tests
{
    public class ActivityServiceTests
    {
        private readonly IBaseRepository<Call> _callRepository;
        private readonly IBaseRepository<Meeting> _meetingRepository;
        private readonly IBaseRepository<Assignment> _taskRepository;
        private readonly IFixture _fixture;
        private readonly ActivityService _activityService;

        public ActivityServiceTests()
        {
            _callRepository = Substitute.For<IBaseRepository<Call>>();
            _meetingRepository = Substitute.For<IBaseRepository<Meeting>>();
            _taskRepository = Substitute.For<IBaseRepository<Assignment>>();
            _fixture = new Fixture();

            _activityService = new ActivityService(_callRepository, _meetingRepository, _taskRepository);
        }

        [Fact]
        public async Task GetCallAsync_NoCallFound_ReturnNoneAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _callRepository.GetAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _activityService.GetCallAsync(id, CancellationToken.None);

            await _callRepository.Received(1).GetAsync(Arg.Any<Guid>(), CancellationToken.None);

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

            _callRepository.GetAsync(id, CancellationToken.None)
                .Returns(call);

            var expected = _fixture.Build<GetCallDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.CallTime, call.CallTime)
                .With(dto => dto.Duration, call.Duration)
                .Create();

            //Act
            var result = await _activityService.GetCallAsync(id, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).GetAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.CallTime.Should().Be(expected.CallTime);
                dto.Duration.Should().Be(expected.Duration);
            });
        }

        [Fact]
        public async Task DeleteCallAsync_RepositoryDeletesTrue_ReturnTrueAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _callRepository.DeleteAsync(Arg.Any<Call>(), CancellationToken.None)
                .Returns(true);

            //Act
            var result = await _activityService.DeleteCallAsync(id, CancellationToken.None);

            //Assert
            await _callRepository.Received(1).DeleteAsync(Arg.Any<Call>(), CancellationToken.None);

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

            _meetingRepository.GetAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _activityService.GetMeetingAsync(id, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).GetAsync(Arg.Any<Guid>(), CancellationToken.None);

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

            _meetingRepository.GetAsync(id, CancellationToken.None)
                .Returns(meeting);

            var expected = _fixture.Build<GetMeetingDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, meeting.Subject)
                .Create();

            //Act
            var result = await _activityService.GetMeetingAsync(id, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).GetAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task DeleteMeetingAsync_RepositoryDeletesTrue_ReturnTrueAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _meetingRepository.DeleteAsync(Arg.Any<Meeting>(), CancellationToken.None)
                .Returns(true);

            //Act
            var result = await _activityService.DeleteMeetingAsync(id, CancellationToken.None);

            //Assert
            await _meetingRepository.Received(1).DeleteAsync(Arg.Any<Meeting>(), CancellationToken.None);

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
        public async Task GetTaskAsync_NoTaskFound_ReturnNoneAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _taskRepository.GetAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _activityService.GetTaskAsync(id, CancellationToken.None);

            //Assert
            await _taskRepository.Received(1).GetAsync(Arg.Any<Guid>(), CancellationToken.None);

            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task GetTaskAsync_TaskFound_ReturnTaskAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var task = _fixture.Build<Assignment>()
                .With(t => t.Id, id)
                .Create();

            _taskRepository.GetAsync(id, CancellationToken.None)
                .Returns(task);

            var expected = _fixture.Build<GetTaskDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, task.Subject)
                .Create();

            //Act
            var result = await _activityService.GetTaskAsync(id, CancellationToken.None);

            //Assert
            await _taskRepository.Received(1).GetAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task DeleteTaskAsync_RepositoryDeletesTrue_ReturnTrueAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _taskRepository.DeleteAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .Returns(true);

            //Act
            var result = await _activityService.DeleteTaskAsync(id, CancellationToken.None);

            //Assert
            await _taskRepository.Received(1).DeleteAsync(Arg.Any<Assignment>(), CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateTaskAsync_CreateSucceeds_ReturnCreatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            var createdTask = _fixture.Build<Assignment>()
                .With(t => t.Id, id)
                .Create();

            _taskRepository.AddAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .Returns(createdTask);

            var createDto = _fixture.Create<CreateTaskDTO>();

            var expected = _fixture.Build<CreatedTaskDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, createdTask.Subject)
                .Create();

            //Act
            var result = await _activityService.CreateTaskAsync(createDto, CancellationToken.None);

            await _taskRepository.Received(1).AddAsync(Arg.Any<Assignment>(), CancellationToken.None);

            //Assert
            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task CreateTaskAsync_CreateFails_ReturnFailAsync()
        {
            // Arange
            _taskRepository.AddAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var createDto = _fixture.Create<CreateTaskDTO>();

            //Act
            var result = await _activityService.CreateTaskAsync(createDto, CancellationToken.None);

            //Assert
            await _taskRepository.Received(1).AddAsync(Arg.Any<Assignment>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateTaskAsync_UpdateSucceeds_ReturnUpdatedDtoAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var updatedTask = _fixture.Build<Assignment>()
                .With(t => t.Id, id)
                .Create();

            _taskRepository.UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .Returns(updatedTask);

            var updateDto = _fixture.Create<UpdateTaskDTO>();

            var expected = _fixture.Build<UpdatedTaskDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Subject, updatedTask.Subject)
                .Create();

            //Act
            var result = await _activityService.UpdateTaskAsync(updateDto, CancellationToken.None);

            //Assert
            await _taskRepository.Received(1).UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Subject.Should().Be(expected.Subject);
            });
        }

        [Fact]
        public async Task UpdateTaskAsync_UpdateFails_ReturnFailAsync()
        {
            // Arange
            _taskRepository.UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var updateDto = _fixture.Create<UpdateTaskDTO>();

            //Act
            var result = await _activityService.UpdateTaskAsync(updateDto, CancellationToken.None);

            //Assert
            await _taskRepository.Received(1).UpdateAsync(Arg.Any<Assignment>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }
    }
}
