using Logic.Schema.Types;
using Microsoft.AspNetCore.Components;
using System;

namespace AgariTakuServer.Shared
{
    public partial class LobbyUserItem
    {
        private string LobbyUserCssClass
        {
            get
            {
                if (CurrentUserId == User.Id)
                {
                    return "pl-1 bg-accent border-l-4 border-accent-light";
                }
                else if (User.DisconnectedSince.HasValue)
                {
                    return "bg-primary text-gray-500";
                }
                else
                {
                    return "bg-primary";
                }
            }
        }

        [Parameter]
        public int Index { get; init; }

        [Parameter]
        public LobbyUser User { get; init; }

        [Parameter]
        public Guid CurrentUserId { get; init; }
    }
}
