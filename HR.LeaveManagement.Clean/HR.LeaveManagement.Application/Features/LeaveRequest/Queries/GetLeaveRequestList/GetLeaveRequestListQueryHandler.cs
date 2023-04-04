using System;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
	public class GetLeaveRequestListQueryHandler:IRequestHandler<GetLeaveRequestListQuery,List<LeaveRequestListDto>>
	{
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestListQueryHandler(IMapper mapper,ILeaveRequestRepository leaveRequestRepository)
		{
            this._mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<LeaveRequestListDto>>(await _leaveRequestRepository.GetLeaveRequestsWithDetails());
        }
    }
}

