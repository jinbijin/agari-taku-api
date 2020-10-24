using Logic.Schema.Types;
using System;

namespace Logic.Lobby.Types
{
    public class LobbyConnection // TODO make internal
    {
        private DateTime? _readySince;
        private DateTime? _disconnectedSince;
        private Guid? _gameId;

        internal LobbyConnection(LobbyConnectionInitializer initializer)
        {
            Id = Guid.NewGuid();
            LobbyId = initializer.LobbyId;
            UserId = initializer.UserId;
            UserName = initializer.UserName;
            Version = DateTime.Now;
        }

        public Guid Id { get; internal init; }
        public string LobbyId { get; internal init; }
        public Guid UserId { get; internal init; }
        public string UserName { get; internal init; }
        public DateTime? ReadySince => _readySince;
        public Guid? GameId {
            get => _gameId;
            set
            {
                if (_gameId != value)
                {
                    _gameId = value;
                    Version = DateTime.Now;
                }
            }
        }
        public bool IsReady
        {
            set
            {
                if (value && !_readySince.HasValue)
                {
                    _readySince = DateTime.Now;
                    Version = DateTime.Now;
                }
                else if (!value && _readySince.HasValue)
                {
                    _readySince = null;
                    Version = DateTime.Now;
                }
            }
        }
        public DateTime Version { get; private set; }
        public DateTime? DisconnectedSince => _disconnectedSince;
        public bool IsConnected
        {
            set
            {
                if (value && _disconnectedSince.HasValue)
                {
                    _disconnectedSince = null;
                    Version = DateTime.Now;
                }
                else if (!value && !_disconnectedSince.HasValue)
                {
                    _readySince = null;
                    _disconnectedSince = DateTime.Now;
                    Version = DateTime.Now;
                }
            }
        }

        public LobbyConnectionStatus ToConnectionStatus() => new()
        {
            Id = Id,
            LobbyId = LobbyId,
            UserId = UserId,
            UserName = UserName,
            ReadySince = ReadySince,
            GameId = GameId,
            Version = Version,
        };

        public LobbyUser ToLobbyUser() => new()
        {
            Id = Id,
            UserName = UserName,
            ReadySince = ReadySince,
            GameId = GameId,
            Version = Version,
            DisconnectedSince = DisconnectedSince,
        };
    }
}
