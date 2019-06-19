using GTANetworkAPI;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Data
{
    class FraktionSystem
    {
        public static int GetRank(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerInfo == null)
                return -1;

            return playerInfo.fraktion;
        }

        public static bool HasRank(Client client, int rank)
        {
            int currentRank = GetRank(client);

            if (currentRank < rank || currentRank > rank)
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


            if (rank == 1)
            {
                tarInfo.last_location = new double[] { 447.9005, -973.0226, 30.68961 };
                tarInfo.Update();
                target.SendChatMessage("[~y~Fraktion~w~]: Du wurdest in die Fraktion ~b~LSPD~w~ eingeladen!");
            }

            if (rank == 2)
            {
                tarInfo.last_location = new double[] { 1151.196, -1529.605, 35.36937 };
                tarInfo.Update();
                target.SendChatMessage("[~y~Fraktion~w~]: Du wurdest in die Fraktion ~b~SARU~w~ eingeladen!");
            }

            if (rank == 3)
            {
                tarInfo.last_location = new double[] { 85.90534, -1956.926, 20.74745 };
                tarInfo.Update();
                target.SendChatMessage("[~y~Fraktion~w~]: Du wurdest in die Fraktion ~b~Grove Street~w~ eingeladen!");
            }

            tarInfo.fraktion = rank;
            tarInfo.Update();
            PlayerData.Respawn(target);
            return true;
        }
    }
}
