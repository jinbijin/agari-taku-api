using Logic.Dependency;
using Logic.Lobby.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Web.Hubs.Clients;

namespace CompositionRoot
{
    public static class ContainerConfig
    {
        public static void ConfigureContainer(this IServiceCollection services)
        {
            services.AddScoped<ILobbyClientContext, LobbyClientContext>();
            RegisterBindings(services, Logic.Lobby.Dependency.Bindings);
        }

        private static void RegisterBindings(IServiceCollection services, IEnumerable<BindingBase> bindings)
        {
            foreach (BindingBase binding in bindings)
            {
                switch (binding)
                {
                    case SimpleBinding simpleBinding:
                        RegisterSimpleBinding(services, simpleBinding);
                        break;
                }
            }
        }

        private static void RegisterSimpleBinding(IServiceCollection services, SimpleBinding simpleBinding)
        {
            switch (simpleBinding.Lifetime)
            {
                case Lifetime.Singleton:
                    services.AddSingleton(simpleBinding.ServiceType, simpleBinding.ImplementationType);
                    break;
                case Lifetime.Scoped:
                    services.AddScoped(simpleBinding.ServiceType, simpleBinding.ImplementationType);
                    break;
                case Lifetime.Transient:
                    services.AddTransient(simpleBinding.ServiceType, simpleBinding.ImplementationType);
                    break;
            }
        }
    }
}
