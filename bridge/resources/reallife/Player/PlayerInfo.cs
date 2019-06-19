using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;
using GTANetworkAPI;
using LiteDB;
using reallife.Data;
using reallife.Db;

namespace reallife.Player
{
    public class PlayerInfo
    {
        static readonly string[] rankNames = new string[] {
            "Bürger",
            "Supporter",
            "Admin",
            "Serverleiter" };

        static readonly string[] frakNames = new string[] {
            "Keine",
            "LSPD",
            "SARU",
            "Grove Street" };

        public int _id { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public double money { get; set; } = 13500;
        public double bank { get; set; }
        public int bkonto { get; set; }
        public int bkontopin { get; set; }
        public int adminrank { get; set; }
        public int fraktion { get; set; }
        public int fwarn { get; set; }
        public int fleader { get; set; }
        public int wantedlevel { get; set; }
        public int cuff { get; set; }
        public int jail { get; set; }
        public int jailtime { get; set; }
        public double[] last_location { get; set; } = new double[] { -1167.994, -700.4285, 21.89281 };
        public double[] temp_location { get; set; }

        public void Update() => Database.Update(this);
        public void Upsert() => Database.Upsert(this);

        public PlayerInfo() { }

        public PlayerInfo(int adminrank, string vorname, string nachname)
        {
            this.adminrank = adminrank;
            this.vorname = vorname;
            this.nachname = nachname;
        }

        public Vector3 GetLastPlayerLocation()
        {
            return new Vector3(last_location[0], last_location[1], last_location[2]);
        }

        public Vector3 GetLastTempLocation()
        {
            return new Vector3(temp_location[0], temp_location[1], temp_location[2]);
        }

        public static string WhichADMIN(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            return rankNames[(playerInfo.adminrank > rankNames.Length) ? 0 : playerInfo.adminrank];
        }
        
        public static string WhichFrak(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);

            return frakNames[(playerInfo.fraktion > frakNames.Length) ? 0 : playerInfo.fraktion];
        }

        public bool AddMoney(double money_to_add)
        {
            if (money_to_add <= 0)
                return false;

            money += money_to_add;
            return true;
        }

        public bool SubMoney(double money_to_sub) 
        {
            if (money_to_sub <= 0)
                return false;

            if (money_to_sub > money)
                return false;

            money -= money_to_sub;

            if (money < 0)
            {
                money = 0;
            }
            return true;
        }

        public bool HasEnoughMoney(double amount)
        {
            if (money >= amount)
                return true;

              return false;
        }

    }
}
