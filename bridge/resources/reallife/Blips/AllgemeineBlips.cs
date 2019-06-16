using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Blips
{
    public class AllgemeineBlips : Script
    {
        public AllgemeineBlips()
        {
            //GARAGEN
            Blip Garag1 = NAPI.Blip.CreateBlip(50, new Vector3(-872.9297, -2736.075, 13.96047), 1.0f, 41);
            NAPI.Blip.SetBlipName(Garag1, "Vehicle Garage"); NAPI.Blip.SetBlipShortRange(Garag1, true); NAPI.Blip.SetBlipScale(Garag1, 0.8f);
            

            //AUTOHÄUSER
            Blip Autohaus1 = NAPI.Blip.CreateBlip(225, new Vector3(-28.59104,-1657.484,38.08443), 1.0f, 5);
            NAPI.Blip.SetBlipName(Autohaus1, "Mosley`s Autohaus");NAPI.Blip.SetBlipShortRange(Autohaus1, true); NAPI.Blip.SetBlipScale(Autohaus1, 0.8f);

            //REPERATUR WERKSTÄTTEN
            Blip Repair1 = NAPI.Blip.CreateBlip(566, new Vector3(731.3959, -1088.848, 22.16904), 1.0f, 0);
            NAPI.Blip.SetBlipName(Repair1, "Reperatur Werkstatt"); NAPI.Blip.SetBlipShortRange(Repair1, true); NAPI.Blip.SetBlipScale(Repair1, 0.8f);

            Blip Repair2 = NAPI.Blip.CreateBlip(566, new Vector3(111.7056, 6625.828, 31.40783), 1.0f, 0);
            NAPI.Blip.SetBlipName(Repair2, "Reperatur Werkstatt"); NAPI.Blip.SetBlipShortRange(Repair2, true); NAPI.Blip.SetBlipScale(Repair2, 0.8f);

            Blip Repair3 = NAPI.Blip.CreateBlip(566, new Vector3(-965.0243, -3024.913, 13.56587), 1.0f, 0);
            NAPI.Blip.SetBlipName(Repair3, "Reperatur Werkstatt"); NAPI.Blip.SetBlipShortRange(Repair3, true); NAPI.Blip.SetBlipScale(Repair3, 0.8f);
        }
    }
}
