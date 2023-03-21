using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Command.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveTypeCommandHandler(IMapper mapper,ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }


        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeToUpdate = mapper.Map<Domain.LeaveType>(request);
            await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
            return Unit.Value;

        }
    }
}
