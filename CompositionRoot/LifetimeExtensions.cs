using Logic.Dependency;
using SimpleInjector;

namespace CompositionRoot
{
    public static class LifetimeExtensions
    {
        public static Lifestyle ToLifestyle(this Lifetime lifetime) => lifetime switch
        {
            Lifetime.Transient => Lifestyle.Transient,
            Lifetime.Scoped => Lifestyle.Scoped,
            Lifetime.Singleton => Lifestyle.Singleton,
            _ => throw new System.NotImplementedException(),
        };
    }
}
