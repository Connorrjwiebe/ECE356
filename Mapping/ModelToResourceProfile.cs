using AutoMapper;
using Supermarket.API.Extensions;
using Supermarket.API.Resources;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Sensor, SensorResource>();

     
        }
    }
}