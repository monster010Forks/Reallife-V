using System;
using GTANetworkAPI;
using LiteDB;
using reallife.Player;
using reallife.Db;
using reallife.Events;
using reallife.Data;

namespace reallife.Commands
{
    public class UserCmds : Script
    {

        [Command("jailtime")]
        public void CMD_JailTime(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo.jail == 0)
            {
                client.SendNotification("Du sitzt nicht im Gefängnis!");
                return;
            }

            TimeSpan ts = TimeSpan.FromMilliseconds(pInfo.jailtime);

            client.SendNotification($"[~b~LSPD~w~]: Du sitzt für ~r~{ts.Minutes}~w~ Minuten.");
        }

        [Command("showstats")]
        public void CMD_ShowStats(Client client, Client player)
        {
            PlayerInfo cInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (client.Position.DistanceTo2D(client.Position) < 3)
            {
                player.SendNotification($"~b~Vorname:~w~ {cInfo.vorname}");
                player.SendNotification($"~b~Nachname:~w~ {cInfo.nachname}");
                //FRAKTIONABFRAGE
                player.SendNotification($"~b~Fraktion:~w~ {PlayerInfo.WhichFrak(client)}");
                //ADMINABFRAGE
                    player.SendNotification($"~b~Rolle:~w~ {PlayerInfo.WhichADMIN(client)}");
                //WANTEDABFRAGE
                if (pInfo.fraktion == 1)
                {
                    if (cInfo.wantedlevel >= 1)
                    {
                        player.SendNotification("~r~Spieler wird gesucht!");
                    }
                }
            } else
            {
                client.SendNotification("Spieler nicht in Reichweite!");
                return;
            }
        }

        [Command("save")]
        public void CMD_Save(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            pInfo.last_location = new double[] { client.Position.X, client.Position.Y, client.Position.Z };
            client.SendNotification("~g~Position erfolgreich gespeichert!");
            pInfo.Update();
        }

        [Command("hilfe")]
        public void CMD_Help(Client client)
        {
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~╔════════════════════════════════════════════════════════════════╗");
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~╠Allgemein: ~w~/befehle. ~w~ (Allgemeine Befehle)");
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~╠Allgemein: ~w~/fahrzeugbefehle. ~w~ (Befehle für Fahrzeuge)");
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~╠Allgemein: ~w~/fraktionsbefehle. ~w~ (Befehle für die Fraktion)");
            NAPI.Chat.SendChatMessageToPlayer(client, "~y~╚════════════════════════════════════════════════════════════════╝");
        }

