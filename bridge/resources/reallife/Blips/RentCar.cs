using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Blips
{
    public class RentCar : Script
    {
        public RentCar()
        {
            Blip RENTSP = NAPI.Blip.CreateBlip(88, new Vector3(-1153.755, -718.8862, 20.97753), 1.0f, 38);
            NAPI.Blip.SetBlipName(RENTSP, "Fahrzeug Vermietung"); NAPI.Blip.SetBlipShortRange(RENTSP, true);

            //Marker rentcar = NAPI.Marker.CreateMarker(30, new Vector3(-1153.755, -718.8862, 20.97753), new Vector3(), new Vector3(0, 0, 236.175), 1, new Color(16, 78, 139, 100));

            NAPI.TextLabel.CreateTextLabel("Hier kannst du dir mit Taste ~b~E~w~ einen Roller\nfür ~g~150$~w~ Mieten!", new Vector3(-1153.755, -718.8862, 20.97753), 12, 1f, 4, new Color(255, 255, 255, 255));
           //NAPI.TextLabel.CreateTextLabel("/rent - Miete dir einen Roller", new Vector3(-1009.779, -2684.478, 13.97447), 12, 1f, 4, new Color(255, 255, 255, 255));
        }
    }
}
