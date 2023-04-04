using System;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
	public class UpdateLeaveAllocationCommandHandler:IRequestHandler<UpdateLeaveAllocationCommand,Unit>
	{
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(ILeaveTypeRepository leaveTypeRepository,ILeaveAllocationRepository leaveAllocationRepository,IMapper mapper)
		{
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository).ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Allocation", validationResult);

            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);
            if (validationResult is null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            _mapper.Map(request, leaveAllocation);
            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);
            return Unit.Value;
           
        }
    }
}

