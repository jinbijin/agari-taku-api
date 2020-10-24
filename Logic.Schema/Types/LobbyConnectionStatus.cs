using System;

namespace Logic.Schema.Types
{
    public class LobbyConnectionStatus : LobbyUser
    {
        public string LobbyId { get; init; } = string.Empty;
        public Guid UserId { get; init; }
    }
}
