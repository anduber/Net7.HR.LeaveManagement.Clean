using System;
using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository:GenericRepository<LeaveType>,ILeaveTypeRepository
	{
		public LeaveTypeRepository(HrDatabaseContext context):base(context)
		{
		}

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            var hasLeaveType = await _context.LeaveTypes.AnyAsync(t => t.Name == name);
            return !hasLeaveType;
        }
    }


}

