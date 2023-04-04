using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Logging;
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
        private readonly IAppLogger<GetLeaveTypeQueryHandler> _logger;
        public IMapper _mapper { get; }
        public ILeaveTypeRepository _leaveTypeRepository { get; }

        public GetLeaveTypeQueryHandler(IMapper mapper,ILeaveTypeRepository leaveTypeRepository,IAppLogger<GetLeaveTypeQueryHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

    

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAsync();
            _logger.LogInformation("Leave types were retrived");
            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }
    }
}
