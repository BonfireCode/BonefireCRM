using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Task;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class ActivityService
    {
        private readonly IBaseRepository<Call> _callRepository;
        private readonly IBaseRepository<Meeting> _meetingRepository;
        private readonly IBaseRepository<Assignment> _taskRepository;

        public ActivityService(
            IBaseRepository<Call> callRepository
            , IBaseRepository<Meeting> meetingRepository
            , IBaseRepository<Assignment> taskRepository)
        {
            _callRepository = callRepository;
            _meetingRepository = meetingRepository;
            _taskRepository = taskRepository;
        }

        public async Task<Option<GetCallDTO>> GetCallAsync(Guid id, CancellationToken ct)
        {
            var call = await _callRepository.GetAsync(id, ct);
            if (call is null)
            {
                return Option<GetCallDTO>.None;
            }

            return call.MapToGetDto();
        }

        public async Task<bool> DeleteCallAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var call = new Call { Id = id };

            var isDeleted = await _callRepository.DeleteAsync(call, ct);

            return isDeleted;
        }

        public async Task<Fin<CreatedCallDTO>> CreateCallAsync(CreateCallDTO createCallDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var call = createCallDTO.MapToCall();

            var createdCall = await _callRepository.AddAsync(call, ct);
            if (createdCall is null)
            {
                return Fin<CreatedCallDTO>.Fail(new AddEntityException<Meeting>());
            }

            return createdCall.MapToCreatedDto();
        }

        public async Task<Fin<UpdatedCallDTO>> UpdateCallAsync(UpdateCallDTO updateCallDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var call = updateCallDTO.MapToCall();

            var updatedCall = await _callRepository.UpdateAsync(call, ct);
            if (updatedCall is null)
            {
                return Fin<UpdatedCallDTO>.Fail(new UpdateEntityException<Meeting>());
            }

            return updatedCall.MapToUpdatedDto();
        }

        public async Task<Option<GetMeetingDTO>> GetMeetingAsync(Guid id, CancellationToken ct)
        {
            var meeting = await _meetingRepository.GetAsync(id, ct);
            if (meeting is null)
            {
                return Option<GetMeetingDTO>.None;
            }

            return meeting.MapToGetDto();
        }

        public async Task<bool> DeleteMeetingAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var meeting = new Meeting { Id = id };

            var isDeleted = await _meetingRepository.DeleteAsync(meeting, ct);

            return isDeleted;
        }

        public async Task<Fin<CreatedMeetingDTO>> CreateMeetingAsync(CreateMeetingDTO createMeetingDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var meeting = createMeetingDTO.MapToMeeting();

            var createdMeeting = await _meetingRepository.AddAsync(meeting, ct);
            if (createdMeeting is null)
            {
                return Fin<CreatedMeetingDTO>.Fail(new AddEntityException<Meeting>());
            }

            return createdMeeting.MapToCreatedDto();
        }

        public async Task<Fin<UpdatedMeetingDTO>> UpdateMeetingAsync(UpdateMeetingDTO updateMeetingDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var meeting = updateMeetingDTO.MapToMeeting();

            var updatedMeeting = await _meetingRepository.UpdateAsync(meeting, ct);
            if (updatedMeeting is null)
            {
                return Fin<UpdatedMeetingDTO>.Fail(new UpdateEntityException<Meeting>());
            }

            return updatedMeeting.MapToUpdatedDto();
        }

        public async Task<Option<GetTaskDTO>> GetTaskAsync(Guid id, CancellationToken ct)
        {
            var task = await _taskRepository.GetAsync(id, ct);
            if (task is null)
            {
                return Option<GetTaskDTO>.None;
            }

            return task.MapToGetDto();
        }

        public async Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var task = new Assignment { Id = id };

            var isDeleted = await _taskRepository.DeleteAsync(task, ct);

            return isDeleted;
        }

        public async Task<Fin<CreatedTaskDTO>> CreateTaskAsync(CreateTaskDTO createTaskDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var task = createTaskDTO.MapToAssignment();

            var createdTask = await _taskRepository.AddAsync(task, ct);
            if (createdTask is null)
            {
                return Fin<CreatedTaskDTO>.Fail(new AddEntityException<Task>());
            }

            return createdTask.MapToCreatedDto();
        }

        public async Task<Fin<UpdatedTaskDTO>> UpdateTaskAsync(UpdateTaskDTO updateTaskDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var task = updateTaskDTO.MapToAssignment();

            var updatedTask = await _taskRepository.UpdateAsync(task, ct);
            if (updatedTask is null)
            {
                return Fin<UpdatedTaskDTO>.Fail(new UpdateEntityException<Assignment>());
            }

            return updatedTask.MapToUpdatedDto();
        }

    }
}
