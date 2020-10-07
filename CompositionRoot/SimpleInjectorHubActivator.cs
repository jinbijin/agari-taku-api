using Microsoft.AspNetCore.SignalR;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CompositionRoot
{
    public sealed class SimpleInjectorHubActivator<T> : IHubActivator<T> where T : Hub
    {
        private readonly Container _container;
        private Scope _scope;

        public SimpleInjectorHubActivator(Container container) => _container = container;

        public T Create()
        {
            _scope = AsyncScopedLifestyle.BeginScope(_container);
            return _container.GetInstance<T>();
        }

        public void Release(T hub) => _scope.Dispose();
    }
}
