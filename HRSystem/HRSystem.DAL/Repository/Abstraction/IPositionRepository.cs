namespace HRSystem.DAL.Repository.Abstraction
{
    public interface IPositionRepository: IGenericRepository<Position>
    {
        public Task<IEnumerable<Position>> GetPositionssWithDepartmentNamesAndNumOfEmployees();
        public Task<Position?> GetPositionByIdWithItsRelatedDepartment(Guid id);
    }
}
