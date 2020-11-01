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
            KeyValuePair<string, LobbyConnection> existingPair = GetKeyValuePairByLobbyAndUser(initializer.LobbyId, initializer.UserId);
            LobbyConnection connection = new(initializer);
            lock (_writeLock)
            {
                if (existingPair.Key != null)
                {
                    _connections.Remove(existingPair.Key);
                    existingPair.Value.IsConnected = true;
                    _connections.Add(connectionId, existingPair.Value);
                    return existingPair.Value;
                }

                _connections.Add(connectionId, connection);
                return connection;
            }
        }

        public LobbyConnection? GetConnection(string connectionId)
        {
            _connections.TryGetValue(connectionId, out LobbyConnection? maybeConnection);
            return maybeConnection;
        }

        private KeyValuePair<string, LobbyConnection> GetKeyValuePairByLobbyAndUser(string lobbyId, Guid userId)
        {
            return _connections.SingleOrDefault(pair => pair.Value.LobbyId == lobbyId && pair.Value.UserId == userId);
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
