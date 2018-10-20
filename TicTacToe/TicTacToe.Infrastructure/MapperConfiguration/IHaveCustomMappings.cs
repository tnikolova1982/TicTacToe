namespace TicTacToe.Infrastructure.MapperConfiguration
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings<TSource, TDestination>(IMapperConfigurationExpression configuration);
    }
}
