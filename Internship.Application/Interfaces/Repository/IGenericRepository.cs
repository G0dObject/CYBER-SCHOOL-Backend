namespace Internship.Application.Interfaces.Repository
{
	public interface IGenericRepository<T>
	{
		Task CreateAsync(T entity);
		Task UpdateAsync(T entity);
		Task Delete(int id);
		Task<T?> GetByIdAsync(int id);
		Task<T?> FirstAsync();
		Task<T?> LastAsync();
		Task<List<T>> GetAllAsync();
	}
}
