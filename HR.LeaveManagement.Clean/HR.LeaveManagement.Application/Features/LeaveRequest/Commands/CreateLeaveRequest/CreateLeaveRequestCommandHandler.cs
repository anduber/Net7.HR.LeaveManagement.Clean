using System;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
	public class CreateLeaveRequestCommandHandler:IRequestHandler<CreateLeaveRequestCommand,Unit>
	{
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public CreateLeaveRequestCommandHandler(IMapper mapper,ILeaveTypeRepository leaveTypeRepository,ILeaveRequestRepository leaveRequestRepository)
		{
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await new CreateLeaveRequestCommandValidator(_leaveTypeRepository).ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid Leave Request", validationResult);

            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            return Unit.Value;
        }
    }
}

