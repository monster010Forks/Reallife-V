using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using reallife.Data;
using reallife.Player;

namespace reallife.Fraktion
{
    class LSPD : Script
    {

        public static void cuff(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            pInfo.cuff = 1;
            pInfo.temp_location = new double[] { 460.3309, -994.3115, 24.91488 };
            client.SetData("cuff", true);
            pInfo.Update();
        }

        public static void uncuff(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            pInfo.cuff = 0;
            pInfo.temp_location = null;
            client.ResetData("cuff");
            pInfo.Update();
        }
        public LSPD()
        {
            //3D Textelemente
            NAPI.TextLabel.CreateTextLabel("Benutze ~b~/fcar~w~ um dir ein Polizeiwagen\n zu spawnen.", new Vector3(458.2729, -1008.082, 28.28012), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Benutze ~b~/entlassen~w~ oder ~b~/einsperren~w~.", new Vector3(461.9194, -989.1077, 24.91486), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Benutze ~b~/akte~w~ um die Akte von jemanden zu sehen.", new Vector3(442.9581, -975.1335, 30.68961), 12, 1f, 4, new Color(255, 255, 255, 255));


            //Checkpoint
            NAPI.Checkpoint.CreateCheckpoint(0, new Vector3(452.2535, -980.0837, 27.3896), new Vector3(0, 1, 0), 1f, new Color(198, 40, 40, 200));
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterLSPDVehicle(Client client, Vehicle vehicle, sbyte seatID)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            uint police = NAPI.Util.GetHashKey("police");

            /*if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du bist dazu nicht befugt!");
                client.WarpOutOfVehicle();
            }*/

            if (vehicle.Model == police)
            {
                if (client.VehicleSeat == -1)
                {
                    if (!FraktionSystem.HasRank(client, 1))
                    {
                        client.SendNotification("~r~Du bist dazu nicht befugt!");
                        client.WarpOutOfVehicle();
                    }
                } 
            }
        }
    }
}
