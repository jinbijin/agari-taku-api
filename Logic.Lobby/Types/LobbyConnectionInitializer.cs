using System;

namespace Logic.Lobby.Types
{
    public record LobbyConnectionInitializer(string ConnectionId, string LobbyId, Guid UserId, string UserName);
}
