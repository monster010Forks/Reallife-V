using GTANetworkAPI;
using reallife.Data;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Commands
{
    class FraktionCmds : Script
    {

        //FCAR ANFANG
        [Command("fcar")]
        public void CMD_FraktionsFahrzeug(Client client)
        {
            int client_id = client.GetData("ID");

            if (client.HasData("FrakVehicle"))
            {
                client.SendNotification("Du besitzt bereits ein Fraktions Fahrzeug!");
                return;
            }

            if (client.Position.DistanceTo2D(new Vector3(458.2729, -1008.082, 28.28012)) <= 5)
            {
                if(FraktionSystem.HasRank(client, 1))
                {
                    if (client.HasData("onduty"))
                    {
                        uint hash = NAPI.Util.GetHashKey("police");
                        Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(447.323150634766, -996.606872558594, 25.3755207061768), 179.479125976563f, 111, 0);
                        NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                        client.SetIntoVehicle(veh, -1);

                        client.SendChatMessage("Nutze ~b~/lock~w~ zum aufschließen oder abschließen!");

                        client.SetData("FrakVehicle", veh);
                        veh.SetData("ID", client_id);
                    }
                    else
                    {
                        client.SendNotification("~r~Du musst dafür im Dienst sein!");
                    }
                } else
                {
                    client.SendNotification("Du gehörst nicht zu dieser Fraktion!");
                }
            }
            else if (client.Position.DistanceTo2D(new Vector3(1193.662, -1487.571, 34.84266)) <= 5)
            {
                if (FraktionSystem.HasRank(client, 2))
                {
                    if (client.HasData("onduty"))
                    {
                        uint hash = NAPI.Util.GetHashKey("ambulance");
                        Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(1200.68286132813, -1489.611328125, 34.5351028442383), 179.496887207031f, 0, 0);
                        NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                        client.SetIntoVehicle(veh, -1);

                        client.SendChatMessage("Nutze ~b~/lock~w~ zum aufschließen oder abschließen!");

                        client.SetData("FrakVehicle", veh);
                        veh.SetData("ID", client_id);
                    }
                    else
                    {
                        client.SendNotification("~r~Du musst dafür im Dienst sein!");
                    }
                }
                else
                {
                    client.SendNotification("Du gehörst nicht zu dieser Fraktion!");
                }
            }
            else if (client.Position.DistanceTo2D(new Vector3(1191.315, -1474.554, 34.85954)) <= 5)
            {
                if (FraktionSystem.HasRank(client, 2))
                {
                    if (client.HasData("fonduty"))
                    {
                        uint hash = NAPI.Util.GetHashKey("firetruk");
                        Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(1200.7568359375, -1468.86804199219, 34.9273796081543), 0.40966796875f, 150, 150);
                        NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                        client.SetIntoVehicle(veh, -1);

                        client.SendChatMessage("Nutze ~b~/lock~w~ zum aufschließen oder abschließen!");

                        client.SetData("FrakVehicle", veh);
                        veh.SetData("ID", client_id);
                    }
                    else
                    {
                        client.SendNotification("~r~Du musst dafür im Dienst sein!");
                    }
                }
                else
                {
                    client.SendNotification("Du gehörst nicht zu dieser Fraktion!");
                }
            }
            else if (client.Position.DistanceTo2D(new Vector3(78.44213, -1974.952, 20.91622)) <= 5)
            {
                if (FraktionSystem.HasRank(client, 3))
                {
                    uint hash = NAPI.Util.GetHashKey("buffalo");
                    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(85.7840270996094, -1971.30688476563, 20.4022369384766), 319.387481689453f, 150, 150);
                    NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                    client.SetIntoVehicle(veh, -1);

                    client.SendChatMessage("Nutze ~b~/lock~w~ zum aufschließen oder abschließen!");

                    client.SetData("FrakVehicle", veh);
                    veh.SetData("ID", client_id);
                }
                else
                {
                    client.SendNotification("Du gehörst nicht zu dieser Fraktion!");
                }
            } else
            {
                client.SendNotification("Du bist nicht in der Nähe von einer Position wo du /fcar benutzen kannst!");
            }
        }
        //FCAR ENDE

        [Command("duty")]
        public void CMD_duty(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerInfo.wantedlevel > 0)
            {
                client.SendNotification("~r~Du kannst nicht in den Dienst gehen wenn du gesucht wirst!");
                return;
            }

            if (client.Position.DistanceTo2D(new Vector3(452.2726, -980.15, 30.68961)) <= 0.8)
            {
                if (FraktionSystem.HasRank(client, 1))
                {
                    if (client.HasData("onduty"))
                    {
                        PlayerData.OffDuty(client);
                    }
                    else
                    {
                        PlayerData.LSPDDuty(client);
                    }
                }
                else
                {
                    client.SendNotification("~r~Du gehörst nicht zur LSPD!");
                }
            }
            else if (client.Position.DistanceTo2D(new Vector3(1207.794, -1487.555, 34.84264)) <= 0.8)
            {
                if (FraktionSystem.HasRank(client, 2))
                {
                    if (client.HasData("onduty") || client.HasData("fonduty"))
                    {
                        PlayerData.OffDuty(client);
                    }
                    else
                    {
                        PlayerData.SARUDuty(client);
                    }
                }
                else
                {
                    client.SendNotification("~r~Du gehörst nicht zur SARU!");
                }
            }
            else
            {
                client.SendNotification("Du bist nicht in der nähe von einem duty!");
            }
        }
    }
}
