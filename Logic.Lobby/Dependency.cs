using Logic.Dependency;
using Logic.Lobby.Components;
using System.Collections.Generic;

namespace Logic.Lobby
{
    public static class Dependency
    {
        public static IReadOnlyCollection<BindingBase> Bindings { get; } = new List<BindingBase>
        {
            new SimpleBinding(typeof(ILobbyConnectionManager), typeof(LobbyConnectionManager), Lifetime.Scoped),
            new SimpleBinding(typeof(ILobbyConnectionCollection), typeof(LobbyConnectionCollection), Lifetime.Singleton),
        };
    }
}
