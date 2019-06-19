using System;
using GTANetworkAPI;
using reallife.Blips;
using reallife.Db;
using reallife.Player;
using reallife.Events;

namespace reallife
{
    public class Main : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            Console.WriteLine("(~~~~~~~~~~~~~~~~~~~~~)");
            Console.WriteLine("( Script by AbsturzPowa 2019)");
            Console.WriteLine("( Version Dev0.4.1");
            Console.WriteLine("(~~~~~~~~~~~~~~~~~~~~~)");

            NAPI.Server.SetAutoRespawnAfterDeath(false);
            NAPI.Server.SetGlobalServerChat(false);
            NAPI.Server.SetAutoSpawnOnConnect(false);
            NAPI.Server.SetAutoRespawnAfterDeath(false);

            NAPI.Server.SetCommandErrorMessage("[~r~SERVER:~w~] Dieser Command Existiert nicht!");
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {  
            for (int i = 0; i < 99; i++) player.SendChatMessage("~w~");
            player.SendChatMessage("╔═══════════════Willkommen auf ~b~Reallife-V~w~══════════════════╗");
            player.SendChatMessage("╠Server Version: ~b~Dev0.4.1                               ");
            player.SendChatMessage("╚═══════════════════════════════════════════════════╝");
            NAPI.ClientEvent.TriggerClientEvent(player, "ConnectFreeze");
            CameraSpawn(player);
            TuneMarker(player);
            RepairMarker1(player);
            RepairMarker2(player);
            RepairMarker3(player);
            Garage1(player);
        }

        private void CameraSpawn(Client player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "Camera");
            Vector3 spawnPos = new Vector3(-370.5946, 4403.613, 46.35886);
            NAPI.Player.SpawnPlayer(player, spawnPos);
            player.Position = spawnPos;
            player.Health = 100;
        }
        public void Garage1(Client client)
        {
            ColShape colShape = NAPI.ColShape.CreateCylinderColShape(new Vector3(-877.3158, -2733.172, 13.82848), 3, 4);
            Marker garag1 = NAPI.Marker.CreateMarker(20, new Vector3(-877.3158, -2733.172, 13.82848), new Vector3(), new Vector3(0, 0, 239.1787), 1, new Color(16, 78, 139, 100));
            Marker gragewill = NAPI.Marker.CreateMarker(30, new Vector3(-87.8849, -2741.272, 13.93595), new Vector3(), new Vector3(0, 0, 236.175), 1, new Color(16, 78, 139, 100));
            colShape.SetData("grag1", garag1);
        }
        //TUNE
        public void TuneMarker(Client client)
        {
            ColShape colShape = NAPI.ColShape.CreateCylinderColShape(new Vector3(-1038.663, -2677.984, 13.83076), 3, 4);
            Marker testtuner = NAPI.Marker.CreateMarker(21, new Vector3(-1038.663, -2677.984, 13.83076), new Vector3(), new Vector3(0, 0, 147.4581), 1, new Color(16, 78, 139, 100));
            colShape.SetData("testtuner", testtuner);
        }
        //REPAIR
        public void RepairMarker1(Client client)
        {
            ColShape colShape = NAPI.ColShape.CreateCylinderColShape(new Vector3(731.3959, -1088.848, 22.16904), 3, 4);
            Marker repair1 = NAPI.Marker.CreateMarker(0, new Vector3(731.3959, -1088.848, 22.16904), new Vector3(), new Vector3(0, 0, 270.1672), 1, new Color(16, 78, 139, 100));
            colShape.SetData("repair1", repair1);
        }

        public void RepairMarker2(Client client)
        {
            ColShape colShape = NAPI.ColShape.CreateCylinderColShape(new Vector3(111.7056, 6625.828, 31.40783), 3, 4);
            Marker repair2 = NAPI.Marker.CreateMarker(0, new Vector3(111.7056, 6625.828, 31.40783), new Vector3(), new Vector3(0, 0, 6.967081), 1, new Color(16, 78, 139, 100));
            colShape.SetData("repair2", repair2);
        }

        public void RepairMarker3(Client client)
        {
            ColShape colShape = NAPI.ColShape.CreateCylinderColShape(new Vector3(-965.0243, -3024.913, 13.56587), 3, 4);
            Marker repair3 = NAPI.Marker.CreateMarker(0, new Vector3(-965.0243, -3024.913, 13.56587), new Vector3(), new Vector3(0, 0, 6.967081), 1, new Color(16, 78, 139, 100));
            colShape.SetData("repair3", repair3);
        }
        [ServerEvent(Event.PlayerEnterColshape)]
        public void Event_EnterColshape(ColShape colshape, Client client)
        {
            Marker grage1 = colshape.GetData("grag1");
            if(grage1 != null)
            {
                client.SendNotification("Vehicle Enter Point Garage");
                return;
            }

            Marker repair1 = colshape.GetData("repair1");
            if (repair1 != null)
            {
                client.SendNotification("~b~Fahrzeug Repariert!");
                client.Vehicle.Repair();
            }

            Marker repair2 = colshape.GetData("repair2");
            if(repair2 != null)
            {
                client.SendNotification("~b~Fahrzeug Repariert!");
                client.Vehicle.Repair();
            }

            Marker repair3 = colshape.GetData("repair3");
            if (repair3 != null)
            {
                client.SendNotification("~b~Fahrzeug Repariert!");
                client.Vehicle.Repair();
            }
            Marker tunepoint = colshape.GetData("testtuner");
            if (tunepoint != null)
            {
                client.SendNotification("Nutze /tune");
            }
        }
    }
}
