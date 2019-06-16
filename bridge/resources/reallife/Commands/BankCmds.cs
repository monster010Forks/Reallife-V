using System;
using GTANetworkAPI;
using reallife.Db;
using reallife.Player;

/*X: 234,8648 Y: 217,0689 Z: 106,2867 | BANKBLIP |
X: 243,1547 Y: 224,7177 Z: 106,2868 | kontobeantragen |
X: 246,6217 Y: 223,5182 Z: 106,2867 | kontopinan |*/

namespace reallife.Commands
{
    public class BankCmds : Script
    {
        [Command("changepin")]
        public void CMD_changepin(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);
            if(client.Position.DistanceTo2D(new Vector3(246.6217, 223.5182, 106.2867)) <= 4)
            {
                if(pInfo.bkonto == 0)
                {
                    client.SendChatMessage("[~g~Bank~w~] Du besitzt kein Bankkonto!");
                    return;
                }
                else
                {
                    NAPI.ClientEvent.TriggerClientEvent(client, "StartPinChangeBrowser");
                }
            }
            else
            {
                client.SendNotification("~r~Du bist in keiner Bank!");
            }
        }
        [Command("bkonto")]
        public void CMD_bkonto(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);
            if (client.Position.DistanceTo2D(new Vector3(243.1547, 224.7177, 106.2868)) <= 4)
            {
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
            else
            {
                client.SendNotification("~r~Du bist bei keiner Bank!");
            }
        }
        [Command("bank")]
        public void CMD_bank(Client client)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo.bkonto == 0)
            {
                client.SendChatMessage("[~g~BANK~w~] Du besitzt kein Bankkonto!");
                return;
            }
            else
            {
                double handgeld = pInfo.money;

                NAPI.ClientEvent.TriggerClientEvent(client, "StartbKontoLoginBrowser", handgeld);
            }
        }
    }
}
