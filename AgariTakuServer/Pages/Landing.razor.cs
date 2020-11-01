using AgariTakuServer.Models;
using AgariTakuServer.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AgariTakuServer.Pages
{
    public partial class Landing
    {
        [Inject]
        private LobbyStatusService LobbyStatusService { get; init; }

        [Parameter]
        public EventCallback OnConnect { get; init; }

        private LobbyUserModel model = new();

        private async Task HandleValidSubmit()
        {
            await LobbyStatusService.Connect(model.LobbyId, model.UserName);
            await OnConnect.InvokeAsync();
        }
    }
}
