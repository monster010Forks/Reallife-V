using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using reallife.Data;
using reallife.Db;
using reallife.Player;

namespace reallife.Commands
{
    class LeaderCmds : Script
    {
        [Command("delfwarn")]
        public void CMD_DelFraktionWarn(Client client, Client player)
        {
            //Spieler Statistiken
            PlayerInfo leaderInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            //Abfrage ob man ein Leader ist
            if (!LeaderSystem.IsLeader(client))
            {
                client.SendNotification("~r~Du bist kein Leader!");
                return;
            }

            if (!LeaderSystem.Same(client, player))
            {
                client.SendNotification("~r~Ihr befindet euch nicht in der selben Fraktion!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber angeben!");
                return;
            }

            if (playerInfo.fwarn == 0)
            {
                player.SendNotification("Spieler besitzt keine Warn's!");
                return;
            }

            playerInfo.fwarn -= 1;
            playerInfo.Update();

            player.SendChatMessage("[~g~Fraktion~w~]: Eine Verwarnung wurde entfernt!");
        }

        [Command("fwarn")]
        public void CMD_FraktionWarn(Client client, Client player)
        {
            //Spieler Statistiken
            PlayerInfo leaderInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            //Abfrage ob man ein Leader ist
            if (!LeaderSystem.IsLeader(client))
            {
                client.SendNotification("~r~Du bist kein Leader!");
                return;
            }

            if (!LeaderSystem.Same(client, player))
            {
                client.SendNotification("~r~Ihr befindet euch nicht in der selben Fraktion!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber angeben!");
                return;
            }

            playerInfo.fwarn += 1;
            playerInfo.Update();

            player.SendChatMessage($"[~g~Fraktion~w~]: Du bekamst eine Verwarnung und besitzt nun ~r~{playerInfo.fwarn}~w~ Verwarnungen.");

            if (playerInfo.fwarn == 3)
            {
                player.SendChatMessage("[~g~Fraktion~w~]: Du besitzt zu viele Verwarnungen weswegen du aus der Fraktion entlassen wurdest!");
                player.SendNotification("~r~Du wurdest aus der Fraktion entlassen!");
                playerInfo.fraktion = 0;
                playerInfo.last_location = new double[] { -1167.994, -700.4285, 21.89281 };

                playerInfo.Update();

                PlayerData.Respawn(player);
            }
        }

        [Command("invite")]
        public void CMD_invite(Client client, string player)
        {
            //Spieler Statistiken
            PlayerInfo leaderInfo = PlayerHelper.GetPlayerStats(client);

            //Abfrage ob man ein Leader ist
            if (!LeaderSystem.IsLeader(client))
            {
                client.SendNotification("~r~Du bist kein Leader!");
                return;
            }

            /*if (client.Name == target.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber einladen!");
                return;
            }*/

            if (FraktionSystem.SetRank(player, leaderInfo.fleader))
            {
                client.SendNotification($"[~r~Server~w~] Spieler {player} wurde in die Fraktion eingeladen.");
                PlayerData.Respawn(client);
                return;
            }
            else
            {
                client.SendNotification($"[~r~Server~w~] Spieler {player} konnte nicht in die Fraktion eingeladen werden!");
                return;
            }

        }

        [Command("uninvite")]
        public void CMD_uninvite(Client client, Client player)
        {
            //Spieler Statistiken
            PlayerInfo leaderInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            //Abfrage ob man ein Leader ist
            if (!LeaderSystem.IsLeader(client))
            {
                client.SendNotification("~r~Du bist kein Leader!");
                return;
            }

            //Abfrage ob Leader und Spieler in der selben Fraktion sind
            if (!LeaderSystem.Same(client, player))
            {
                client.SendNotification("~r~Dieser Spieler gehört nicht zu deiner Fraktion!");
                return;
            }

            /*if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber entlassen!");
                return;
            }*/

            client.SendNotification("Spieler wurde erfolgreich aus der Fraktion rausgeworfen!");
            player.SendNotification("~r~Du wurdest aus der Fraktion entlassen!");
            playerInfo.fraktion = 0;
            playerInfo.last_location = new double[] { -1167.994, -700.4285, 21.89281 };

            playerInfo.Update();

            PlayerData.Respawn(player);
        }
    }
}
