using GTANetworkAPI;
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
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            client.SetClothes(1, playerInfo.clothes_1, 0);
            client.SetClothes(2, playerInfo.clothes_2, 0);
            client.SetClothes(3, playerInfo.clothes_3, 0);
            client.SetClothes(4, playerInfo.clothes_4, 0);
            client.SetClothes(5, playerInfo.clothes_5, 0);
            client.SetClothes(6, playerInfo.clothes_6, 0);
            client.SetClothes(7, playerInfo.clothes_7, 0);
            client.SetClothes(8, playerInfo.clothes_8, 0);
            client.SetClothes(9, playerInfo.clothes_9, 0);
            client.SetClothes(10, playerInfo.clothes_10, 0);
            client.SetClothes(11, playerInfo.clothes_11, 0);

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


            //playerInfo.cuff - Testzwecke
            if(playerInfo.jail == 1 || playerInfo.cuff == 1)
            {
                client.Position = playerInfo.GetLastTempLocation();
                client.Freeze(true);
                client.RemoveAllWeapons();

                playerInfo.cuff = 0;
                playerInfo.Update();
            } else
            {
                client.Position = playerInfo.GetLastPlayerLocation();
                client.Freeze(false);
            }

            client.SendNotification("~g~Du wurdest respawnt!");
        }

        public static void ResetClothes(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            client.SetClothes(1, playerInfo.clothes_1, 0);
            client.SetClothes(2, playerInfo.clothes_2, 0);
            client.SetClothes(3, playerInfo.clothes_3, 0);
            client.SetClothes(4, playerInfo.clothes_4, 0);
            client.SetClothes(5, playerInfo.clothes_5, 0);
            client.SetClothes(6, playerInfo.clothes_6, 0);
            client.SetClothes(7, playerInfo.clothes_7, 0);
            client.SetClothes(8, playerInfo.clothes_8, 0);
            client.SetClothes(9, playerInfo.clothes_9, 0);
            client.SetClothes(10, playerInfo.clothes_10, 0);
            client.SetClothes(11, playerInfo.clothes_11, 0);
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
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            client.SetClothes(1, playerInfo.clothes_1, 0);
            client.SetClothes(2, playerInfo.clothes_2, 0);
            client.SetClothes(3, playerInfo.clothes_3, 0);
            client.SetClothes(4, playerInfo.clothes_4, 0);
            client.SetClothes(5, playerInfo.clothes_5, 0);
            client.SetClothes(6, playerInfo.clothes_6, 0);
            client.SetClothes(7, playerInfo.clothes_7, 0);
            client.SetClothes(8, playerInfo.clothes_8, 0);
            client.SetClothes(9, playerInfo.clothes_9, 0);
            client.SetClothes(10, playerInfo.clothes_10, 0);
            client.SetClothes(11, playerInfo.clothes_11, 0);

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
