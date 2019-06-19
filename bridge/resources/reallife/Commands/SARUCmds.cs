using GTANetworkAPI;
using reallife.Data;
using reallife.Events;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Commands
{
    class SARUCmds : Script
    {
        [Command("revive")]
        public void CMD_Revive(Client client, Client player)
        {
            PlayerInfo cInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du gehörst nicht zur SARU!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du bist nicht im Dienst!");
                return;
            }

            if (client.Position.DistanceTo2D(player.Position) <= 3)
            {
                if (!player.HasData("dead"))
                {
                    client.SendNotification("Dieser Spieler ist nicht gestorben!");
                    return;
                }

                NAPI.Player.SpawnPlayer(player, pInfo.GetLastPlayerLocation());
                player.SendNotification("Du wurdest respawnt!");
                NAPI.ClientEvent.TriggerClientEvent(player, "DeathFalse");
                player.ResetData("dead");

                cInfo.money += 100;
                cInfo.Update();
                client.SendNotification("[~r~SARU~w~]: Du hast 100~g~$~w~ erhalten.");

                if (pInfo.money > 100)
                {
                    pInfo.money -= 100;
                    pInfo.Update();
                    player.SendNotification("[~r~SARU~w~]: Du hast für die Behandlung 100~g~$~w~ bezahlt.");
                }
                else if (pInfo.bank > 100)
                {
                    pInfo.bank -= 100;
                    pInfo.Update();
                    player.SendNotification("[~r~SARU~w~]: Du hast für die Behandlung 100~g~$~w~ bezahlt.");
                } else
                {
                    client.SendNotification("[~y~EasterEgg~w~]: Du bist verdammt arm! geh arbeiten!");
                }

                EventTriggers.Update_Money(client);
                EventTriggers.Update_Bank(client);

                EventTriggers.Update_Money(player);
                EventTriggers.Update_Bank(player);

            } else
            {
                client.SendNotification("Du bist nicht in der Nähe dieser Person!");
            }
        }

        [Command("fduty")]
        public void CMD_fduty(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (client.Position.DistanceTo2D(new Vector3(1194.831, -1477.961, 34.85954)) <= 0.8)
            {
                if (FraktionSystem.HasRank(client, 2))
                {
                    if (client.HasData("fonduty") || client.HasData("onduty"))
                    {
                        PlayerData.OffDuty(client);
                    }
                    else
                    {
                        client.SetClothes(8, 129, 0);
                        client.SetClothes(11, 95, 0);
                        client.SetClothes(4, 13, 0);
                        client.SetClothes(6, 10, 0);
                        client.SetClothes(0, 124, 0);
                        client.SetClothes(5, 45, 0);

                        client.SendNotification("Du bist nun im Dienst!");

                        client.SetData("fonduty", 1);
                    }
                }
                else
                {
                    client.SendNotification("~r~Du gehörst nicht zur SARU!");
                }
            } else
            {
                client.SendNotification("Du bist nicht in der nähe von einem duty!");
            }
        }

        [Command("heal")]
        public void CMD_heal(Client client, Client player)
        {
            if (!FraktionSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du gehörst nicht zur SARU!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du befindest dich nicht im Dienst!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber angeben!");
                return;
            }

            if (client.Position.DistanceTo2D(player.Position) <= 3)
            {
                player.Health = 100;
                client.SendNotification("~g~Spieler erfolgreich geheilt!");
                player.SendNotification($"~g~Du wurdest von {client.Name} geheilt!");
            } else
            {
                client.SendNotification("Der Spieler befindet sich nicht in deiner Nähe!");
            }
        }

        /*[Command("sarucar")]
        public void CMD_saruCar(Client client)
        {

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (!FraktionSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du gehörst nicht zur SARU!");
                return;
            }

            if (client.Position.DistanceTo2D(new Vector3(1193.662, -1487.571, 34.84266)) <= 5)
            {
                if (client.HasData("FrakVehicle"))
                {
                    client.SendNotification("Du besitzt bereits ein SARU Fahrzeug!");
                    return;
                }

                if (client.HasData("onduty"))
                {
                    uint hash = NAPI.Util.GetHashKey("ambulance");
                    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(1200.68286132813, -1489.611328125, 34.5351028442383), 179.496887207031f, 0, 0);
                    NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                    client.SetIntoVehicle(veh, -1);

                    client.SendChatMessage("Nutze ~b~/lock~w~ zum aufschließen oder abschließen!");

                    client.SetData("FrakVehicle", veh);
                }
                else
                {
                    client.SendNotification("~r~Du musst dafür im Dienst sein!");
                }
            }
            else if (client.Position.DistanceTo2D(new Vector3(1191.315, -1474.554, 34.85954)) <= 5)
            {
                if (client.HasData("FrakVehicle"))
                {
                    client.SendNotification("Du besitzt bereits ein SARU Fahrzeug!");
                    return;
                }

                if (client.HasData("fonduty"))
                {
                    uint hash = NAPI.Util.GetHashKey("firetruk");
                    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(1200.7568359375, -1468.86804199219, 34.9273796081543), 0.40966796875f, 150, 150);
                    NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                    client.SetIntoVehicle(veh, -1);

                    client.SendChatMessage("Nutze ~b~/lock~w~ zum aufschließen oder abschließen!");

                    client.SetData("FrakVehicle", veh);
                }
                else
                {
                    client.SendNotification("~r~Du musst dafür im Dienst sein!");
                }
            }
            else
            {
                client.SendNotification("Du bist nicht in Reichweite!");
            }
        }*/
    }
}
