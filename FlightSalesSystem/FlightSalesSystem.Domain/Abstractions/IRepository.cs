namespace FlightSalesSystem.Domain.Abstractions;
public interface IRepository<T> where T : AggregateRoot
{
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void SaveChanges();
    Task SaveChangesAsync();
}
