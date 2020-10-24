using System;

namespace Logic.Schema.Types
{
    public class LobbyUser
    {
        public Guid Id { get; init; }
        public string UserName { get; init; } = string.Empty;
        public DateTime? ReadySince { get; init; }
        public Guid? GameId { get; init; }
        public DateTime Version { get; init; }
        public DateTime? DisconnectedSince { get; init; }
    }
}
