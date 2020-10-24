using Microsoft.AspNetCore.Components;
using System;

namespace AgariTakuServer.Components
{
    public abstract class DisposableComponentBase : ComponentBase, IDisposable
    {
        public abstract void Dispose();
    }
}
