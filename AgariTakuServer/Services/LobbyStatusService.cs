using Logic.Schema.Types;
using Logic.Schema.Types.Clients;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgariTakuServer.Services
{
    public class LobbyStatusService : LobbyClientBase
    {
        private Dictionary<Guid, LobbyUser> _users = new();
        private LobbyConnectionStatus? _status;
        private readonly object _writeLock = new();

        public LobbyStatusService(IConfiguration config) : base(config)
        {
        }

        public IReadOnlyCollection<LobbyUser> Users =>
            _users.Values
                .Where(user => !user.DisconnectedSince.HasValue || user.DisconnectedSince.Value >= DateTime.Now.AddSeconds(-30))
                .OrderBy(user => user.ReadySince ?? DateTime.MaxValue)
                .ThenBy(user => user.DisconnectedSince.HasValue)
                .ThenBy(user => user.UserName)
                .ToList();

        public LobbyConnectionStatus? Status => _status;

        public async Task Connect(string lobbyId, string userName)
        {
            await SetupConnection();

            await Identify(lobbyId, null, userName);
        }

        public async Task Disconnect()
        {
            await CleanupConnection();

            _users = new();
            _status = null;
        }

        public async Task ToggleReady()
        {
            bool isReady = Status?.ReadySince.HasValue ?? false;
            await SetReady(!isReady);
        }

        protected override void UpdateConnectionStatus(LobbyConnectionStatus status)
        {
            _status = status;
            NotifyStateChanged();
        }

        protected override void ReplaceUsers(IReadOnlyCollection<LobbyUser> users)
        {
            Merge(users);
            NotifyStateChanged();
        }

        protected override void UpdateUser(LobbyUser user)
        {
            Merge(user);
            NotifyStateChanged();
        }

        private void Merge(IReadOnlyCollection<LobbyUser> users)
        {
            lock (_writeLock)
            {
                if (_users == null)
                {
                    _users = users.ToDictionary(user => user.Id);
                }
                else
                {
                    foreach (LobbyUser user in users)
                    {
                        MergeUser(user);
                    }
                }
            }
        }

        private void Merge(LobbyUser user)
        {
            lock (_writeLock)
            {
                MergeUser(user);
            }
        }

        private void MergeUser(LobbyUser user)
        {
            if (_users.ContainsKey(user.Id) && _users[user.Id].Version <= user.Version)
            {
                _users[user.Id] = user;
            }
            else if (!_users.ContainsKey(user.Id))
            {
                _users.Add(user.Id, user);
            }
        }
    }
}
