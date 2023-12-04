using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services.Communication;

namespace Supermarket.API.Domain.Services
{
    public interface ISensorService
    {
         Task<IEnumerable<Sensor>> ListAsync();
         Task<Response<Sensor>> SaveAsync(Sensor sensor);
         Task<Response<Sensor>> UpdateAsync(int id, Sensor sensor);
         
        //Task SaveAsync(Sensor sensor);

    }
}