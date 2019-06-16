using GTANetworkAPI;
using reallife.Data;
using reallife.Db;
using reallife.Events;

namespace reallife.Player
{
    public static class LoginHandler
    {
        public static void FinishLogin(Client client)
        {

            int client_id = client.GetData("ID");

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);

            //Database.Update(playerInfo);
            playerInfo.Update();

            EventTriggers.Update_Money(client);
            EventTriggers.Update_Bank(client);
            EventTriggers.Update_Wanteds(client);

            NAPI.Player.SetPlayerName(client, playerInfo.vorname + "" + playerInfo.nachname);

            if (playerInfo.wantedlevel >= 1)
            {
                NAPI.Player.SetPlayerNametagColor(client, 249, 27, 27);
            }

            if(playerInfo.temp_location == null)
            {
                client.Position = playerInfo.GetLastPlayerLocation();
            } else
            {
                client.Position = playerInfo.GetLastTempLocation();
                client.SendChatMessage("~r~Du sitzt noch " + playerInfo.jailtime);
                client.Freeze(true);
            }

            if(playerInfo.cuff == 1)
            {
                client.SendChatMessage("~r~Du hast Offlineflucht begangen und sitzt nun 10 Minuten länger im Gefängnis!");
                playerInfo.cuff = 0;
                playerInfo.Update();
            }

            PlayerData.ResetClothes(client);

            PlayerVehicles.GetLastCarPosition(client);
            //client.SendNotification("~g~Erfolgreich eingeloggt!");
            
            NAPI.ClientEvent.TriggerClientEvent(client, "LoginUnFreeze");
            NAPI.ClientEvent.TriggerClientEvent(client, "CameraDestroy");
            return;
        }
    }
}
