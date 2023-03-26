using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain
{
    public class LeaveType:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultDays { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
    