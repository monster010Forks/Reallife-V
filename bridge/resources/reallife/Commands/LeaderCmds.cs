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

        [Command("invite")]
        public void CMD_invite(Client client, Client player)
        {
            //Spieler Statistiken
            PlayerInfo leaderInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            //Abfrage ob man ein Leader ist
            if (!LeaderSystem.IsLeader(client, 1))
            {
                client.SendNotification("~r~Du bist kein Leader!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber einladen!");
                return;
            }

            if (LeaderSystem.HasRank(client, 1))
            {
                client.SendNotification("Spieler wurde erfolgreich zur Fraktion eingeladen!");
                player.SendNotification("Du wurdest zur LSPD eingeladen!");
                playerInfo.last_location = new double[] { 447.9005, -973.0226, 30.68961 };
                playerInfo.fraktion = 1;
                playerInfo.Update();

                PlayerData.Respawn(player);
            }
            else if (LeaderSystem.HasRank(client, 2))
            {
                client.SendNotification("Spieler wurde erfolgreich zur Fraktion eingeladen!");
                player.SendNotification("Du wurdest zur SARU eingeladen!");
                playerInfo.last_location = new double[] { 1151.196, -1529.605, 35.36937 };
                playerInfo.fraktion = 2;
                playerInfo.Update();

                PlayerData.Respawn(player);
            }
            else
            {
                client.SendNotification("~r~Etwas ist schiefgelaufen!");
            }
        }

        [Command("uninvite")]
        public void CMD_uninvite(Client client, Client player)
        {
            //Spieler Statistiken
            PlayerInfo leaderInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            //Abfrage ob man ein Leader ist
            if (!LeaderSystem.IsLeader(client, 1))
            {
                client.SendNotification("~r~Du bist kein Leader!");
                return;
            }

            if (client.Name == player.Name)
            {
                client.SendNotification("~r~Du kannst dich nicht selber entlassen!");
                return;
            }

            //Abfrage ob Leader und Spieler in der selben Fraktion sind
            if (LeaderSystem.Same(client, player))
                {
                    client.SendNotification("Spieler wurde erfolgreich aus der Fraktion rausgeworfen!");
                    player.SendNotification("~r~Du wurdest aus der Fraktion entlassen!");
                    playerInfo.fraktion = 0;
                    playerInfo.last_location = new double[] { -1167.994, -700.4285, 21.89281 };

                    Database.Update(playerInfo);

                    PlayerData.Respawn(player);
                } else
                {
                    client.SendNotification("~r~Dieser Spieler gehört nicht zu deiner Fraktion!");
                }
        }
    }
}
