using Logic.Lobby.Types;
using Logic.Schema.Types.Clients;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs.Clients
{
    public class LobbyClientContext : ILobbyClientContext
    {
        private readonly IHubContext<LobbyHub, ILobbyClient> _context;

        public LobbyClientContext(IHubContext<LobbyHub, ILobbyClient> context)
        {
            _context = context;
        }

        public IHubClients<ILobbyClient> Clients => _context.Clients;

        public IGroupManager Groups => _context.Groups;
    }
}
