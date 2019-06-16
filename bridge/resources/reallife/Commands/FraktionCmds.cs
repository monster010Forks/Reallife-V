using GTANetworkAPI;
using reallife.Data;
using reallife.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Commands
{
    class FraktionCmds : Script
    {

        [Command("duty")]
        public void CMD_duty(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (client.Position.DistanceTo2D(new Vector3(452.2726, -980.15, 30.68961)) <= 0.8)
            {
                if (FraktionSystem.HasRank(client, 1))
                {
                    if (client.HasData("onduty"))
                    {
                        PlayerData.OffDuty(client);
                    }
                    else
                    {
                        PlayerData.LSPDDuty(client);
                    }
                }
                else
                {
                    client.SendNotification("~r~Du gehörst nicht zur LSPD!");
                }
            }
            else if (client.Position.DistanceTo2D(new Vector3(1207.794, -1487.555, 34.84264)) <= 0.8)
            {
                if (FraktionSystem.HasRank(client, 2))
                {
                    if (client.HasData("onduty") || client.HasData("fonduty"))
                    {
                        PlayerData.OffDuty(client);
                    }
                    else
                    {
                        PlayerData.SARUDuty(client);
                    }
                }
                else
                {
                    client.SendNotification("~r~Du gehörst nicht zur SARU!");
                }
            }
            else
            {
                client.SendNotification("Du bist nicht in der nähe von einem duty!");
            }
        }
    }
}
