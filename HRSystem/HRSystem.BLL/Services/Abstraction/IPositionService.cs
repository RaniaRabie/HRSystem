using HRSystem.BLL.ModelVM.Position;

namespace HRSystem.BLL.Services.Abstraction
{
    public interface IPositionService
    {
        // Get all Positions
        Task<Response<IEnumerable<PositionVM>>> GetAllPositionsAsync();

        // Get a position by ID
        Task<Response<PositionVM>> GetPositionByIdAsync(Guid id);


        // Add a new department
        Task<Response<bool>> AddPositionAsync(PositionFormVM positionVM);

        // Update an existing department
        Task<Response<bool>> UpdatePositionAsync(PositionFormVM positionVM);

        // Delete an existing department
        Task<Response<bool>> DeletePositionAsync(Guid id);
    }
}
