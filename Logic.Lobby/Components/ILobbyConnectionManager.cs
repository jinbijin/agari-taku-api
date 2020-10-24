using Logic.Lobby.Types;
using Logic.Schema.Types;
using System;
using System.Threading.Tasks;

namespace Logic.Lobby.Components
{
    public interface ILobbyConnectionManager
    {
        Task AddConnection(LobbyConnectionInitializer initializer);
        Task SendUsers(string connectionId, string lobbyId);
        Task RemoveConnection(string connectionId);
        Task SetReady(string connectionId, bool isReady);
    }
}
