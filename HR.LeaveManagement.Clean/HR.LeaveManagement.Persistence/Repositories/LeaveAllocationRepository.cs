using HR.LeaveManagement.Application.Contracts;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext hrDatabaseContext) : base(hrDatabaseContext)
        {

        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.LeaveAllocations.AddRangeAsync(allocations);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(t => t.EmployeeId == userId &&
                                                                   t.LeaveTypeId == leaveTypeId &&
                                                                   t.Period == period);
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return _context.LeaveAllocations.Include(i => i.LeaveType).ToListAsync();
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            return _context.LeaveAllocations.Include(i => i.LeaveType).Where(t => t.EmployeeId == userId).ToListAsync();
        }

        public Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return _context.LeaveAllocations.Include(i => i.LeaveType).FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return _context.LeaveAllocations.FirstOrDefaultAsync(t => t.EmployeeId == userId && t.LeaveTypeId == leaveTypeId);
        }
    }


}

