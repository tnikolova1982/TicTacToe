namespace TicTacToe.Services.Services
{
    using AutoMapper;
    using AutoMapper.Configuration;
    using System;
    using System.Collections.Generic;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.MapperConfiguration;

    public class MapperResolverService : IMapperResolverService
    {
        private static Dictionary<Tuple<Type, Type>, MapperConfiguration> configurationCache = new Dictionary<Tuple<Type, Type>, MapperConfiguration>();

        public TDestination Resolver<TSource, TDestination>(TSource model, IHaveCustomMappings customMappings = null)
        {
            var key = new Tuple<Type, Type>(typeof(TSource), typeof(TDestination));

            if (!configurationCache.ContainsKey(key))
            {
                var baseMappings = new MapperConfigurationExpression();

                if (customMappings != null)
                {
                    customMappings.CreateMappings<TSource, TDestination>(baseMappings);
                }
                else
                {
                    baseMappings.CreateMap<TSource, TDestination>();
                }

                var newConfig = new MapperConfiguration(baseMappings);

                configurationCache[key] = newConfig;
            }

            var config = configurationCache[key];

            IMapper mapper = new Mapper(config);

            return mapper.Map<TDestination>(model);
        }
    }
}
