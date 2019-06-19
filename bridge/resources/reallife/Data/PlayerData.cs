using GTANetworkAPI;
using reallife.Events;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Data
{
    class PlayerData
    {
        public static void Respawn(Client client)
        {
            PlayerClothes playerClothes = PlayerHelper.GetPlayerClothes(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerClothes == null)
            {
                playerClothes = new PlayerClothes();
                playerClothes._id = playerInfo._id;
                playerClothes.Upsert();
            }

            client.SetClothes(1, playerClothes.clothes_1, 0);
            client.SetClothes(2, playerClothes.clothes_2, 0);
            client.SetClothes(3, playerClothes.clothes_3, 0);
            client.SetClothes(4, playerClothes.clothes_4, 0);
            client.SetClothes(5, playerClothes.clothes_5, 0);
            client.SetClothes(6, playerClothes.clothes_6, 0);
            client.SetClothes(7, playerClothes.clothes_7, 0);
            client.SetClothes(8, playerClothes.clothes_8, 0);
            client.SetClothes(9, playerClothes.clothes_9, 0);
            client.SetClothes(10, playerClothes.clothes_10, 0);
            client.SetClothes(11, playerClothes.clothes_11, 0);

            if (client.HasData("FrakVehicle"))
            {
                Vehicle previous_vehicle = client.GetData("FrakVehicle");
                previous_vehicle.Delete();
                client.ResetData("FrakVehicle");
                client.SendNotification("Dein Fraktionsfahrzeug wurde gelöscht!");
            }

            if (client.HasData("onduty"))
            {
                client.ResetData("onduty");
                client.SendNotification("Du bist nun nicht mehr im Dienst!");
            }

            if (client.HasData("fonduty"))
            {
                client.ResetData("fonduty");
                client.SendNotification("Du bist nun nicht mehr im Dienst!");
            }

            if (playerInfo.jail == 1)
            {
                TimeSpan ts = TimeSpan.FromMilliseconds(playerInfo.jailtime);

                client.Position = playerInfo.GetLastTempLocation();
                client.RemoveAllWeapons();

                if (playerInfo.wantedlevel == 1)
                {
                    playerInfo.jailtime += 60000;
                }
                else if (playerInfo.wantedlevel == 2)
                {
                    playerInfo.jailtime += 120000;
                }
                else if (playerInfo.wantedlevel == 3)
                {
                    playerInfo.jailtime += 180000;
                }
                else if (playerInfo.wantedlevel == 4)
                {
                    playerInfo.jailtime += 240000;
                }
                else if (playerInfo.wantedlevel == 5)
                {
                    playerInfo.jailtime += 300000;
                }
                else if (playerInfo.wantedlevel >= 6)
                {
                    playerInfo.jailtime += 360000;
                }

                playerInfo.wantedlevel = 0;
                playerInfo.cuff = 0;
                playerInfo.Update();

                NAPI.ClientEvent.TriggerClientEvent(client, "JailTrue");
                client.SendNotification("[~b~LSPD~w~]: Bitte logge dich nicht aus sonst sitzt du wieder so lange!");

                client.SendNotification($"[~b~LSPD~w~]: Du sitzt für ~r~{ts.Minutes}~w~ Minuten.");

                NAPI.Task.Run(() =>
                {
                    NAPI.ClientEvent.TriggerClientEvent(client, "JailFalse");
                    client.SendNotification("[~b~LSPD~w~]: Du bist nun frei!");
                    playerInfo.jail = 0;
                    playerInfo.jailtime = 0;
                    playerInfo.Update();
                    Respawn(client);
                }, delayTime: playerInfo.jailtime);
            } else
            {
                client.Position = playerInfo.GetLastPlayerLocation();
            }

            EventTriggers.Update_Wanteds(client);
            EventTriggers.Update_Money(client);
            EventTriggers.Update_Bank(client);

            client.SendNotification("~g~Du wurdest respawnt!");
        }

        public static void ResetClothes(Client client)
        {

            int client_id = client.GetData("ID");
            PlayerClothes playerClothes = PlayerHelper.GetPlayerClothes(client);

            if (playerClothes == null)
            {
                playerClothes = new PlayerClothes();
                playerClothes._id = client_id;
                playerClothes.Upsert();
            }

            if (playerClothes._id == client_id)
            {
                client.SetClothes(1, playerClothes.clothes_1, 0);
                client.SetClothes(2, playerClothes.clothes_2, 0);
                client.SetClothes(3, playerClothes.clothes_3, 0);
                client.SetClothes(4, playerClothes.clothes_4, 0);
                client.SetClothes(5, playerClothes.clothes_5, 0);
                client.SetClothes(6, playerClothes.clothes_6, 0);
                client.SetClothes(7, playerClothes.clothes_7, 0);
                client.SetClothes(8, playerClothes.clothes_8, 0);
                client.SetClothes(9, playerClothes.clothes_9, 0);
                client.SetClothes(10, playerClothes.clothes_10, 0);
                client.SetClothes(11, playerClothes.clothes_11, 0);
            }
        }

        public static void LSPDDuty(Client client)
        {
            client.SetClothes(8, 129, 0);
            client.SetClothes(11, 55, 0);
            client.SetClothes(4, 13, 0);
            client.SetClothes(6, 10, 0);
            client.SetClothes(7, 0, 0);

            client.RemoveAllWeapons();

            WeaponHash hash1 = NAPI.Util.WeaponNameToModel("pistol");
            client.GiveWeapon(hash1, 999);

            WeaponHash hash2 = NAPI.Util.WeaponNameToModel("smg");
            client.GiveWeapon(hash2, 999);

            WeaponHash hash3 = NAPI.Util.WeaponNameToModel("flashlight");
            client.GiveWeapon(hash3, 1);

            WeaponHash hash4 = NAPI.Util.WeaponNameToModel("nightstick");
            client.GiveWeapon(hash4, 1);


            client.SendNotification("Du bist nun im Dienst!");
            client.SetData("onduty", 1);
        }

        public static void SARUDuty(Client client)
        {
            client.SetClothes(8, 129, 0);
            client.SetClothes(11, 26, 0);
            client.SetClothes(4, 13, 0);
            client.SetClothes(6, 10, 0);
            client.SetClothes(7, 126, 0);

            client.RemoveAllWeapons();

            client.SendNotification("Du bist nun im Dienst!");
            client.SetData("onduty", 1);
        }

        public static void OffDuty(Client client)
        {
            PlayerClothes playerClothes = PlayerHelper.GetPlayerClothes(client);

            client.SetClothes(1, playerClothes.clothes_1, 0);
            client.SetClothes(2, playerClothes.clothes_2, 0);
            client.SetClothes(3, playerClothes.clothes_3, 0);
            client.SetClothes(4, playerClothes.clothes_4, 0);
            client.SetClothes(5, playerClothes.clothes_5, 0);
            client.SetClothes(6, playerClothes.clothes_6, 0);
            client.SetClothes(7, playerClothes.clothes_7, 0);
            client.SetClothes(8, playerClothes.clothes_8, 0);
            client.SetClothes(9, playerClothes.clothes_9, 0);
            client.SetClothes(10, playerClothes.clothes_10, 0);
            client.SetClothes(11, playerClothes.clothes_11, 0);

            client.RemoveAllWeapons();

            if (client.HasData("FrakVehicle"))
            {
                Vehicle previous_vehicle = client.GetData("FrakVehicle");
                previous_vehicle.Delete();
                client.ResetData("FrakVehicle");
                client.SendNotification("Dein Fraktionsfahrzeug wurde gelöscht!");
            }

            if (client.HasData("onduty"))
            {
                client.ResetData("onduty");
            }

            if (client.HasData("fonduty"))
            {
                client.ResetData("fonduty");
            }

            client.SendNotification("Du bist nun nicht mehr im Dienst!");
        }
    }
}
