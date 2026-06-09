using HRSystem.BLL.ModelVM.LeaveType;
namespace HRSystem.BLL.Services.Abstraction
{
    public interface ILeaveTypeService
    {
        Task<Response<IEnumerable<LeaveTypeVM>>> GetAllAsync();
        Task<Response<LeaveTypeFormVM>> GetForEditAsync(Guid id);
        Task<Response<bool>> CreateAsync(LeaveTypeFormVM vm);
        Task<Response<bool>> UpdateAsync(LeaveTypeFormVM vm);
        Task<Response<bool>> DeleteAsync(Guid id);
    }
}
