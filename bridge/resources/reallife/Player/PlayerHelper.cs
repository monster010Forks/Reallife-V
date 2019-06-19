using GTANetworkAPI;
using reallife.Db;

namespace reallife.Player
{
    class PlayerHelper
    {
        public static Players GetPlayer(Client client)
        {
            if (!client.HasData("ID"))
                return null;

            int pInfo = client.GetData("ID");
            Players p = Database.GetById<Players>(pInfo);
            return p;
        }

        public static PlayerInfo GetPlayerStats(Client client)
        {
            if (!client.HasData("ID"))
                return null;

            int pInfo = client.GetData("ID");
            PlayerInfo playerInfo = Database.GetById<PlayerInfo>(pInfo);
            return playerInfo;
        }

        public static PlayerClothes GetPlayerClothes(Client client)
        {
            if (!client.HasData("ID"))
                return null;

            int pInfo = client.GetData("ID");
            PlayerClothes pClothes = Database.GetById<PlayerClothes>(pInfo);
            return pClothes;
        }

        public static BanLog BanLogs(Client client)
        {
            if (!client.HasData("ID"))
                return null;

            int bpid = client.GetData("ID");
            BanLog Banlog = Database.GetById<BanLog>(bpid);
            return Banlog;
        }

        public static KickLog KickLogs(Client client)
        {
            if (!client.HasData("ID"))
                return null;

            int kpid = client.GetData("ID");
            KickLog Kicklog = Database.GetById<KickLog>(kpid);
            return Kicklog;
        }

        public static PlayerVehicles GetpVehiclesStats(Client client)
        {
            if (!client.HasData("ID"))
                return null;

            int pInfo = client.GetData("ID");
            PlayerVehicles pVeh = Database.GetById<PlayerVehicles>(pInfo);
            return pVeh;
        }

        public static Client GetClientByName(string name)
        {
            Client client = null;

            foreach (Client client_itr in NAPI.Pools.GetAllPlayers())
            {
                if (client.Name.ToLower() == name.ToLower())
                {
                    return client_itr;
                }
            }

            return client;
        }
    }
}
