using GTANetworkAPI;

namespace reallife.Blips
{
    public class FrakBlips : Script
    {
        public FrakBlips()
        {
            //LSPD BLIPS
            Blip LSPD1 = NAPI.Blip.CreateBlip(60, new Vector3(427.6091, -981.9763, 30.71009), 1.0f, 38);
            NAPI.Blip.SetBlipName(LSPD1, "LSPD"); NAPI.Blip.SetBlipShortRange(LSPD1, true);

            //SARU BLIPS
            Blip SARU1 = NAPI.Blip.CreateBlip(61, new Vector3(1151.196, -1529.605, 35.36937), 1.0f, 1);
            NAPI.Blip.SetBlipName(SARU1, "SARU"); NAPI.Blip.SetBlipShortRange(SARU1, true);
        }
    }
}
