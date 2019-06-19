using GTANetworkAPI;
using reallife.Data;
using reallife.Db;
using reallife.Events;
using reallife.Fraktion;
using reallife.Player;
using reallife.Chat;

namespace reallife.Commands
{
    class LSPDCmds : Script
    {
        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        }

        [Command("akte")]
        public void CMD_Akte(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("Du hast dazu keine Berechtigung!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du bist nicht im Dienst!");
                return;
            }

            double vermoegen = playerInfo.money + playerInfo.bank;

            if(client.Position.DistanceTo2D(new Vector3(442.9581, -975.1335, 30.68961)) <= 2)
            {
                client.SendNotification($"[~b~Akte~w~]: {player.Name}");
                client.SendNotification($"[~b~WantedLevel~w~]: {playerInfo.wantedlevel}");
                client.SendNotification($"[~b~Vermögen~w~]: {vermoegen}~g~$");
                client.SendNotification($"[~b~Fraktion~w~]: {PlayerInfo.WhichFrak(player)}");
            } else
            {
                client.SendNotification("Du befindest dich nicht in der Nähe der Akten.");
            }

        }

        [Command("takeweapons")]
        public void CMD_TakeWeapons(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("Du hast dazu keine Berechtigung!");
                return;
            }

            if (!player.HasData("onduty"))
            {
                client.SendNotification("~r~Du bist nicht im Dienst!");
                return;
            }

            if (client.Position.DistanceTo2D(player.Position) < 5)
            {
                player.RemoveAllWeapons();
                client.SendNotification($"[~b~LSPD~w~]: Die Waffen von {player.Name} wurden entfernt!");
                player.SendNotification($"[~b~LSPD~w~]: Deine Waffen wurden von {client.Name} entfernt!");
            } else
            {
                client.SendNotification("Spieler ist nicht in Reichweite!");
            }
        }

        [Command("einsperren")]
        public void CMD_Einsperren(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("Du hast dazu keine Berechtigung!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du bist nicht im Dienst!");
                return;
            }

            if (!player.HasData("cuff") == true)
            {
                client.SendNotification("Spieler ist nicht in Handschellen!");
                return;
            }

            if (playerInfo.wantedlevel == 0)
            {
                client.SendNotification("Dieser Spieler wird nicht gesucht!");
                return;
            }

            if (client.Position.DistanceTo2D(new Vector3(461.9194, -989.1077, 24.91486)) <= 3)
            {
                if (client.Position.DistanceTo2D(player.Position) <= 5)
                {
                    playerInfo.jail = 1;
                    playerInfo.cuff = 0;

                    playerInfo.Update();

                    PlayerData.Respawn(player);

                    client.SendNotification($"[~b~LSPD~w~]: Du hast ~y~{player.Name}~w~ in das Gefägnis gesteckt!");
                    player.SendNotification($"[~b~LSPD~w~]: {client.Name}~w~ hat dich in das Gefängnis gesteckt!");
                } else
                {
                    client.SendNotification("Dieser Spieler befindet sich nicht in deiner Nähe!");
                }

            } else
            {
                client.SendNotification("Du kannst die Person hier nicht einsperren!");
            }

        }

        [Command("entlassen")]
        public void CMD_Entlassen(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("Du hast dazu keine Berechtigung!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du bist nicht im Dienst!");
                return;
            }

            if (client.Position.DistanceTo2D(new Vector3(461.9194, -989.1077, 24.91486)) <= 3)
            {
                    playerInfo.jail = 0;
                    playerInfo.jailtime = 0;
                    playerInfo.temp_location = null;
                    playerInfo.Update();

                    PlayerData.Respawn(player);
                    client.SendNotification($"[~b~LSPD~w~]: Du hast ~y~{player.Name}~w~ aus dem Gefägnis gelassen!");
                    player.SendNotification($"[~b~LSPD~w~]: {client.Name}~w~ hat dich aus dem Gefägnis gelassen!");
            }
            else
            {
                client.SendNotification("Du kannst die Person hier nicht entlassen!");
            }
        }

        [Command("cuff")] 
        public void CMD_Cuff(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("Du hast dazu keine Berechtigung!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du bist nicht im Dienst!");
                return;
            }

            if (player.HasData("cuff"))
            {
                client.SendNotification("Dieser Spieler besitzt bereits Handschellen!");
                return;
            }

            if (playerInfo.jail == 1)
            {
                client.SendNotification("Dieser Spieler kann im Gefängnis keine Handschellen tragen!");
                return;
            }

            if(player.Position.DistanceTo2D(client.Position) < 5)
            {
                NAPI.Player.PlayPlayerAnimation(player, (int)(AnimationFlags.Loop | AnimationFlags.OnlyAnimateUpperBody | AnimationFlags.AllowPlayerControl), "mp_arresting", "idle");
                client.SendNotification("[~b~LSPD~w~]:~w~ Du hast diese Person festgenommen: " + player.Name);
                LSPD.cuff(player);
            } else
            {
                client.SendNotification("Spieler ist nicht in Reichweite!");
            }
        }

        [Command("uncuff")]
        public void CMD_UnCuff(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("Du hast dazu keine Berechtigung!");
                return;
            }

            if (!player.HasData("onduty"))
            {
                client.SendNotification("~r~Du bist nicht im Dienst!");
                return;
            }

            if (!player.HasData("cuff"))
            {
                client.SendNotification("[~b~LSPD~w~]: Dieser Spieler besitzt keine Handschellen!");
                return;
            }

            if (player.Position.DistanceTo2D(client.Position) < 5)
            {
                NAPI.Player.StopPlayerAnimation(player);
                client.SendNotification("[~b~LSPD~w~]: Du hast diese Person freigelassen: " + player.Name);
                LSPD.uncuff(player);
            } else
            {
                client.SendNotification("Spieler ist nicht in Reichweite!");
            }
        }

        [Command("mp")]
        public void CMD_MegaPhone(Client client, string message)
        {

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("Du hast keinen Zugriff auf diesen Befehl!");
                return;
            }

            //Überprüfen ob ein Spieler innerhalb der Reichweite (25) ist.
            Client[] clients = NAPI.Pools.GetAllPlayers().FindAll(x => x.Position.DistanceTo2D(client.Position) <= 25).ToArray();

            for (int i = 0; i < clients.Length; i++)
            {
                if (!clients[i].Exists)
                    continue;

                clients[i].SendChatMessage($"[~b~Megaphone~w~] {client.Name}: {message}");
            }
        }

        [Command("removewanted")]
        public void CMD_RemoveWanted(Client client, Client player, int wanteds)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du gehörst nicht zur LSPD!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du befindest dich nicht im Dienst!");
                return;
            }

            if (player == null)
            {
                client.SendNotification("Spieler wurde nicht gefunden!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber angeben!");
                return;
            }

            if (pInfo.wantedlevel - wanteds <= 0)
            {
                client.SendChatMessage("Du kannst dem Spieler nicht weniger als 0 Wanteds geben!");
                return;
            }

            pInfo.wantedlevel -= wanteds;
            client.SendNotification($"[~b~LSPD~w~]: Du hast ~r~{player.Name}~w~ erfolgreich ~r~{wanteds} Wanteds~w~ abgezogen!");
            player.SendChatMessage($"[~b~LSPD~w~]: ~b~{client.Name}~w~ hat dir ~r~{wanteds} Wanteds~w~ abgezogen!");
            pInfo.Update();
            EventTriggers.Update_Wanteds(player);
        }

        [Command("delakte")]
        public void CMD_DelAkte(Client client, Client player)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du gehörst nicht zur LSPD!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du befindest dich nicht im Dienst!");
                return;
            }

            if (player == null)
            {
                client.SendNotification("Spieler wurde nicht gefunden!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber angeben!");
                return;
            }

            if (pInfo.wantedlevel > 4)
            {
                client.SendNotification("Dieser Spieler hat zu viele Wanteds!");
                return;
            }

            pInfo.wantedlevel = 0;
            client.SendNotification($"[~b~LSPD~w~]: Du hast die Akte von {player.Name} ~r~gelöscht~w~!");
            player.SendChatMessage($"[~b~LSPD~w~]: ~b~{client.Name}~w~ hat deine Akte ~r~gelöscht~w~!");
            pInfo.Update();
            EventTriggers.Update_Wanteds(player);
        }

        [Command("setwanted")]
        public void CMD_SetWanted(Client client, Client player, int wanteds)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du gehörst nicht zur LSPD!");
                return;
            }

            if (!client.HasData("onduty"))
            {
                client.SendNotification("~r~Du befindest dich nicht im Dienst!");
                return;
            }

            if (player == null)
            {
                client.SendNotification("Spieler wurde nicht gefunden!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber angeben!");
                return;
            }

            if (wanteds > 6)
            {
                client.SendChatMessage("Du kannst nicht mehr als 6 Wanteds verteilen!");
                return;
            }

            pInfo.wantedlevel += wanteds;
            client.SendNotification($"[~b~LSPD~w~]: Du hast ~r~{player.Name}~w~ erfolgreich ~r~{wanteds} Wanteds~w~ gegeben!");
            player.SendChatMessage($"[~b~LSPD~w~]: ~b~{client.Name}~w~ hat dir ~r~{wanteds} Wanteds~w~ gegeben!");
            pInfo.Update();
            EventTriggers.Update_Wanteds(player);
        }

        /*[Command("lspdcar")]
        public void CMD_LSPDCar(Client client)
        {

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (!FraktionSystem.HasRank(client, 1))
            {
                client.SendNotification("~r~Du gehörst nicht zur LSPD!");
                return;
            }

            if (client.Position.DistanceTo2D(new Vector3(458.2729, -1008.082, 28.28012)) <= 5)
            {
                if (client.HasData("FrakVehicle"))
                {
                    client.SendNotification("~g~[POLICE]:~w~ Du besitzt bereits ein LSPD Fahrzeug!");
                    return;
                }

                if (client.HasData("onduty"))
                {
                    uint hash = NAPI.Util.GetHashKey("police");
                    Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(447.323150634766, -996.606872558594, 25.3755207061768), 179.479125976563f, 111, 0);
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
