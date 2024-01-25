using AutoMapper;
using PryanikyWebApp.Data;
using PryanikyWebApp.Dto;

namespace PryanikyWebApp.Helpers
{
    public class MapperInitializer
    {
        private static readonly object _locker = new object();
        public static void Initialize()
        {
            lock (_locker)
            {
                Mapper.Reset();
                Mapper.Initialize(cfg => {
                    cfg.CreateMap<Order, OrderDto>();
                    cfg.CreateMap<Product, ProductDto>();
                });
            }
        }
    }
}
