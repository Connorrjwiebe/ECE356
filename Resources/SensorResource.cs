namespace Supermarket.API.Resources
{
    public record SensorResource
    {
        public DateTime Time { get; set; }
		public float Ph { get; set; } 
		public float Conductivity { get; set; } 
		public float Temperature { get; set; } 
    }
}