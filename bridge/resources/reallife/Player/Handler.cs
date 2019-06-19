using GTANetworkAPI;
using reallife.Data;
using reallife.Db;
using reallife.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Player
{
    public static class Handler
    {
        public static void FinishLogin(Client client)
        {

            int client_id = client.GetData("ID");

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            PlayerClothes playerClothes = PlayerHelper.GetPlayerClothes(client);
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

            if (playerInfo.cuff == 1)
            {
                client.SendNotification("~r~Du hast Offlineflucht begangen und sitzt nun 5 Minuten länger im Gefängnis!");
                playerInfo.jailtime += 300000;
                playerInfo.cuff = 0;
                playerInfo.jail = 1;
                playerInfo.Update();
            }

            PlayerVehicles.GetLastCarPosition(client);
            //client.SendNotification("~g~Erfolgreich eingeloggt!");

            NAPI.ClientEvent.TriggerClientEvent(client, "LoginUnFreeze");
            NAPI.ClientEvent.TriggerClientEvent(client, "CameraDestroy");

            PlayerData.Respawn(client);
            return;
        }

        public static void DisconnectFinish(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            Vehicle pVehicle = client.GetData("PersonalVehicle");
            Vehicle RentVehicle = client.GetData("RentVehicle");
            Vehicle FrakVeh = client.GetData("FrakVehicle");

            if (client.HasData("ID") == false)
                return;

            if (client.HasData("RentVehicle"))
            {
                NAPI.Task.Run(() =>
                {
                    RentVehicle.Delete();
                }, delayTime: 1000);
            }

            if (client.HasData("PersonalVehicle"))
            {
                NAPI.Task.Run(() =>
                {
                    pVehicle.Delete();
                }, delayTime: 1000);
            }

            if (client.HasData("FrakVehicle"))
            {
                NAPI.Task.Run(() =>
                {
                    FrakVeh.Delete();
                }, delayTime: 1000);
            }

            if (client.HasData("cuff"))
            {
                NAPI.Task.Run(() =>
                {
                    playerInfo.jail = 1;
                }, delayTime: 1000);
            }

            Database.Update(playerInfo);
        }
    }
}
