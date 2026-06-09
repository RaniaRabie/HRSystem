namespace HRSystem.DAL.Repository.Abstraction
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}
