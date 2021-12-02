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
            //mce.ShouldMapProperty = p => p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsPrivate || p.GetMethod.IsAssembly);

            var mc = new MapperConfiguration(mce);
            mc.AssertConfigurationIsValid();
 
            
            //     cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            return new Mapper(mc);
        }
    }
}