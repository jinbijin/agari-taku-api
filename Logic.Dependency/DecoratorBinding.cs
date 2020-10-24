using System;

namespace Logic.Dependency
{
    public record DecoratorBinding(Type ServiceType, Type ImplementationType, Lifetime Lifetime) : BindingBase(ServiceType, Lifetime);
}
