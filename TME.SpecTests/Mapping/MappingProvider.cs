using AutoMapper;
using Castle.DynamicProxy.Internal;
using Microsoft.Extensions.Logging.Abstractions;

namespace TME.SpecTests.Mapping
{
    public static class MapperProvider
    {
        public static IMapper GetMapper(params Profile[] configs)
        {
            var mc = new MapperConfiguration(cfg => cfg.AddProfiles(configs), NullLoggerFactory.Instance);
            mc.AssertConfigurationIsValid();
            return new Mapper(mc);
        }
    }
}