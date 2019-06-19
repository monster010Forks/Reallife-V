using GTANetworkAPI;
using reallife.Db;
using System;

namespace reallife.Player
{
    public class PlayerVehicles
    {
        //public int _id { get; set; }
        public int _id { get; set; }
        //public int _cid { get; set; }
        public int carslot { get; set; }
        public string carmodel { get; set; }
        public double[] last_location { get; set; } = new double[] { 0, 0, 0};
        public float last_rotation { get; set; } 
        public int Color1 { get; set; }
        public int Color2 { get; set; }
        public int spoilers { get; set; }
        public int fbumber { get; set; }
        public int rbumber { get; set; }
        public int sskirt { get; set; }
        public int exhaust { get; set; }
        public int frame { get; set; }
        public int grill { get; set; }
        public int roof { get; set; }
        public int motortuning { get; set; }
        public int brakes { get; set; }
        public int transmission { get; set; }
        public int turbo { get; set; }
        public int fwheels { get; set; }
        public int bwheels { get; set; }
        public int window { get; set; }
        public int suspension { get; set; }

        public static void CarLock(Client client)
        {
            Vehicle[] vehicles = new Vehicle[] { client.GetData("PersonalVehicle"), client.GetData("FrakVehicle"), client.GetData("RentVehicle") };
            Vehicle vehicleToLock = null;

            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle != null && client.Position.DistanceTo2D(vehicle.Position) <= 2)
                {
                    vehicleToLock = vehicle;
                }
            }

            if (vehicleToLock != null)
            {
                vehicleToLock.Locked = !vehicleToLock.Locked;

                if (!vehicleToLock.Locked)
                {
                    client.SendNotification($"~g~Das Fahrzeug wurde aufgeschlossen!");
                }
                else
                {
                    client.SendNotification($"~r~Das Fahrzeug wurde abgeschlossen!");
                }
            }
            else
            {
                client.SendNotification("Du befindest dich nicht in der nähe von einem deiner Fahrzeuge!");
            }
        }

        public static void Engine(Client client)
        {
            Vehicle personal_vehicle = client.GetData("PersonalVehicle");
            Vehicle frak_vehicle = client.GetData("FrakVehicle");
            Vehicle rent_vehicle = client.GetData("RentVehicle");

            int zahl = 0;

            if (!client.IsInVehicle)
            {
                client.SendNotification("Du befindest dich in keinem Fahrzeug!");
                return;
            }

            if (client.HasData("PersonalVehicle"))
            {
                if (client.Vehicle.GetData("ID") == client.GetData("ID"))
                {
                    zahl = 1;
                }
            }

            if (client.HasData("FrakVehicle"))
            {
                if (client.Vehicle.GetData("ID") == client.GetData("ID"))
                {
                    zahl = 2;
                }
            }

            if (client.HasData("RentVehicle"))
            {
                if (client.Vehicle.GetData("ID") == client.GetData("ID"))
                {
                    zahl = 3;
                }
            }

            switch (zahl)
            {
                case 1:
                    EngineStatus(client);
                    break;

                case 2:
                    EngineStatus(client);
                    break;

                case 3:
                    EngineStatus(client);
                    break;

                default:
                    client.SendNotification("~r~Du besitzt für dieses Fahrzeug keinen Schlüssel!");
                    break;
            }
        }

        public static void EngineStatus(Client client)
        {
            bool engine = client.Vehicle.EngineStatus;

            if (engine == false)
            {
                engine = client.Vehicle.EngineStatus = true;
                client.SendNotification("~g~Der Motor wurde gestartet!");
            }
            else
            {
                engine = client.Vehicle.EngineStatus = false;
                client.SendNotification("~r~Der Motor wurde augeschaltet");
            }
        }

        public static void GetLastCarPosition(Client client)
        {
            Vehicle personal_vehicle = client.GetData("PersonalVehicle");

            int client_id = client.GetData("ID");

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);


            if (pVeh == null)
                return;

            if (pVeh._id == client_id)
            {
                Vector3 pVehSpawn = new Vector3(pVeh.last_location[0], pVeh.last_location[1], pVeh.last_location[2]);

                //client.SendChatMessage("~g~Du besitzt ein Fahrzeug!");
                uint pVehHash = NAPI.Util.GetHashKey(pVeh.carmodel);
                Vehicle veh = NAPI.Vehicle.CreateVehicle(pVehHash, pVehSpawn, pVeh.last_rotation, 0, 0);
                //NAPI.Vehicle.SetVehicleEngineStatus(veh, false);
                NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);

                NAPI.Vehicle.SetVehicleLocked(veh, true);
                NAPI.Vehicle.SetVehicleEngineStatus(veh, false);
                NAPI.Vehicle.SetVehicleExtra(veh, 0, true);

                //Tunes
                LoadTunes(client, veh);

                client.SetData("Engine", false);
                client.SetData("PersonalVehicle", veh);
                veh.SetData("ID", client_id);
            }
        }
        public static void LoadTunes(Client client, Vehicle vehicle)
        {
            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);
            NAPI.Vehicle.SetVehiclePrimaryColor(vehicle, pVeh.Color1);
            NAPI.Vehicle.SetVehicleSecondaryColor(vehicle, pVeh.Color2);
            NAPI.Vehicle.SetVehicleMod(vehicle, 0, pVeh.spoilers);
            NAPI.Vehicle.SetVehicleMod(vehicle, 1, pVeh.fbumber);
            NAPI.Vehicle.SetVehicleMod(vehicle, 2, pVeh.rbumber);
            NAPI.Vehicle.SetVehicleMod(vehicle, 3, pVeh.sskirt);
            NAPI.Vehicle.SetVehicleMod(vehicle, 4, pVeh.exhaust);
            NAPI.Vehicle.SetVehicleMod(vehicle, 5, pVeh.frame);
            NAPI.Vehicle.SetVehicleMod(vehicle, 6, pVeh.grill);
            NAPI.Vehicle.SetVehicleMod(vehicle, 10, pVeh.roof);
            NAPI.Vehicle.SetVehicleMod(vehicle, 11, pVeh.motortuning);
            NAPI.Vehicle.SetVehicleMod(vehicle, 12, pVeh.brakes);
            NAPI.Vehicle.SetVehicleMod(vehicle, 13, pVeh.transmission);
            NAPI.Vehicle.SetVehicleMod(vehicle, 18, pVeh.turbo);
            NAPI.Vehicle.SetVehicleMod(vehicle, 23, pVeh.fwheels);
            NAPI.Vehicle.SetVehicleMod(vehicle, 24, pVeh.bwheels); //MOTORAD
            NAPI.Vehicle.SetVehicleWindowTint(vehicle, pVeh.window);
            NAPI.Vehicle.SetVehicleMod(vehicle, 15, pVeh.suspension);
        }
    }
}
