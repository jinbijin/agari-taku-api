using Logic.Lobby.Components;
using Logic.Schema.Types.Clients;
using Logic.Schema.Types.Servers;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Web.Hubs
{
    public class LobbyHub : Hub<ILobbyClient>, ILobbyServer
    {
        private readonly ILobbyConnectionManager _connectionManager;

        public LobbyHub(ILobbyConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public async Task Identify(string lobbyId, Guid? maybeUserId, string userName)
        {
            await _connectionManager.SendUsers(Context.ConnectionId, lobbyId);
            await _connectionManager.AddConnection(new(Context.ConnectionId, lobbyId, maybeUserId ?? Guid.NewGuid(), userName));
        }

        public async Task SetReady(bool isReady)
        {
            await _connectionManager.SetReady(Context.ConnectionId, isReady);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await _connectionManager.RemoveConnection(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
