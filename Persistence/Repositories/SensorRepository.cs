using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class SensorRepository : BaseRepository, ISensorRepository
    {
        public SensorRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Sensor>> ListAsync()
        {
            return await _context.test356
                                 .AsNoTracking()
                                 .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(Sensor sensor)
        {
            await _context.test356.AddAsync(sensor);
        }

        public async Task<Sensor?> FindByIdAsync(int id)
        {
            return await _context.test356.FindAsync(id);
        }

        public void Update(Sensor sensor)
        {
            _context.test356.Update(sensor);
        }

        public void Remove(Sensor sensor)
        {
            _context.test356.Remove(sensor);
        }
    }
}