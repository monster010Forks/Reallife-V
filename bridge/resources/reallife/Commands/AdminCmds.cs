using System;
using GTANetworkAPI;
using reallife.Db;
using reallife.Player;
using reallife.Events;
using reallife.Data;

namespace reallife.Commands
{
    class AdminCmds : Script
    {
        [Command("respawn")]
        public void CMD_Respawn(Client client, string target)
        {
            Client player = NAPI.Player.GetPlayerFromName(target);

            if (!AdminSystem.HasRank(client, 1))
            {
                client.SendChatMessage("Du bist nicht dazu befugt!");
                return;
            }

            if (player != null)
            {
                PlayerData.Respawn(player);
            } else
            {
                client.SendChatMessage("Spieler wurde nicht gefunden!");
                return;
            }
        }

        [Command("tune")]
        public void CMD_tune (Client client, int color1, int color2, int motor, int window, int turbo, int spoiler, int wheel)
        {
            int client_id = client.GetData("ID");
            Vehicle personal_vehicle = client.GetData("PersonalVehicle");

            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);

            if (client.Position.DistanceTo2D(new Vector3(-1038.625, -2678.062, 13.25966)) <= 4)
            {
                if (!client.IsInVehicle)
                {
                    client.SendNotification("~r~Du bist in keinem Fahrzeug!");
                    return;
                }
                else if (client.Position.DistanceTo2D(personal_vehicle.Position) <= 0.1)
                {
                    if (pVeh._id == client_id)
                    {
                        Vehicle previous_vehicle = client.GetData("PersonalVehicle");
                        previous_vehicle.Delete();

                        Vector3 pVehSpawn = new Vector3(-1038.625, -2678.062, 13.25966);
                        uint pVehHash = NAPI.Util.GetHashKey(pVeh.carmodel);
                        Vehicle veh = NAPI.Vehicle.CreateVehicle(pVehHash, pVehSpawn, 149.258f, 0, 0);
                        NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                        client.SetIntoVehicle(veh, -1);

                        //TUNES

                        pVeh.Color1 = color1; pVeh.Color2 = color2; pVeh.spoilers = spoiler; pVeh.fbumber = -1; pVeh.rbumber = -1; pVeh.sskirt = -1; pVeh.exhaust = -1; pVeh.frame = -1;
                        pVeh.grill = -1; pVeh.roof = -1; pVeh.motortuning = motor; pVeh.brakes = -1; pVeh.transmission = -1; pVeh.turbo = turbo; pVeh.fwheels = wheel; pVeh.bwheels = -1;
                        pVeh.window = window; pVeh.suspension = -1;

                        NAPI.Vehicle.SetVehiclePrimaryColor(veh, pVeh.Color1); NAPI.Vehicle.SetVehicleSecondaryColor(veh, pVeh.Color2); NAPI.Vehicle.SetVehicleMod(veh, 0, pVeh.spoilers);
                        NAPI.Vehicle.SetVehicleMod(veh, 1, pVeh.fbumber); NAPI.Vehicle.SetVehicleMod(veh, 2, pVeh.rbumber); NAPI.Vehicle.SetVehicleMod(veh, 3, pVeh.sskirt);
                        NAPI.Vehicle.SetVehicleMod(veh, 4, pVeh.exhaust); NAPI.Vehicle.SetVehicleMod(veh, 5, pVeh.frame); NAPI.Vehicle.SetVehicleMod(veh, 6, pVeh.grill); NAPI.Vehicle.SetVehicleMod(veh, 10, pVeh.roof);
                        NAPI.Vehicle.SetVehicleMod(veh, 11, pVeh.motortuning); NAPI.Vehicle.SetVehicleMod(veh, 12, pVeh.brakes); NAPI.Vehicle.SetVehicleMod(veh, 13, pVeh.transmission);
                        NAPI.Vehicle.SetVehicleMod(veh, 18, pVeh.turbo); NAPI.Vehicle.SetVehicleMod(veh, 23, pVeh.fwheels); NAPI.Vehicle.SetVehicleMod(veh, 24, pVeh.bwheels); //MOTORAD
                        NAPI.Vehicle.SetVehicleWindowTint(veh, pVeh.window); NAPI.Vehicle.SetVehicleMod(veh, 15, pVeh.suspension);

                        Database.Update(pVeh);
                        client.SetData("PersonalVehicle", veh);
                    }
                }
                else
                {
                    client.SendNotification("~r~Du darfst dieses Fahrzeug nicht tunen!");
                    return;
                }
            }
            else
            {
                client.SendNotification("Du bist nicht beim Test Tuner");
                return;
            }
        }

