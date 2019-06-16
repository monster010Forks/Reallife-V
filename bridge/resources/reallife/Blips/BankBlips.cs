using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Blips
{
    public class BankBlips : Script 
    {
        public BankBlips()
        {
            //BANKBLIPS
            Blip Bank1 = NAPI.Blip.CreateBlip(500, new Vector3(234.8648, 217.0689, 106.2867), 1.0f, 25);
            NAPI.Blip.SetBlipName(Bank1, "Bank"); NAPI.Blip.SetBlipShortRange(Bank1, true);

            Blip Bank2 = NAPI.Blip.CreateBlip(500, new Vector3(-1212.882, -330.5203, 37.78701), 1.0f, 25);
            NAPI.Blip.SetBlipName(Bank2, "Bank"); NAPI.Blip.SetBlipShortRange(Bank2, true);

            Blip Bank3 = NAPI.Blip.CreateBlip(500, new Vector3(-350.8686, -49.62464, 49.04257), 1.0f, 25);
            NAPI.Blip.SetBlipName(Bank3, "Bank"); NAPI.Blip.SetBlipShortRange(Bank3, true);

            Blip Bank4 = NAPI.Blip.CreateBlip(500, new Vector3(314.1704, -278.6919, 54.17079), 1.0f, 25);
            NAPI.Blip.SetBlipName(Bank4, "Bank"); NAPI.Blip.SetBlipShortRange(Bank4, true);

            Blip Bank5 = NAPI.Blip.CreateBlip(500, new Vector3(149.6047, -1040.399, 29.37408), 1.0f, 25);
            NAPI.Blip.SetBlipName(Bank5, "Bank"); NAPI.Blip.SetBlipShortRange(Bank5, true);

            Blip Bank6 = NAPI.Blip.CreateBlip(500, new Vector3(-2962.524, 482.8735, 15.7031), 1.0f, 25);
            NAPI.Blip.SetBlipName(Bank6, "Bank"); NAPI.Blip.SetBlipShortRange(Bank6, true);

            Blip Bank7 = NAPI.Blip.CreateBlip(500, new Vector3(1175.184, 2706.114, 38.09402), 1.0f, 25);
            NAPI.Blip.SetBlipName(Bank7, "Bank"); NAPI.Blip.SetBlipShortRange(Bank7, true);

            //BANKTEXTE
            /*BANK1*/
            NAPI.TextLabel.CreateTextLabel("Hier kannst du dein Bankkonto beantragen\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(243.1547, 224.7177, 106.2868), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Hier kannst du deinen Pin von\ndeinem Bankkonto ändern\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(246.6217, 223.5182, 106.2867), 12, 1f, 4, new Color(255, 255, 255, 255));
            /*BANK2*/
            NAPI.TextLabel.CreateTextLabel("Hier kannst du dein Bankkonto beantragen\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(-1212.084, -330.4282, 37.78704), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Hier kannst du deinen Pin von\ndeinem Bankkonto ändern\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(-1214.762, -331.7896, 37.7907), 12, 1f, 4, new Color(255, 255, 255, 255));
            /*BANK3*/
            NAPI.TextLabel.CreateTextLabel("Hier kannst du dein Bankkonto beantragen\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(-350.551, -50.09611, 49.04259), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Hier kannst du deinen Pin von\ndeinem Bankkonto ändern\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(-352.7701, -49.25988, 49.04625), 12, 1f, 4, new Color(255, 255, 255, 255));
            /*BANK4*/
            NAPI.TextLabel.CreateTextLabel("Hier kannst du dein Bankkonto beantragen\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(314.8576, -279.327, 54.17081), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Hier kannst du deinen Pin von\ndeinem Bankkonto ändern\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(312.1963, -278.3508, 54.17446), 12, 1f, 4, new Color(255, 255, 255, 255));
            /*BANK5*/
            NAPI.TextLabel.CreateTextLabel("Hier kannst du dein Bankkonto beantragen\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(150.402, -1040.878, 29.3741), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Hier kannst du deinen Pin von\ndeinem Bankkonto ändern\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(148.3513, -1040.156, 29.37776), 12, 1f, 4, new Color(255, 255, 255, 255));
            /*BANK6*/
            NAPI.TextLabel.CreateTextLabel("Hier kannst du dein Bankkonto beantragen\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(-2962.552, 483.3518, 15.70312), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Hier kannst du deinen Pin von\ndeinem Bankkonto ändern\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(-2962.656, 480.7722, 15.70677), 12, 1f, 4, new Color(255, 255, 255, 255));
            /*BANK7*/
            NAPI.TextLabel.CreateTextLabel("Hier kannst du dein Bankkonto beantragen\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(1174.971, 2706.805, 38.09408), 12, 1f, 4, new Color(255, 255, 255, 255));
            NAPI.TextLabel.CreateTextLabel("Hier kannst du deinen Pin von\ndeinem Bankkonto ändern\n==>Zum öffnen nutze die taste ~g~E~w~<==", new Vector3(1176.94, 2706.805, 38.09771), 12, 1f, 4, new Color(255, 255, 255, 255));
        }
    }
}