        [Command("befehle")]
        public void CMD_Befehle(Client player)
        {
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╔════════════════════════════════════════════════════════════════╗");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Befehle:~w~ /save - Speichere deine jetzige Position.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Befehle:~w~ /showstats [Spieler] - Zeige einem Spieler Informationen über dich.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Befehle:~w~ /money - Siehe dein Geld und Bankguthaben.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Befehle:~w~ /givemoney [Spieler] [Menge] - Gib einem Spieler Geld.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╚════════════════════════════════════════════════════════════════╝");
        }

        [Command("fahrzeugbefehle")]
        public void CMD_FahrzeugBefehle(Client player)
        {
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╔════════════════════════════════════════════════════════════════╗");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Fahrzeugbefehle: ~w~/lock - Auf & Abschliessen.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Fahrzeugbefehle: ~w~/park - Fahrzeug Parken.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Fahrzeugbefehle: ~w~/gurt - An & Abschnallen.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Fahrzeugbefehle: ~w~/motor - Motor anschalten / abschalten.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Mietbefehle: ~w~/rent - Miete dir für 150$ einen Roller.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠Mietbefehle: ~w~/unrent - Gib deinen Roller wieder ab.");
            NAPI.Chat.SendChatMessageToPlayer(player, "~y~╚════════════════════════════════════════════════════════════════╝");
        }

        [Command("fraktionsbefehle")]
        public void CMD_FraktionsBefehle(Client player)
        {
            if (FraktionSystem.HasRank(player, 0))
            {
                player.SendChatMessage("[~r~Server~w~] Du gehörst zu keiner Fraktion!");
            } else if (FraktionSystem.HasRank(player, 1))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╔════════════════════════════════════════════════════════════════╗");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/duty - In oder aus dem Dienst gehen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/lspdcar - Fahrzeug spawnen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/mp [Nachricht]- Megaphone benutzen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/setwanted [Spieler] [Anzahl] - Wanteds verteilen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/delakte [Spieler]- Akte des Spielers löschen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/lock - Fahrzeug Auf-/ Abschließen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/motor - Motor starten/abschalten.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╚════════════════════════════════════════════════════════════════╝");
            }
            else if (FraktionSystem.HasRank(player, 2))
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╔════════════════════════════════════════════════════════════════╗");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/duty - In oder aus dem Dienst gehen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/sarucar - Fahrzeug spawnen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/heal [Spieler] - Spieler heilen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/lock - Fahrzeug Auf-/ Abschließen.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╠FraktionsBefehle: ~w~/motor - Motor starten/abschalten.");
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~╚════════════════════════════════════════════════════════════════╝");
            }
        }
        // Player Befehle

        // Fahrzeug Befehle
        [Command("unrent")]
        public void CMD_UnRent(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            Vehicle rent = client.GetData("RentVehicle");

            if (client.HasData("RentVehicle"))
            {
                if (client.IsInVehicle)
                {
                    if (client.Position.DistanceTo2D(rent.Position) <= 0.1)
                    {
                        if (NAPI.Vehicle.GetVehicleHealth(client) >= 800)
                        {
                            Vehicle RentVehicle = client.GetData("RentVehicle");
                            RentVehicle.Delete();
                            client.SendChatMessage("Dein Mietvertrag wurde gekündigt. Du erhälst ~g~75$~w~ zurück!");
                            client.SendNotification("Du hast ~g~75$~w~ erhalten.");
                            client.ResetData("RentVehicle");
                            pInfo.AddMoney(75);
                            Database.Update(pInfo);
                            EventTriggers.Update_Money(client);
                        }
                        else if (NAPI.Vehicle.GetVehicleHealth(client) <= 800)
                        {
                            Vehicle RentVehicle = client.GetData("RentVehicle");
                            RentVehicle.Delete();
                            client.SendChatMessage("Dein Fahrzeug hat zu viele beschädigungen. Du erhälst kein Geld zurück!");
                            client.ResetData("RentVehicle");
                        }
                    }
                    else
                    {
                        client.SendNotification("Du befindest dich nicht auf dem Roller!");
                    }
                } else
                {
                    client.SendNotification("Du befindest dich nicht auf dem Roller!");
                }
            }
            else //if (client.HasData("RentVehicle") == false)
            {
                client.SendNotification("~r~Du besitzt kein Mietfahrzeug!");
            }
        }
        [Command("vrespawn")]
        public void CMD_VehRespawn(Client client)
        {
            int client_id = client.GetData("ID");

            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);

            if (pVeh == null)
            {
                client.SendNotification("~r~Du besitzt kein Fahrzeug das respawnt werden könnte!");
                return;
            }

            if (pVeh._id == client_id)
            {
                Vehicle previous_vehicle = client.GetData("PersonalVehicle");
                previous_vehicle.Delete();

                Vector3 pVehSpawn = new Vector3(pVeh.last_location[0], pVeh.last_location[1], pVeh.last_location[2]);

                client.SendChatMessage("~g~Dein Fahrzeug wurde Respawnt!");
                uint pVehHash = NAPI.Util.GetHashKey(pVeh.carmodel);
                Vehicle veh = NAPI.Vehicle.CreateVehicle(pVehHash, pVehSpawn, pVeh.last_rotation, 0, 0);
                //NAPI.Vehicle.SetVehicleEngineStatus(veh, false);
                NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);

                NAPI.Vehicle.SetVehicleLocked(veh, true);
                NAPI.Vehicle.SetVehicleEngineStatus(veh, false);

                NAPI.Vehicle.SetVehiclePrimaryColor(veh, pVeh.Color1); NAPI.Vehicle.SetVehicleSecondaryColor(veh, pVeh.Color2); NAPI.Vehicle.SetVehicleMod(veh, 0, pVeh.spoilers);
                NAPI.Vehicle.SetVehicleMod(veh, 1, pVeh.fbumber); NAPI.Vehicle.SetVehicleMod(veh, 2, pVeh.rbumber); NAPI.Vehicle.SetVehicleMod(veh, 3, pVeh.sskirt);
                NAPI.Vehicle.SetVehicleMod(veh, 4, pVeh.exhaust); NAPI.Vehicle.SetVehicleMod(veh, 5, pVeh.frame); NAPI.Vehicle.SetVehicleMod(veh, 6, pVeh.grill); NAPI.Vehicle.SetVehicleMod(veh, 10, pVeh.roof);
                NAPI.Vehicle.SetVehicleMod(veh, 11, pVeh.motortuning); NAPI.Vehicle.SetVehicleMod(veh, 12, pVeh.brakes); NAPI.Vehicle.SetVehicleMod(veh, 13, pVeh.transmission);
                NAPI.Vehicle.SetVehicleMod(veh, 18, pVeh.turbo); NAPI.Vehicle.SetVehicleMod(veh, 23, pVeh.fwheels); NAPI.Vehicle.SetVehicleMod(veh, 24, pVeh.bwheels); //MOTORAD
                NAPI.Vehicle.SetVehicleWindowTint(veh, pVeh.window); NAPI.Vehicle.SetVehicleMod(veh, 15, pVeh.suspension);

                client.SetData("PersonalVehicle", veh);
                veh.SetData("ID", client_id);
            }
        }
        [Command("park")]
        public void CMD_Park(Client client)
        {
            int client_id = client.GetData("ID");
            Vehicle personal_vehicle = client.GetData("PersonalVehicle");

            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (client.IsInVehicle)
            {
                if (client.Position.DistanceTo2D(personal_vehicle.Position) <= 0.1)
                {
                        pVeh.last_location = new double[] { client.Vehicle.Position.X, client.Vehicle.Position.Y, client.Vehicle.Position.Z };
                        pVeh.last_rotation = client.Vehicle.Rotation.Z;
                        Database.Upsert(pVeh);

                        client.SendNotification("~g~Fahrzeug erfolgreich geparkt!");
                }
                else
                {
                    client.SendNotification("~r~Du hast kein schlüssel für dieses Fahrzeug!");
                }
            }
            else
            {
                client.SendNotification("Du befindest dich in keinem Fahrzeug!");
            }
        }

        [Command("lock")]
        public void CMD_LockVehicle(Client client)
        {

            PlayerVehicles.CarLock(client);

        }
        [Command("gurt")]
        public void CMD_gurt(Client player)
        {
            if (!player.IsInVehicle)
            {
                player.SendChatMessage("~r~Du bist in keinem Fahrzeug!");
                return;
            }

                if(!player.Seatbelt == true)
                {
                    player.SendNotification("~g~Angeschnallt!");
                    player.Seatbelt = true;
                }
                else
                {
                    player.SendNotification("~r~Abgeschnallt!");
                    player.Seatbelt = false;
                }
        }

        [Command("motor")]
        public void Motor(Client client)
        {

            PlayerVehicles.Engine(client);

        }
    }
}
