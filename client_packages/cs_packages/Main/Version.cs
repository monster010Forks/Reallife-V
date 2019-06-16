using RAGE;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Version
{
    public class Version : Events.Script
    {
        public static int ResX = 0;
        public static int ResY = 0;

        public Version()
        {

            RAGE.Game.Graphics.GetScreenResolution(ref ResX, ref ResY);

            Events.Tick += OnUpdate;

        }

        private void OnUpdate(List<Events.TickNametagData> nametags)
        {
            RAGE.Game.UIText.Draw("Reallife-V Dev 0.4.1", new Point(ResY - 115, 10), 0.4f, Color.White, RAGE.Game.Font.HouseScript, false);
        }

    }
}
