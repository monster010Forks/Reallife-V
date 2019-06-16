using GTANetworkAPI;
using reallife.Db;
using reallife.Events;

namespace reallife.Player
{
    public static class DisconnectHandler
    {

        public static void DisconnectFinish(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            Vehicle pVehicle = client.GetData("PersonalVehicle");
            Vehicle RentVehicle = client.GetData("RentVehicle");
            Vehicle FrakVeh = client.GetData("FrakVehicle");

            if (client.HasData("ID") == false)
                return;

            if (client.HasData("RentVehicle"))
            {
                NAPI.Task.Run(() =>
                {
                    RentVehicle.Delete();
                }, delayTime: 1000);
            }

            if (client.HasData("PersonalVehicle"))
            {
                NAPI.Task.Run(() =>
                {
                    pVehicle.Delete();
                }, delayTime: 1000);
            }

            if (client.HasData("FrakVehicle"))
            {
                NAPI.Task.Run(() =>
                {
                    FrakVeh.Delete();
                }, delayTime: 1000);
            }

            if (client.HasData("cuff"))
            {
                NAPI.Task.Run(() =>
                {
                    playerInfo.jail = 1;
                }, delayTime: 1000);
            }

            Database.Update(playerInfo);
        }
    }
}
