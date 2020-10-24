using System;

namespace Logic.Dependency
{
    public record SimpleBinding(Type ServiceType, Type ImplementationType, Lifetime Lifetime) : BindingBase(ServiceType, Lifetime);
}
