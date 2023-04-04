using AutoMapper;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeQueryHandlerTests
	{
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveTypeQueryHandler>> _mockAppLogger;

        public GetLeaveTypeQueryHandlerTests()
		{
			_mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
			var mapperConfig = new MapperConfiguration(c =>
			{
				c.AddProfile<LeaveTypeProfile>();
			});
			_mapper = mapperConfig.CreateMapper();
			_mockAppLogger = new Mock<IAppLogger<GetLeaveTypeQueryHandler>>();

        }

		[Fact]
		public async Task GetLeaveTypeListTest()
		{
			var handler = new GetLeaveTypeQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
			var result = await handler.Handle(new GetLeaveTypeQuery(), CancellationToken.None);
			result.ShouldBeOfType<List<LeaveTypeDto>>();
			result.Count.ShouldBe(3);
		}
	}
}

