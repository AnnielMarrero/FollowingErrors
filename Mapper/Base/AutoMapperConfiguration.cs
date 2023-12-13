using AutoMapper;

namespace FollowingErrors.Mapper.Base
{
    public static class AutoMapperConfiguration
    {

        public static void AddAutoMappers(
            this IServiceCollection services,
            MapperConfigurationExpression cfg
        )
        {
            IMapper mapper = new MapperConfiguration(cfg).CreateMapper();
            services.AddSingleton(mapper);
        }

        public static MapperConfigurationExpression AddAutoMapper(
            this MapperConfigurationExpression cfg
        )
        {
            cfg.AddProfile<BugMapperProfile>();
            return cfg;
        }

        public static MapperConfigurationExpression CreateExpression()
        {
            return new MapperConfigurationExpression();
        }
    }

}