        [Command("clearchat")]
        public void CMD_ClearChat(Client client)
        {
            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

            for (int i = 0; i < 99; i++) NAPI.Chat.SendChatMessageToAll("~w~");
            NAPI.Chat.SendChatMessageToAll($"~r~[SERVER]: ~w~{client.Name} hat den Chat gesäubert!");

        }

        [Command("makeadmin")]
        public void CMD_MakeAdmin(Client client, string playerName, int rank)
        {
            Client player = NAPI.Player.GetPlayerFromName(playerName);

            if (!AdminSystem.HasRank(client, 3))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

            if (player != null)
            {
                if (AdminSystem.SetRank(client, playerName, rank))
                {
                    client.SendNotification("Rank wurde gesetzt!");
                    return;
                }
                else
                {
                    client.SendNotification("Rank wurde NICHT gesetzt!");
                    return;
                }
            } else
            {
                client.SendChatMessage("Spieler wurde nicht gefunden!");
                return;
            }

        }

        [Command("makeleader")]
        public void CMD_MakeLeader(Client client, Client player, int rank)
        {
            PlayerInfo tarInfo = PlayerHelper.GetPlayerStats(player);

            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                if (rank == 1)
                {
                    tarInfo.fraktion = rank;
                    tarInfo.fleader = rank;
                    tarInfo.last_location = new double[] { 447.9005, -973.0226, 30.68961 };
                    tarInfo.Update();
                    PlayerData.Respawn(client);
                    player.SendChatMessage("Du wurdest zum ~y~Leader~w~ der ~b~LSPD~w~ ernannt!");
                }
                else if (rank == 2)
                {
                    tarInfo.fraktion = rank;
                    tarInfo.fleader = rank;
                    tarInfo.last_location = new double[] { 1151.196, -1529.605, 35.36937 };
                    //Rotation: 325.3967
                    tarInfo.Update();
                    PlayerData.Respawn(client);
                    player.SendChatMessage("Du wurdest zum ~y~Leader~w~ der ~b~SARU~w~ ernannt!");
                }
                else if (rank == 0)
                {
                    tarInfo.fraktion = rank;
                    tarInfo.fleader = rank;
                    tarInfo.last_location = new double[] { -1167.994, -700.4285, 21.89281 };
                    tarInfo.Update();
                    PlayerData.Respawn(client);
                    player.SendChatMessage($"Du wurdest als Leader entlassen und bist absofort Arbeitslos!");
                }
        }

