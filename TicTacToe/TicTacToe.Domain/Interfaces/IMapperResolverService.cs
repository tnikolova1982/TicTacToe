namespace TicTacToe.Domain.Interfaces
{
    using TicTacToe.Infrastructure.MapperConfiguration;

    public interface IMapperResolverService
    {
        TDestination Resolver<TSource, TDestination>(TSource model, IHaveCustomMappings customMappings = null);
    }
}
