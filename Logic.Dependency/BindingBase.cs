using System;

namespace Logic.Dependency
{
    public abstract record BindingBase(Type ServiceType, Lifetime Lifetime);
}
