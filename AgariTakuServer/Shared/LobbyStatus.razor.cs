using AgariTakuServer.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgariTakuServer.Shared
{
    public partial class LobbyStatus
    {
        private bool collapseLobbyStatus = true;

        private string? LobbyStatusCssClass => collapseLobbyStatus ? "block" : "hidden md:block";

        [Inject]
        private NavigationManager NavigationManager { get; init; }

        [Inject]
        private LobbyStatusService LobbyStatusService { get; init; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object>? InputAttributes { get; init; }

        private void NavigateHome()
        {
            NavigationManager.NavigateTo("/", true);
        }

        private void ToggleLobbyStatus()
        {
            collapseLobbyStatus = !collapseLobbyStatus;
        }

        protected override async Task OnInitializedAsync()
        {
            LobbyStatusService.OnChange += ChangeHandler;
            await base.OnInitializedAsync();
        }

        public override void Dispose()
        {
            LobbyStatusService.OnChange -= ChangeHandler;
        }

        private void ChangeHandler()
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
