using AgariTakuServer.Services;
using Logic.Schema.Types;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AgariTakuServer.Pages
{
    public partial class Lobby
    {
        [Inject]
        private LobbyStatusService LobbyStatusService { get; init; }

        private string StatusText(LobbyConnectionStatus status)
        {
            return status.ReadySince.HasValue ? "Waiting for game to start..." : "You are not ready yet.";
        }

        private string ButtonText(LobbyConnectionStatus status)
        {
            return status.ReadySince.HasValue ? "Cancel" : "Ready!";
        }

        private async Task ToggleReady()
        {
            await LobbyStatusService.ToggleReady();
        }

        protected override async Task OnInitializedAsync()
        {
            LobbyStatusService.OnChange += ChangeHandler;
            await base.OnInitializedAsync();
        }

        public override void Dispose()
        {
            LobbyStatusService.OnChange -= ChangeHandler;

            LobbyStatusService.Disconnect().Wait();
        }

        private void ChangeHandler()
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
