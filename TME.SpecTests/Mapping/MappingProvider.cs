using AutoMapper;
using AutoMapper.Configuration;
using Castle.DynamicProxy.Internal;

namespace TME.SpecTests.Mapping
{
    public static class MapperProvider
    {
        public static IMapper GetMapper(params MapperConfigurationExpression[] configs)
        {
            var mce = new MapperConfigurationExpression();
            mce.AddProfiles(configs);
            mce.Advanced.AllowAdditiveTypeMapCreation = true;
          
            var mc = new MapperConfiguration(mce);
            mc.AssertConfigurationIsValid();
            return new Mapper(mc);
        }
    }
}