using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Command.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandHandler(IMapper mapper,ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }


        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateLeaveTypeCommandValidator(_leaveTypeRepository).ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave type", validationResult);

            var leaveTypeToUpdate = mapper.Map<Domain.LeaveType>(request);
            await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
            return Unit.Value;

        }
    }
}
