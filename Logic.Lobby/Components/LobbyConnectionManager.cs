using Logic.Lobby.Types;
using Logic.Schema.Types;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Lobby.Components
{
    internal class LobbyConnectionManager : ILobbyConnectionManager
    {
        private readonly ILobbyClientContext _context;
        private readonly ILobbyConnectionCollection _connections;

        public LobbyConnectionManager(ILobbyClientContext context, ILobbyConnectionCollection connections)
        {
            _context = context;
            _connections = connections;
        }

        public async Task AddConnection(LobbyConnectionInitializer initializer)
        {
            LobbyConnection connection = _connections.AddConnection(initializer.ConnectionId, initializer);

            await _context.Groups.AddToGroupAsync(initializer.ConnectionId, connection.LobbyId);
            await _context.Clients.Group(connection.LobbyId).UpdateUser(connection.ToLobbyUser());
            await _context.Clients.Client(initializer.ConnectionId).UpdateConnectionStatus(connection.ToConnectionStatus());
        }

        public async Task SendUsers(string connectionId, string lobbyId)
        {
            IReadOnlyCollection<LobbyUser> users = _connections.GetConnections(lobbyId);
            await _context.Clients.Client(connectionId).ReplaceUsers(users);
        }

        public async Task RemoveConnection(string connectionId)
        {
            LobbyConnection? maybeConnection = _connections.GetConnection(connectionId);
            if (maybeConnection is LobbyConnection connection)
            {
                connection.IsConnected = false;
                _connections.RemoveConnection(connectionId);

                await _context.Clients.Group(connection.LobbyId).UpdateUser(connection.ToLobbyUser());
                await _context.Groups.RemoveFromGroupAsync(connectionId, connection.LobbyId);
            }
        }

        public async Task SetReady(string connectionId, bool isReady)
        {
            LobbyConnection? maybeConnection = _connections.GetConnection(connectionId);
            if (maybeConnection is LobbyConnection connection)
            {
                _connections.SetReady(connection, isReady);

                await _context.Clients.Group(connection.LobbyId).UpdateUser(connection.ToLobbyUser());
                await _context.Clients.Client(connectionId).UpdateConnectionStatus(connection.ToConnectionStatus());
            }
        }
    }
}
