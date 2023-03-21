using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeQueryHandler : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
    {
        public IMapper _mapper { get; }
        public ILeaveTypeRepository _leaveTypeRepository { get; }

        public GetLeaveTypeQueryHandler(IMapper mapper,ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }

    

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAsync();
            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }
    }
}
