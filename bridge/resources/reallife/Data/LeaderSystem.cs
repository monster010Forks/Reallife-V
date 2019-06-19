using GTANetworkAPI;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Data
{
    class LeaderSystem
    {
        static readonly string[] leaderText = new string[] {
            "als Leader der Fraktion entlassen.",
            "zum Leader der Fraktion LSPD ernannt.",
            "zum Leader der Fraktion SARU ernannt.",
            "zum Leader der Fraktion Grove Street ernannt." };

        public static string GetSetLeaderText(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            return leaderText[(playerInfo.fraktion > leaderText.Length) ? 0 : playerInfo.fraktion];
        }

        public static int GetRank(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerInfo == null)
                return -1;

            return playerInfo.fleader;
        }

        public static bool HasRank(Client client, int rank)
        {
            int currentRank = GetRank(client);

            if (currentRank < rank || currentRank > rank)
                return false;

            return true;
        }

        public static bool IsLeader(Client client)
        {
            int rank = 1;

            int currentRank = GetRank(client);

            if (currentRank < rank)
                return false;

            return true;
        }

        public static bool Same(Client client, Client player)
        {
            PlayerInfo clientInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(player);

            //if (clientInfo.fleader < playerInfo.fraktion || clientInfo.fleader > playerInfo.fraktion)
            if (clientInfo.fleader != playerInfo.fraktion)
                return false;

            return true;
        }

        public static bool SetRank(string playerName, int rank)
        {

            Client target = NAPI.Player.GetPlayerFromName(playerName);

            if (target == null)
                return false;

            PlayerInfo tarInfo = PlayerHelper.GetPlayerStats(target);

            if (tarInfo == null)
                return false;

            if (rank == 0)
            {
                tarInfo.last_location = new double[] { -1167.994, -700.4285, 21.89281 };
                tarInfo.Update();
                target.SendChatMessage($"Du wurdest als Leader entlassen und bist absofort Arbeitslos!");
            }

            if (rank == 1)
            {
                tarInfo.last_location = new double[] { 447.9005, -973.0226, 30.68961 };
                tarInfo.Update();
                target.SendChatMessage("Du wurdest zum ~y~Leader~w~ der ~b~LSPD~w~ ernannt!");
            }

            if (rank == 2)
            {
                tarInfo.last_location = new double[] { 1151.196, -1529.605, 35.36937 };
                tarInfo.Update();
                target.SendChatMessage("Du wurdest zum ~y~Leader~w~ der ~b~SARU~w~ ernannt!");
            }

            if (rank == 3)
            {
                tarInfo.last_location = new double[] { 85.90534, -1956.926, 20.74745 };
                tarInfo.Update();
                target.SendChatMessage("Du wurdest zum ~y~Leader~w~ der ~g~Grove Street~w~ ernannt!");
            }

            tarInfo.fleader = rank;
            tarInfo.fraktion = rank;
            tarInfo.Update();
            PlayerData.Respawn(target);
            return true;
        }
    }
}
