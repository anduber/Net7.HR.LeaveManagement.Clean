﻿using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts
{
    public interface ILeaveRequestRepository:IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetails(int id);
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
    }
}