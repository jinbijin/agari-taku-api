using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using Web.Hubs;

namespace CompositionRoot
{
    public static class ContainerConfig
    {
        public static void ConfigureContainer(this IServiceCollection services)
        {
            services.AddSimpleInjector(BuildContainer(), options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();
            });

            services.AddScoped(typeof(IHubActivator<>), typeof(SimpleInjectorHubActivator<>));
        }

        private static Container BuildContainer()
        {
            Container container = new();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            IEnumerable<Type> types = container.GetTypesToRegister<Hub>(typeof(DebugHub).Assembly);
            foreach (Type type in types)
            {
                container.Register(type, type, Lifestyle.Scoped);
            }
            // Enter registrations here...

            return container;
        }
    }
}
