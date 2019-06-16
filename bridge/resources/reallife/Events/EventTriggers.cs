using System;
using GTANetworkAPI;
using System.Collections.Generic;
using System.Text;

using reallife.Player;

namespace reallife.Events
{
    public static class EventTriggers
    {
        public static void Update_Money(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo == null)
                return;

            NAPI.ClientEvent.TriggerClientEvent(client, "update_money", Convert.ToSingle(pInfo.money));
        }

        public static void Update_Wanteds(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo == null)
                return;

            NAPI.ClientEvent.TriggerClientEvent(client, "update_wanteds", Convert.ToSingle(pInfo.wantedlevel));
        }

        public static void Update_Bank(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo == null)
                return;

            NAPI.ClientEvent.TriggerClientEvent(client, "update_bank", Convert.ToSingle(pInfo.bank));
        }

        public static void Update_Version(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo == null)
                return;

            NAPI.ClientEvent.TriggerClientEvent(client, "update_bank", Convert.ToSingle(pInfo.bank));
        }
    }
}
