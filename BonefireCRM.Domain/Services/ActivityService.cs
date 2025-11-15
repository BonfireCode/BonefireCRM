using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Assignment;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class ActivityService
    {
        private readonly IBaseRepository<Call> _callRepository;
        private readonly IBaseRepository<Meeting> _meetingRepository;
        private readonly IBaseRepository<Assignment> _assignmentRepository;

        public ActivityService(
            IBaseRepository<Call> callRepository
            , IBaseRepository<Meeting> meetingRepository
            , IBaseRepository<Assignment> assignmentRepository)
        {
            _callRepository = callRepository;
            _meetingRepository = meetingRepository;
            _assignmentRepository = assignmentRepository;
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

        public IEnumerable<GetCallDTO> GetAllCalls(GetAllCallsDTO getAllCallsDTO, CancellationToken ct)
        {
            var filterExpression = CallQueryExpressions.Filter(getAllCallsDTO);

            var sortExpression = CallQueryExpressions.Sort(getAllCallsDTO.SortBy);

            var skip = (getAllCallsDTO.PageNumber - 1) * getAllCallsDTO.PageSize;
            var take = getAllCallsDTO.PageSize;

            var calls = _callRepository.GetAll(filterExpression, sortExpression, getAllCallsDTO.SortDirection, skip, take, ct);

            var getCallsResultDTO = calls.Select(c => c.MapToGetDto());

            return getCallsResultDTO;
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

        public IEnumerable<GetMeetingDTO> GetAllMeetings(GetAllMeetingsDTO getAllMeetingsDTO, CancellationToken ct)
        {
            var filterExpression = MeetingQueryExpressions.Filter(getAllMeetingsDTO);

            var sortExpression = MeetingQueryExpressions.Sort(getAllMeetingsDTO.SortBy);

            var skip = (getAllMeetingsDTO.PageNumber - 1) * getAllMeetingsDTO.PageSize;
            var take = getAllMeetingsDTO.PageSize;

            var meetings = _meetingRepository.GetAll(filterExpression, sortExpression, getAllMeetingsDTO.SortDirection, skip, take, ct);

            var getMeetingsResultDTO = meetings.Select(m => m.MapToGetDto());

            return getMeetingsResultDTO;
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

        public async Task<Option<GetAssignmentDTO>> GetAssignmentAsync(Guid id, CancellationToken ct)
        {
            var assignment = await _assignmentRepository.GetAsync(id, ct);
            if (assignment is null)
            {
                return Option<GetAssignmentDTO>.None;
            }

            return assignment.MapToGetDto();
        }

        public IEnumerable<GetAssignmentDTO> GetAllAssignments(GetAllAssignmentsDTO getAllAssignmentsDTO, CancellationToken ct)
        {
            var filterExpression = AssignmentQueryExpressions.Filter(getAllAssignmentsDTO);

            var sortExpression = AssignmentQueryExpressions.Sort(getAllAssignmentsDTO.SortBy);

            var skip = (getAllAssignmentsDTO.PageNumber - 1) * getAllAssignmentsDTO.PageSize;
            var take = getAllAssignmentsDTO.PageSize;

            var tasks = _assignmentRepository.GetAll(filterExpression, sortExpression, getAllAssignmentsDTO.SortDirection, skip, take, ct);

            var getAssignmentsResultDTO = tasks.Select(t => t.MapToGetDto());

            return getAssignmentsResultDTO;
        }

        public async Task<bool> DeleteAssignmentAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var assignment = new Assignment { Id = id };

            var isDeleted = await _assignmentRepository.DeleteAsync(assignment, ct);

            return isDeleted;
        }

        public async Task<Fin<CreatedAssignmentDTO>> CreateAssignmentAsync(CreateAssignmentDTO createAssignmentDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var assignment = createAssignmentDTO.MapToAssignment();

            var createdAssignment = await _assignmentRepository.AddAsync(assignment, ct);
            if (createdAssignment is null)
            {
                return Fin<CreatedAssignmentDTO>.Fail(new AddEntityException<Assignment>());
            }

            return createdAssignment.MapToCreatedDto();
        }

        public async Task<Fin<UpdatedAssignmentDTO>> UpdateAssignmentAsync(UpdateAssignmentDTO updateAssignmentDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var assignment = updateAssignmentDTO.MapToAssignment();

            var updatedTask = await _assignmentRepository.UpdateAsync(assignment, ct);
            if (updatedTask is null)
            {
                return Fin<UpdatedAssignmentDTO>.Fail(new UpdateEntityException<Assignment>());
            }

            return updatedTask.MapToUpdatedDto();
        }

    }
}
