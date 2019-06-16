using GTANetworkAPI;
using reallife.Data;
using reallife.Db;
using reallife.Events;
using reallife.Player;
using System;

namespace reallife.Commands
{
    public class PlayerMoney : Script
    {
        [Command("money")]
        public void CMD_Money(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerInfo == null)
            {
                Console.WriteLine("Spieler Statistiken konnten nicht gefunden werden.");
                return;
            }

            client.SendNotification($"Dein Guthaben beträgt: ~g~{playerInfo.money}$");
            client.SendNotification($"Dein Bankguthaben beträgt: ~g~{playerInfo.bank}$");

            EventTriggers.Update_Money(client);
        }

        [Command("givemoney")]
        public void CMD_GiveMoney(Client client, Client player, double amount)
        {
            if (!client.HasData("ID"))
                return;

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);

            if (pInfo == null)
            {
                client.SendNotification($"Spieler {player.Name} konnte nicht gefunden werden.");
                return;
            }


            PlayerInfo otherInfo = Database.GetById<PlayerInfo>(pInfo._id);

            if (otherInfo == null)
            {
                Console.WriteLine($"{pInfo.vorname} {pInfo.nachname} besitzt keine PlayerInfo Tabelle!");
                return;
            }

            bool result = playerInfo.SubMoney(amount);

            if (!result)
            {
                client.SendNotification("~r~Dieses Geld besitzt du nicht!");
                return;
            }

            otherInfo.AddMoney(amount);

            Database.Update(otherInfo);
            Database.Update(playerInfo);

            client.SendNotification($"Du hast dem Spieler {player.Name} erfolgreich ~g~{amount}$ ~w~gegeben!");
            player.SendNotification($"Du hast ~g~{amount}~w~ von {client.Name} erhalten!");
            EventTriggers.Update_Money(client);

            Client other_player = NAPI.Player.GetPlayerFromName(player.Name);

            if (other_player == null)
                return;

            EventTriggers.Update_Money(other_player);
        }

        [Command("burnmoney")]
        public void CMD_BurnMoney(Client client, double amount)
        {
            if (!client.HasData("ID"))
                return;

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            if (playerInfo == null)
                return;

            bool result = playerInfo.SubMoney(amount);
            Database.Update(playerInfo);

            if (!result)
                return;

            client.SendNotification($"~g~Du hast {amount}$ weggeworfen!");
            EventTriggers.Update_Money(client);
        }
    }
}
