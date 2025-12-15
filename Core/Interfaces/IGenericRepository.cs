using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpecAsync(ISPecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISPecification<T> spec);
    Task<TResult?> GetEntityWithSpecAsync<TResult>(ISPecification<T, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISPecification<T, TResult> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveAllAsync();
    bool Exists(int id);
    Task<int> CountAsync(ISPecification<T> spec);
}