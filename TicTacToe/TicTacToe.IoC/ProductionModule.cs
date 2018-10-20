namespace TicTacToe.IoC
{
    using System.Configuration;

    using TicTacToe.Core.Logger;
    using TicTacToe.Data;
    using TicTacToe.Data.Context;
    using TicTacToe.Domain.Interfaces;
    using TicTacToe.Infrastructure.Logger;
    using TicTacToe.PostgreSqlData.Context;
    using TicTacToe.PostgreSqlData.Repositories;
    using TicTacToe.Services.Services;

    using Unity;
    using Unity.AspNet.Mvc;
    using Unity.Injection;

    public class ProductionModule
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            BindInfrastructure(container);
            BindRepositories(container);
            BindServices(container);
        }

        private static void BindRepositories(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>()
                     .RegisterType<IRoleRepository, RoleRepository>()
                     .RegisterType<ITicTacToeGameRepository, TicTacToeGameRepository>();
        }


        private static void BindServices(IUnityContainer container)
        {
            container.RegisterType<IUserService, UserService>()
                .RegisterType<ISearchTableService, SearchTableService>()
                .RegisterType<IMapperResolverService, MapperResolverService>()
                .RegisterType<IRoleService, RoleService>()
                .RegisterType<ITicTacToeGameService, TicTacToeGameService>()
                .RegisterType<ITicTacToeCommonServices, TicTacToeCommonServices>()
                .RegisterType<ITicTacToeAIService, TicTacToeAIService>();
        }

        private static void BindInfrastructure(IUnityContainer container)
        {
            container.RegisterType<ILogger, NLogger>()
                .RegisterType<IContext, Context>(
                    new PerRequestLifetimeManager(),
                    new InjectionConstructor(
                        ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                .RegisterType<ITransaction, Transaction>(new PerRequestLifetimeManager())
                .RegisterType<IContextManager, ContextManager>(new PerRequestLifetimeManager());
        }
    }
}
