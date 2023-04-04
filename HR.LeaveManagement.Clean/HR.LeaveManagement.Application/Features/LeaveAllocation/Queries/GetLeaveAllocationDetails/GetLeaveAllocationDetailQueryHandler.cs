using System;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
	public class GetLeaveAllocationDetailQueryHandler:IRequestHandler<GetLeaveAllocationDetailQuery,LeaveAllocationDetailsDto>
	{
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetailQueryHandler(ILeaveAllocationRepository leaveAllocationRepository,IMapper mapper)
		{
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._mapper = mapper;
        }

        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
            if (leaveAllocation is null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            return _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocation); 
        }
    }
}

