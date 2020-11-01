using AgariTakuServer.Services;
using Microsoft.AspNetCore.Components;

namespace AgariTakuServer.Pages
{
    [Route("/")]
    public partial class Index
    {
        [Inject]
        private LobbyStatusService LobbyStatusService { get; init; }
    }
}
