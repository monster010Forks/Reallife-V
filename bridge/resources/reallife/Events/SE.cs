using GTANetworkAPI;
using reallife.Player;
using reallife.Db;

namespace reallife.Events
{
    class SE : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client client, Vehicle vehicle, sbyte seatID)
        {
            int client_id = client.GetData("ID");
            Vehicle personal_vehicle = client.GetData("PersonalVehicle");

            if (client.HasData("PersonalVehicle"))
            {
                if (Database.GetData<PlayerVehicles>("_id", client_id) == null)
                {
                    if (client.VehicleSeat != (int)VehicleSeat.Driver)
                    {
                        client.SendNotification("~r~Du hast kein schlüssel für dieses Fahrzeug!");

                    }
                }
                return;
            }
        }
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client client, DisconnectionType type, string reason)
        {
            Player.DisconnectHandler.DisconnectFinish(client);
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Client player, Client killer, uint reason)
        {
            NAPI.Task.Run(() =>
            {
                if (!killer.IsNull)
                {
                    NAPI.Player.SpawnPlayer(player, new Vector3(335.866, -597.0053, 28.77587));
                    NAPI.Chat.SendChatMessageToPlayer(player, $"Du wurdest von {killer.Name} getötet!");
                }
                else
                {
                    NAPI.Player.SpawnPlayer(player, new Vector3(335.866, -597.0053, 28.77587));
                    NAPI.Chat.SendChatMessageToPlayer(player, $"Du bist gestorben!");
                }
            }, delayTime: 10000);
        }
    }
}
