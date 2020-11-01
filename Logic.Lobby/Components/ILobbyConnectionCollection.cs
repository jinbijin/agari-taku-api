using Logic.Lobby.Types;
using Logic.Schema.Types;
using System;
using System.Collections.Generic;

namespace Logic.Lobby.Components
{
    public interface ILobbyConnectionCollection
    {
        LobbyConnection AddConnection(string connectionId, LobbyConnectionInitializer initializer);
        LobbyConnection? GetConnection(string connectionId);
        IReadOnlyCollection<LobbyUser> GetConnections(string lobbyId);
        void RemoveConnection(string connectionId);
        void SetReady(LobbyConnection connection, bool isReady);
    }
}
