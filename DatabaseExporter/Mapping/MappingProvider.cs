using AutoMapper;
using AutoMapper.Configuration;

namespace DatabaseExporter.Mapping
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