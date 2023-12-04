using Microsoft.Extensions.Caching.Memory;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Services
{
	public class SensorService : ISensorService
	{
		private readonly ISensorRepository _sensorRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMemoryCache _cache;
		private readonly ILogger<SensorService> _logger;

		public SensorService
		(
			ISensorRepository sensorRepository,
			IUnitOfWork unitOfWork,
			IMemoryCache cache,
			ILogger<SensorService> logger
		)
		{
			_sensorRepository = sensorRepository;
			_unitOfWork = unitOfWork;
			_cache = cache;
			_logger = logger;
		}

		public async Task<IEnumerable<Sensor>> ListAsync()
		{
			// Here I try to get the sensors list from the memory cache. If there is no data in cache, the anonymous method will be
			// called, setting the cache to expire one minute ahead and returning the Task that lists the sensors from the repository.
			var sensors = await _cache.GetOrCreateAsync(CacheKeys.SensorsList, (entry) =>
			{
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
				return _sensorRepository.ListAsync();
			});

			return sensors ?? new List<Sensor>();
		}

		public async Task<Response<Sensor>> SaveAsync(Sensor sensor)
		{
			try
			{
				await _sensorRepository.AddAsync(sensor);
				await _unitOfWork.CompleteAsync();

				return new Response<Sensor>(sensor);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Could not save sensor.");
				return new Response<Sensor>($"An error occurred when saving the sensor: {ex.Message}");
			}
		}

		public async Task<Response<Sensor>> UpdateAsync(int id, Sensor sensor)
		{
			var existingSensor = await _sensorRepository.FindByIdAsync(id);
			if (existingSensor == null)
			{
				return new Response<Sensor>("sensor not found.");
			}

			existingSensor.Time = sensor.Time;

			try
			{
				await _unitOfWork.CompleteAsync();
				return new Response<Sensor>(existingSensor);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Could not update Sensor with ID {id}.", id);
				return new Response<Sensor>($"An error occurred when updating the Sensor: {ex.Message}");
			}
		}

		
	}
}
