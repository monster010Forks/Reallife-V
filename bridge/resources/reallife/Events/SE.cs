using GTANetworkAPI;
using reallife.Player;
using reallife.Db;
using reallife.Data;

namespace reallife.Events
{
    class SE : Script
    {
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client client, DisconnectionType type, string reason)
        {
            Player.Handler.DisconnectFinish(client);
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Client player, Client killer, uint reason)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (killer == null)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, $"Du bist gestorben!");
            }
            else
            {
                NAPI.Chat.SendChatMessageToPlayer(player, $"Du wurdest von {killer.Name} getötet!");
            }

            NAPI.ClientEvent.TriggerClientEvent(player, "DeathTrue");

            player.SetData("dead", true);

            NAPI.Task.Run(() =>
            {
                if (player.HasData("dead"))
                {
                    NAPI.Player.SpawnPlayer(player, pInfo.GetLastPlayerLocation());
                    player.SendNotification("Du wurdest respawnt!");
                    NAPI.ClientEvent.TriggerClientEvent(player, "DeathFalse");
                    player.ResetData("dead");
                }
            }, delayTime: 120000);
        }
    }
}
