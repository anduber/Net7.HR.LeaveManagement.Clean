using System;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
	public class CreateLeaveAllocationCommandValidator:AbstractValidator<CreateLeaveAllocationCommand>
	{
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
		{
            this._leaveTypeRepository = leaveTypeRepository;

            RuleFor(r => r.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTpeMustExist)
                .WithMessage("{PropertyName} does not exist.");
            ;

        }

        private async Task<bool> LeaveTpeMustExist(int id, CancellationToken arg2)
        {
            return await _leaveTypeRepository.GetByIdAsync(id) != null;
        }
    }
}