        [Command("kick")]
        public void CMD_Kick(Client client, Client player, string grund)
        {
            KickLog Kicklog = PlayerHelper.KickLogs(client);

            if (!AdminSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                NAPI.Chat.SendChatMessageToAll($"[~r~SERVER~w~] Der Spieler {player.Name} wurde wegen: {grund}, ~y~gekickt!");
                Kicklog = new KickLog();
                Kicklog.kicked = player.Name;
                Kicklog.kickedby = client.Name;
                Kicklog.grund = grund;
                Database.Upsert(Kicklog);
                player.Kick();
        }

        [Command("unban")]
        public void CMD_UnBan(Client client, Client player)
        {
            PlayerInfo tarInfo = PlayerHelper.GetPlayerStats(player);

            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

            if (tarInfo == null)
            {
                client.SendNotification("Spieler konnte in der Datenbank NICHT gefunden werden.");
                return;
            }

            if (tarInfo.ban <= 0 && tarInfo.ban >= 2)
            {
                client.SendNotification("Spieler ist nicht gebannt!");
                return;
            }

            if (tarInfo.ban == 1)
            {
                tarInfo.ban = 0;
                client.SendNotification("Spieler wurde erfolgreich entbannt!");
                tarInfo.Update();
            }
        }

        [Command("ban")]
        public void CMD_Ban(Client client, Client player, string grund)
        {
            PlayerInfo tarInfo = PlayerHelper.GetPlayerStats(player);
            BanLog Banlog = PlayerHelper.BanLogs(client);

            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

            if (tarInfo.username == null)
            {
                client.SendNotification("Spieler existiert nicht!");
            }

            tarInfo.ban = 1;
            Database.Update(tarInfo);
            tarInfo.Update();
            player.SendChatMessage("~r~Du wurdest gebannt!");
            NAPI.Chat.SendChatMessageToAll($"[~r~SERVER~w~]Der Spieler {player.Name} wurde wegen: {grund}, ~r~gebannt!");
            client.SendNotification($"Du hast den Spieler {player.Name} erfolgreich gebannt!");
            Banlog = new BanLog();
            Banlog.banned = player.Name;
            Banlog.bannedby = client.Name;
            Banlog.grund = grund;
            Database.Upsert(Banlog);
            player.Kick();
        }

        [Command("tempban")]
        public void CMD_TempBan(Client player)
        {
            //Befehl hier einfügen
            //Spieler wird vom Server Zeitlich ausgeschlossen
        }

        [Command("setweather")]
        public void CMD_SetWeather(Client player)
        {
            //Befehl hier einfügen
            //Das Wetter von ganz GTA wird verändert
        }

        [Command("goto")]
        public void CMD_GoTo(Client client, Client player)
        {
            if (!AdminSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                client.Position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z);
        }

        [Command("gethere")]
        public void CMD_GetHere(Client client, Client player1, Client player2)
        {

            if (!AdminSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                player1.Position = new Vector3(player2.Position.X, player2.Position.Y, player2.Position.Z);
        }

        [Command("getweapon")]
        public void CMD_GetWeapon(Client client, WeaponHash hash)
        {
            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                client.GiveWeapon(hash, 999);
        }

        [Command("sethealth")]
        public void CMD_SetHealth(Client client, int wert)
        {

            if (!AdminSystem.HasRank(client, 3))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                client.Health = wert;
        }

        [Command("ac")]
        public void CMD_AC(Client client, string message)
        {
            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                NAPI.Chat.SendChatMessageToAll($"[~r~AC~w~] {client.Name} sagt: {message}");
        }

        [Command("setmoney")]
        public void CMD_setmoney(Client client, Client player, double amount)
        {
            if (!client.HasData("ID"))
                return;

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

            client.SendNotification($"Du hast dem Spieler ~y~{player.Name}~w~ erfolgreich ~g~{amount}$~w~ gegeben!");
            player.SendNotification($"Du hast ~g~{amount}$~w~ erhalten!");

            pInfo.AddMoney(amount);

            Database.Update(pInfo);
            EventTriggers.Update_Money(player);
        }
        [Command("getpos")]
        public void GetPosition(Client client, string message)
        {

            if (!AdminSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(client);
                Vector3 rPlayerPos = NAPI.Entity.GetEntityRotation(client);
                NAPI.Chat.SendChatMessageToPlayer(client, $"X:  {PlayerPos.X} Y: {PlayerPos.Y} Z: {PlayerPos.Z} | {message} |");
                NAPI.Chat.SendChatMessageToPlayer(client, $"rX: {rPlayerPos.X} rY: {rPlayerPos.Y} rZ {rPlayerPos.Z}");
                Console.WriteLine($"X: {PlayerPos.X} Y: {PlayerPos.Y} Z: {PlayerPos.Z}  | rX: {rPlayerPos.X} rY: {rPlayerPos.Y} rZ {rPlayerPos.Z} | {message} |");
        }

        [Command("veh")]
        public void CMD_CreateVeh(Client client, string fahrzeug_modell)
        {
            int client_id = client.GetData("ID");

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);

            if (!AdminSystem.HasRank(client, 2))
            {
                client.SendNotification("~r~Du hast dazu keine Berechtigung!");
                return;
            }

                if (pVeh == null)
                {
                    uint hash = NAPI.Util.GetHashKey(fahrzeug_modell);
                    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, client.Position, client.Rotation.Z, 0, 0);
                    NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                    client.SetIntoVehicle(veh, -1);

                    veh.Locked = true;
                    pVeh = new PlayerVehicles();
                    pVeh._id = client_id;
                    pVeh.carslot += 1;
                    pVeh.carmodel = fahrzeug_modell;
                    pVeh.last_location = new double[] { veh.Position.X, veh.Position.Y, veh.Position.Z };
                    pVeh.last_rotation = veh.Rotation.Z;

                    //TUNES
                    pVeh.Color1 = 0; pVeh.Color2 = 0; pVeh.spoilers = -1;pVeh.fbumber = -1; pVeh.rbumber = -1;pVeh.sskirt = -1;pVeh.exhaust = -1;pVeh.frame = -1;
                    pVeh.grill = -1; pVeh.roof = -1;pVeh.motortuning = -1; pVeh.brakes = -1;pVeh.transmission = -1; pVeh.turbo = -1; pVeh.fwheels = -1; pVeh.bwheels = -1;
                    pVeh.window = -1;

                    NAPI.Vehicle.SetVehiclePrimaryColor(veh, pVeh.Color1); NAPI.Vehicle.SetVehicleSecondaryColor(veh, pVeh.Color2); NAPI.Vehicle.SetVehicleMod(veh, 0, pVeh.spoilers);
                    NAPI.Vehicle.SetVehicleMod(veh, 1, pVeh.fbumber); NAPI.Vehicle.SetVehicleMod(veh, 2, pVeh.rbumber); NAPI.Vehicle.SetVehicleMod(veh, 3, pVeh.sskirt);
                    NAPI.Vehicle.SetVehicleMod(veh, 4, pVeh.exhaust);NAPI.Vehicle.SetVehicleMod(veh, 5, pVeh.frame); NAPI.Vehicle.SetVehicleMod(veh, 6, pVeh.grill);NAPI.Vehicle.SetVehicleMod(veh, 10, pVeh.roof);
                    NAPI.Vehicle.SetVehicleMod(veh, 11, pVeh.motortuning);NAPI.Vehicle.SetVehicleMod(veh, 12, pVeh.brakes); NAPI.Vehicle.SetVehicleMod(veh, 13, pVeh.transmission);
                    NAPI.Vehicle.SetVehicleMod(veh, 18, pVeh.turbo);NAPI.Vehicle.SetVehicleMod(veh, 23, pVeh.fwheels); NAPI.Vehicle.SetVehicleMod(veh, 24, pVeh.bwheels); //MOTORAD
                    NAPI.Vehicle.SetVehicleWindowTint(veh, pVeh.window);

                    Database.Upsert(pVeh);
                    client.SetData("PersonalVehicle", veh);
                }
                else if (pVeh._id == client_id)
                {
                    Vehicle previous_vehicle = client.GetData("PersonalVehicle");
                    previous_vehicle.Delete();

                    uint hash = NAPI.Util.GetHashKey(fahrzeug_modell);
                    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, client.Position, client.Rotation.Z, 0, 0);
                    NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                    client.SetIntoVehicle(veh, -1);

                    veh.Locked = true;
                    pVeh = new PlayerVehicles();
                    pVeh._id = client_id;
                    pVeh.carslot += 1;
                    pVeh.carmodel = fahrzeug_modell;
                    pVeh.last_location = new double[] { veh.Position.X, veh.Position.Y, veh.Position.Z };
                    pVeh.last_rotation = veh.Rotation.Z;

                    //TUNES
                    pVeh.Color1 = 0; pVeh.Color2 = 0; pVeh.spoilers = -1; pVeh.fbumber = -1; pVeh.rbumber = -1; pVeh.sskirt = -1; pVeh.exhaust = -1; pVeh.frame = -1;
                    pVeh.grill = -1; pVeh.roof = -1; pVeh.motortuning = -1; pVeh.brakes = -1; pVeh.transmission = -1; pVeh.turbo = -1; pVeh.fwheels = -1; pVeh.bwheels = -1;
                    pVeh.window = -1; pVeh.suspension = -1;

                    NAPI.Vehicle.SetVehiclePrimaryColor(veh, pVeh.Color1); NAPI.Vehicle.SetVehicleSecondaryColor(veh, pVeh.Color2); NAPI.Vehicle.SetVehicleMod(veh, 0, pVeh.spoilers);
                    NAPI.Vehicle.SetVehicleMod(veh, 1, pVeh.fbumber); NAPI.Vehicle.SetVehicleMod(veh, 2, pVeh.rbumber); NAPI.Vehicle.SetVehicleMod(veh, 3, pVeh.sskirt);
                    NAPI.Vehicle.SetVehicleMod(veh, 4, pVeh.exhaust); NAPI.Vehicle.SetVehicleMod(veh, 5, pVeh.frame); NAPI.Vehicle.SetVehicleMod(veh, 6, pVeh.grill); NAPI.Vehicle.SetVehicleMod(veh, 10, pVeh.roof);
                    NAPI.Vehicle.SetVehicleMod(veh, 11, pVeh.motortuning); NAPI.Vehicle.SetVehicleMod(veh, 12, pVeh.brakes); NAPI.Vehicle.SetVehicleMod(veh, 13, pVeh.transmission);
                    NAPI.Vehicle.SetVehicleMod(veh, 18, pVeh.turbo); NAPI.Vehicle.SetVehicleMod(veh, 23, pVeh.fwheels); NAPI.Vehicle.SetVehicleMod(veh, 24, pVeh.bwheels); //MOTORAD
                    NAPI.Vehicle.SetVehicleWindowTint(veh, pVeh.window); NAPI.Vehicle.SetVehicleMod(veh, 15, pVeh.suspension);

                    Database.Update(pVeh);

                    client.SetData("PersonalVehicle", veh);
                }

                return;
        }
    }
}