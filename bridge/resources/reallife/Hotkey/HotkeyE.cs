using GTANetworkAPI;
using reallife.Player;
using reallife.Db;
using reallife.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Hotkey
{
    public class HotkeyE : Script
    {
        [RemoteEvent("testanzeige")]
        public void testanzeige(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            double[] money = new[] {pInfo.money};

            var dataInJson = NAPI.Util.ToJson(money);

            client.TriggerEvent("showgeldanzeige", NAPI.Util.ToJson(money));
        }
        [RemoteEvent("OpenMosMenu")]
        public void OpenMosMenu(Client client)
        {
            client.TriggerEvent("StartAutohausBrowser");
        }
        [RemoteEvent("OpenATMcode")]
        public void OpenATMcode(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo.bkonto == 0)
            {
                client.SendChatMessage("[~g~BANK~w~] Du besitzt kein Bankkonto!");
                return;
            }
            else
            {
                //client.SendChatMessage("PIN OPEN");
                //NAPI.ClientEvent.TriggerClientEvent(client, "StartbKontoLoginBrowser");
                client.TriggerEvent("StartbKontoLoginBrowser");
            }
        }
        [RemoteEvent("bKontoErst")]
        public void bKontoErst(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);
            if (pInfo.bkonto == 1)
            {
                client.SendChatMessage("[~g~BANK~w~] Du besitzt bereits ein Bankkonto!");
                // client.TriggerEvent("bKontoResult", 0);
                return;
            }
            else
            {
                //client.SendChatMessage("PIN OPEN");
                NAPI.ClientEvent.TriggerClientEvent(client, "StartbKontoBrowser");
            }
        }
        [RemoteEvent("bKontob")]
        public void bKontob(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);
            if (pInfo.bkonto == 0)
            {
                client.SendChatMessage("[~g~Bank~w~] Du besitzt kein Bankkonto!");
                return;
            }
            else
            {
                NAPI.ClientEvent.TriggerClientEvent(client, "StartPinChangeBrowser");
            }
        }
        [RemoteEvent("HotRent")]
        public void HotRent(Client client)
        {
            NAPI.ClientEvent.TriggerClientEvent(client, "OpenRentBrowser");
        }
        [RemoteEvent("RentSpawnCarRoller1")]
        public void RentSpawnCarRoller(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (client.HasData("RentVehicle"))
            {
                client.SendNotification("~y~Du besitzt bereits einen Mietvertrag!");
                client.SendChatMessage("~r~Mietfahrzeug:~w~ Nutze /unrent um deinen Mietvertrag zu kündigen!");
                return;
            }

            uint rveh = NAPI.Util.GetHashKey("faggio2");

            Vehicle veh = NAPI.Vehicle.CreateVehicle(rveh, new Vector3(-1151.06201171875, -716.578186035156, 20.6585292816162), 311.515930175781f, 0, 0);
            NAPI.Vehicle.SetVehicleNumberPlate(veh, client.Name);
            client.SetIntoVehicle(veh, -1);
            veh.Locked = true;

            client.SendChatMessage("~r~Mietfahrzeug:~w~ /lock - Abschliessen & Aufschliessen.");
            client.SendChatMessage("~r~Mietfahrzeug:~w~/motor - Motor starten/abschalten.");

            pInfo.SubMoney(150);
            Database.Update(pInfo);

            EventTriggers.Update_Money(client);

            client.SendNotification("Du hast ~g~150$~w~ für dein Mietfahrzeug gezahlt.");

            client.SetData("RentVehicle", veh);
        }
    }
}
