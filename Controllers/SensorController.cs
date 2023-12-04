using System.Web.Http.Cors;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Resources;

namespace Supermarket.API.Controllers
{
	[Route("/api/[controller]")]
	public class SensorsController : BaseApiController
	{
		private readonly ISensorService _sensorService;
		private readonly IMapper _mapper;

		public SensorsController(ISensorService sensorService, IMapper mapper)
		{
			_sensorService = sensorService;
			_mapper = mapper;
		}

		/// <summary>
		/// Lists all categories.
		/// </summary>
		/// <returns>List os categories.</returns>
		//[EnableCors]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<SensorResource>), 200)]
		public async Task<IEnumerable<SensorResource>> ListAsync()
		{
			var categories = await _sensorService.ListAsync();
			return _mapper.Map<IEnumerable<SensorResource>>(categories);
		}

		/// <summary>
		/// Saves a new sensor.
		/// </summary>
		/// <param name="resource">Sensor data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(SensorResource), 201)]
		[ProducesResponseType(typeof(ErrorResource), 400)]
		public async Task<IActionResult> PostAsync([FromBody] SaveSensorResource resource)
		{
			var sensor = _mapper.Map<Sensor>(resource);
			var result = await _sensorService.SaveAsync(sensor);

			if (!result.Success)
			{
				return BadRequest(new ErrorResource(result.Message!));
			}

			var sensorResource = _mapper.Map<SensorResource>(result.Resource!);
			return Ok(sensorResource);
		}

		/// <summary>
		/// Updates an existing sensor according to an identifier.
		/// </summary>
		/// <param name="id">Sensor identifier.</param>
		/// <param name="resource">Updated sensor data.</param>
		/// <returns>Response for the request.</returns>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(SensorResource), 200)]
		[ProducesResponseType(typeof(ErrorResource), 400)]
		public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSensorResource resource)
		{
			var sensor = _mapper.Map<Sensor>(resource);
			var result = await _sensorService.UpdateAsync(id, sensor);

			if (!result.Success)
			{
				return BadRequest(new ErrorResource(result.Message!));
			}

			var sensorResource = _mapper.Map<SensorResource>(result.Resource!);
			return Ok(sensorResource);
		}

		/// <summary>
		/// Deletes a given sensor according to an identifier.
		/// </summary>
		/// <param name="id">Sensor identifier.</param>
		/// <returns>Response for the request.</returns>
		
	}
}