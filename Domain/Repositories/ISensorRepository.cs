using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Repositories
{
	public interface ISensorRepository
{
	Task<IEnumerable<Sensor>> ListAsync();
	Task AddAsync(Sensor sensor);
	Task<Sensor> FindByIdAsync(int id);
	void Update(Sensor sensor);
}
}