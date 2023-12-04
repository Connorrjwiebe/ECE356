namespace Supermarket.API.Domain.Models
{
	public class Sensor
	{
		public DateTime Time { get; set; }
		public float Ph { get; set; } 
		public float Conductivity { get; set; } 
		public float Temperature { get; set; } 
	}
}