using RAGE;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Wanteds
{
    public class Wanteds : Events.Script
    {
        public static float Player_Wanteds { get; set; } = 0;
        public static int ResX = 0;
        public static int ResY = 0;

        public Wanteds()
        {
            // Chat.Output("Client wurde geladen!");
            Events.Add("update_wanteds", UpdateWanteds);

            Events.Tick += OnUpdate;
            RAGE.Game.Graphics.GetScreenResolution(ref ResX, ref ResY);
        }

        private void OnUpdate(List<Events.TickNametagData> nametags)
        {
            RAGE.Game.UIText.Draw($"W: {Player_Wanteds}", new Point(ResX - 1050, 655), 0.4f, Color.Red, RAGE.Game.Font.Pricedown, false);
        }

        private static void UpdateWanteds(object[] args)
        {
            Player_Wanteds = Convert.ToSingle(args[0]);
        }
    }
}
