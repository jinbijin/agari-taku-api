using Microsoft.AspNetCore.SignalR;
using Logic.Schema.Types.Clients;

namespace Logic.Lobby.Types
{
    public interface ILobbyClientContext
    {
        IHubClients<ILobbyClient> Clients { get; }
        IGroupManager Groups { get; }
    }
}
