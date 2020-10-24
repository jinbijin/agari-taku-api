using Logic.Lobby.Types;
using Logic.Schema.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Lobby.Components
{
    internal class LobbyConnectionCollection : ILobbyConnectionCollection
    {
        private readonly Dictionary<string, LobbyConnection> _connections = new();
        private readonly object _writeLock = new();

        public LobbyConnection AddConnection(string connectionId, LobbyConnectionInitializer initializer)
        {
            LobbyConnection connection = new(initializer);
            lock (_writeLock)
            {
                _connections.Add(connectionId, connection);
            }
            return connection;
        }

        public LobbyConnection? GetConnection(string connectionId)
        {
            _connections.TryGetValue(connectionId, out LobbyConnection? maybeConnection);
            return maybeConnection;
        }

        public LobbyConnection? GetConnectionByLobbyAndUser(string lobbyId, Guid userId)
        {
            return _connections.Values.SingleOrDefault(connection => connection.LobbyId == lobbyId && connection.UserId == userId);
        }

        public IReadOnlyCollection<LobbyUser> GetConnections(string lobbyId)
        {
            return _connections.Values
                .Where(connection => connection.LobbyId == lobbyId)
                .Select(connection => connection.ToConnectionStatus())
                .ToList();
        }

        public void RemoveConnection(string connectionId)
        {
            lock (_writeLock)
            {
                _connections.Remove(connectionId);
            }
        }

        public void SetReady(LobbyConnection connection, bool isReady)
        {
            lock (_writeLock)
            {
                connection.IsReady = isReady;
            }
        }
    }
}
