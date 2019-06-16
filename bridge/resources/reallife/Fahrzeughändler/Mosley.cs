using GTANetworkAPI;
using reallife.Db;
using reallife.Events;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;


namespace reallife.Fahrzeughändler
{
    public class Mosley : Script //244.6719
    {
        public Mosley()
        {
            uint mVeh1Hash = NAPI.Util.GetHashKey("issi2");
            uint mVeh2Hash = NAPI.Util.GetHashKey("rhapsody");

            Vector3 mVeh1Loc = new Vector3(-52.03815, -1677.032, 28.93776);
            Vector3 mVeh2Loc = new Vector3(-54.7171249389648, -1680.2490234375, 29.1529884338379);

            Vehicle mVeh1 = NAPI.Vehicle.CreateVehicle(mVeh1Hash, mVeh1Loc, 244.6719f, 0, 0);
            NAPI.TextLabel.CreateTextLabel("Fahrzeug: Issi\nMax. Km/h: 155\nPreis: ~g~12500$", mVeh1Loc, 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.Vehicle.SetVehicleEngineStatus(mVeh1, false);
            NAPI.Vehicle.SetVehicleLocked(mVeh1, true);
            NAPI.Vehicle.SetVehicleNumberPlate(mVeh1, "Mosley");

            Vehicle mVeh2 = NAPI.Vehicle.CreateVehicle(mVeh2Hash, mVeh2Loc, 244.6719f, 0, 0);
            NAPI.TextLabel.CreateTextLabel("Fahrzeug: Rhapsody\nMax. Km/h: 150\nPreis: ~g~10250$", mVeh2Loc, 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.Vehicle.SetVehicleEngineStatus(mVeh2, false);
            NAPI.Vehicle.SetVehicleLocked(mVeh2, true);
            NAPI.Vehicle.SetVehicleNumberPlate(mVeh2, "Mosley");

            mVeh1.SetData("mVeh1Vehicle", mVeh1);
            mVeh2.SetData("mVeh2Vehicle", mVeh2);
            
            NAPI.TextLabel.CreateTextLabel("Mosley Auto Haus\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(-40.82757, -1674.866, 29.5), 12, 1f, 4, new Color(255, 255, 255, 255));
            //Marker autohaus = NAPI.Marker.CreateMarker(30, new Vector3(-40.82757, -1674.866, 29.5), new Vector3(), new Vector3(0, 0, 0), 1, new Color(16, 78, 139, 100));
        }
        [RemoteEvent("OnPlayerBuyVehicle2")]
        public void OnPlayerBuyVehicle2(Client client)
        {
            int client_id = client.GetData("ID");

            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);
            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);

            if (client.HasData("PersonalVehicle"))
            {
                client.SendChatMessage("Du besitzt bereits zu viele Fahrzeuge!");
                return;
            }

            if (pInfo.money >= 12500)
            {
                uint hash = NAPI.Util.GetHashKey("rhapsody");
                Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(-21.45006, -1676.862, 29.16188), 108.823f, 0, 0);
                NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                client.SetIntoVehicle(veh, -1);

                veh.Locked = true;
                pVeh = new PlayerVehicles();
                pVeh._id = client_id;
                pVeh.carslot += 1;
                pVeh.carmodel = "rhapsody";
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

                client.SetData("PersonalVehicle", veh);

                pInfo.SubMoney(10250);
                Database.Update(pInfo);
                Database.Upsert(pVeh);

                EventTriggers.Update_Money(client);
            }
            else
            {
                client.SendChatMessage("[~y~Autohaus~w~] Du hast nicht genügend Bargeld bei dir!");
            }
        }

        [RemoteEvent("OnPlayerBuyVehicle1")]
        public void OnPlayerBuyVehicle1(Client client)
        {
            int client_id = client.GetData("ID");

            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);
            PlayerVehicles pVeh = PlayerHelper.GetpVehiclesStats(client);

            if (client.HasData("PersonalVehicle"))
            {
                client.SendChatMessage("Du besitzt bereits zu viele Fahrzeuge!");
                return;
            }

            if (pInfo.money >= 12500)
            {
                uint hash = NAPI.Util.GetHashKey("issi2");
                Vehicle veh = NAPI.Vehicle.CreateVehicle(hash, new Vector3(-21.45006, -1676.862, 29.16188), 108.823f, 0, 0);
                NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
                client.SetIntoVehicle(veh, -1);

                veh.Locked = true;
                pVeh = new PlayerVehicles();
                pVeh._id = client_id;
                pVeh.carslot += 1;
                pVeh.carmodel = "issi2";
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

                client.SetData("PersonalVehicle", veh);

                pInfo.SubMoney(12500);
                Database.Update(pInfo);
                Database.Upsert(pVeh);

                EventTriggers.Update_Money(client);
            } else
            {
                client.SendChatMessage("[~y~Autohaus~w~] Du hast nicht genügend Bargeld bei dir!");
            }
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seatID)
        {
        }

    }
}
