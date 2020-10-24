using AgariTakuServer.Models;
using AgariTakuServer.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AgariTakuServer.Pages
{
    public partial class Index
    {
        [Inject]
        private LobbyStatusService LobbyStatusService { get; init; }

        [Inject]
        private NavigationManager NavigationManager { get; init; }

        private LobbyUserModel model = new();

        private async Task HandleValidSubmit()
        {
            await LobbyStatusService.Connect(model.LobbyId, model.UserName);
            NavigationManager.NavigateTo("/lobby");
        }
    }
}
