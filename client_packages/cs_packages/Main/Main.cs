using RAGE;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Main
{
    public class Main : Events.Script
    {
        public static float Player_Money { get; set; } = 0;
        public static float Player_Bank { get; set; } = 0;
        public static int ResX = 0;
        public static int ResY = 0;

        public Main()
        {
            // Chat.Output("Client wurde geladen!");
            Events.Add("update_money", UpdateMoney);
            Events.Add("update_bank", UpdateBank);

            Events.Tick += OnUpdate;
            RAGE.Game.Graphics.GetScreenResolution(ref ResX, ref ResY);
        }

        private void OnUpdate(List<Events.TickNametagData> nametags)
        {
            RAGE.Game.UIText.Draw($"${Player_Money}", new Point(ResX - 1050, 670), 0.5f, Color.White, RAGE.Game.Font.Pricedown, false);
            RAGE.Game.UIText.Draw($"${Player_Bank}", new Point(ResX - 1050, 690), 0.4f, Color.Blue, RAGE.Game.Font.Pricedown, false);
        }

        private static void UpdateMoney(object[] args)
        {
            Player_Money = Convert.ToSingle(args[0]);
        }

        private static void UpdateBank(object[] args)
        {
            Player_Bank = Convert.ToSingle(args[0]);
        }
    }
}
