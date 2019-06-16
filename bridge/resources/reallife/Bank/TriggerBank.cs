using GTANetworkAPI;
using reallife.Player;
using reallife.Db;
using System;
using System.Collections.Generic;
using System.Text;
using reallife.Events;

namespace reallife.Bank
{
    public class TriggerBank : Script
    {
  //-------------------[Bankkonto Change Pin]-------------------//
        [RemoteEvent("test")]
        public void test(Client client, int handgeld)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);
            handgeld = 1000;
        }
        [RemoteEvent("OnPlayerPinChange")]
        public void OnPlayerPinChange(Client client, int opin, int npin, int npinre)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if(pInfo.bkontopin != opin)
            {
                client.SendChatMessage("[~g~Bank~w~] Dein Alter Pin stimmt nicht!");
                client.TriggerEvent("bChangepinResult", 0);
                return;
            }
            else if(npin != npinre)
            {
                client.SendChatMessage("[~g~Bank~w~] Dein Neuer Pin stimmt nicht über ein!");
                client.TriggerEvent("bChangepinResult");
                return;
            }
            else
            {
                client.SendChatMessage($"[~g~Bank~w~] Du hast dein Pin erfolgreich geändert zu {npin}");
                pInfo.bkontopin = npin;
                client.TriggerEvent("bChangepinResult", 1);
                Database.Update(pInfo);
            }
        }
//-------------------[Bankkonto Login]-------------------//
        [RemoteEvent("OnplayerbKontoLogin")]
        public void OnplayerbKontoLogin(Client client, int pin)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if(pInfo.bkontopin != pin)
            {
                client.SendChatMessage("[~g~Bank~w~] Falscher Pin, bitte gib in erneut ein!");
                client.TriggerEvent("bKontoLoginResult", 0);
                return;
            }
            else
            {
                client.SendChatMessage("[~g~Bank~w~] Du hast dich erfolgreich eingeloggt!");
                NAPI.ClientEvent.TriggerClientEvent(client, "StartBankBrowser");
                client.TriggerEvent("bKontoLoginResult", 1);
            }
        }
//-------------------[Bankkonto Erstellung]-------------------//
        [RemoteEvent("OnPlayerbKonto")]
        public void OnPlayerbKonto(Client client, int pin, int repin)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pin != repin)
            {
                client.SendChatMessage("Dein Pin stimmt nicht überein!");
                client.TriggerEvent("bKontoResult", 0);
                return;
            }
            else
            {
                client.SendChatMessage($"[~g~Bank~w~] Dein Pin lautet {pin}");
                client.SendChatMessage($"[~g~Bank~w~] Du kannst deinen Pin jederzeit in einer Bank ändern!");
                pInfo.bkontopin += pin;
                pInfo.bkonto += 1;

                client.TriggerEvent("bKontoResult", 1);
                Database.Update(pInfo);
            }
        }
//-------------------[Bankkonto Einzahlung/Auszahlung/Überweisung]-------------------//
        [RemoteEvent("OnPlayerUberweisungAttempt")]
        public void OnPlayerUberweisungAttempt(Client client, string name, int summe)
        {
            if (!client.HasData("ID"))
                return;

            Client player = NAPI.Pools.GetAllPlayers().Find(x => x.Name == name);

            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(player);
            PlayerInfo otherInfo = Database.GetById<PlayerInfo>(pInfo._id);

            string spielername = pInfo.vorname + pInfo.nachname;

            /*if (name)
            {
                client.SendChatMessage("[~g~BANK~w~] Diese Person existiert nicht!");
                client.TriggerEvent("BankResult", 0);
                return;
            }*/

            if(spielername == null)
            {
                client.SendChatMessage("[~g~BANK~w~] Diese Person existiert nicht!");
                client.TriggerEvent("BankResult", 0);
                return;
            }

            if (summe <= 0)
            {
                client.SendChatMessage("[~g~BANK~w~] Dein Betrag ist zu klein!");
                client.TriggerEvent("BankResult", 0);
                return;
            }
            else if(playerInfo.bank < summe)
            {
                client.SendChatMessage("[~g~BANK~w~] Dein Guthaben reicht nicht aus!");
                client.TriggerEvent("BankResult", 0);
                return;
            }
            if(client.Name == player.Name)
            {
                client.SendChatMessage($"[~g~BANK~w~] Du kannst dir nicht selber Geld Überweisen!");
                client.TriggerEvent("BankResult", 0);
                return;
            }
            else
            {
                client.SendChatMessage($"[~g~BANK~w~] Du hast {otherInfo.vorname}{otherInfo.nachname} ~g~{summe}$~w~ überwiesen!");
                player.SendChatMessage($"[~g~BANK~w~] {playerInfo.vorname}{playerInfo.nachname} hat dir ~g~{summe}$~w~ überwiesen.");

                playerInfo.bank -= summe;
                otherInfo.bank += summe;

                Database.Update(playerInfo);
                EventTriggers.Update_Bank(client);

                Database.Update(otherInfo);
                EventTriggers.Update_Bank(player);
            }
        }
        [RemoteEvent("OnPlayerAuszahlung")]
        public void OnPlayerAuszahlung(Client client, int summe)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo.bank < summe)
            {
                client.SendChatMessage("~r~Du hast nicht genung Geld auf der Bank!");
                client.TriggerEvent("BankResult", 0);
                return;
            }
            else
            {
                pInfo.bank -= summe;
                pInfo.money += summe;
                client.SendChatMessage($"~w~Du hast ~g~${summe} ~w~von deinem Konto abgehoben");
                client.SendChatMessage($"~w~Neuer Kontostand: ~g~${pInfo.bank}~w~ | Bargeld: ~g~${pInfo.money}");
                client.TriggerEvent("BankResult", 1);
                Database.Upsert(pInfo);

                EventTriggers.Update_Money(client);
                EventTriggers.Update_Bank(client);
            }
        }
        [RemoteEvent("OnPlayerEinzahlung")]
        public void OnPlayerEinzahlung(Client client, int summe)
        {
            PlayerInfo pInfo = PlayerHelper.GetPlayerStats(client);

            if (pInfo.money < summe)
            {
                client.SendChatMessage("~r~Du hast nicht genung Bargeld!");
                client.TriggerEvent("BankResult", 0);
                return;
            }
            else
            {
                pInfo.money -= summe;
                pInfo.bank += summe;
                client.SendChatMessage($"~w~Du hast ~g~${summe} ~w~auf dein Konto eingezahlt!");
                client.SendChatMessage($"~w~Neuer Kontostand: ~g~${pInfo.bank}~w~ | Bargeld: ~g~${pInfo.money}");
                client.TriggerEvent("BankResult", 1);
                Database.Upsert(pInfo);

                EventTriggers.Update_Money(client);
                EventTriggers.Update_Bank(client);
            }
        }
    }
}
