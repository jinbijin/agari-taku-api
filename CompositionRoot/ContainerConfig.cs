using Logic.Dependency;
using Logic.Lobby.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using Web.Hubs;
using Web.Hubs.Clients;

namespace CompositionRoot
{
    public static class ContainerConfig
    {
        public static Container _container = new();

        public static void ConfigureContainer(this IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore();
            });

            RegisterHubs(services);
            RegisterBindings(Logic.Lobby.Dependency.Bindings);
        }

        public static void ConfigureContainer(this IApplicationBuilder app)
        {
            app.UseSimpleInjector(_container);
        }

        private static void RegisterHubs(IServiceCollection services)
        {
            IEnumerable<Type> hubTypes = _container.GetTypesToRegister<Hub>(typeof(LobbyHub).Assembly);
            foreach (Type hubType in hubTypes)
            {
                services.AddScoped(typeof(IHubActivator<>).MakeGenericType(hubType), typeof(SimpleInjectorHubActivator<>).MakeGenericType(hubType));
                _container.Register(hubType, hubType, Lifestyle.Scoped);
            }
            _container.Register<ILobbyClientContext, LobbyClientContext>(Lifestyle.Scoped);
        }

        private static void RegisterBindings(IEnumerable<BindingBase> bindings)
        {
            foreach (BindingBase binding in bindings)
            {
                switch (binding)
                {
                    case SimpleBinding simpleBinding:
                        _container.Register(simpleBinding.ServiceType, simpleBinding.ImplementationType, simpleBinding.Lifetime.ToLifestyle());
                        break;
                    case DecoratorBinding decoratorBinding:
                        _container.RegisterDecorator(decoratorBinding.ServiceType, decoratorBinding.ImplementationType, decoratorBinding.Lifetime.ToLifestyle());
                        break;
                }
            }
        }
    }
}
