using System;
using System.Collections.Generic;
using System.Text;
using BCrypt;
using GTANetworkAPI;
using LiteDB;
using reallife.Db;

namespace reallife.Player
{
    public class PlayerInfo
    {
        public int _id { get; set; }
        public string socialclub { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public double money { get; set; } = 13500;
        public double bank { get; set; } = 0;
        public int bkonto { get; set; } = 0;
        public int bkontopin { get; set; } = 0;
        public int adminrank { get; set; } = 0;
        public int fraktion { get; set; } = 0;
        public int fwarn { get; set; }
        public int fleader { get; set; }
        public int wantedlevel { get; set; }
        public int cuff { get; set; }
        public int jail { get; set; }
        public int jailtime { get; set; }
        public int ban { get; set; } = 0;
        public int warn { get; set; }
        public double[] last_location { get; set; } = new double[] { -1167.994, -700.4285, 21.89281 };
        public double[] temp_location { get; set; } = null;
        public int clothes_1 { get; set; } = 0;
        public int clothes_2 { get; set; } = 49;
        public int clothes_3 { get; set; } = 0;
        public int clothes_4 { get; set; } = 9;
        public int clothes_5 { get; set; } = 0;
        public int clothes_6 { get; set; } = 4;
        public int clothes_7 { get; set; } = 0;
        public int clothes_8 { get; set; } = 0;
        public int clothes_9 { get; set; } = 0;
        public int clothes_10 { get; set; } = 0;
        public int clothes_11 { get; set; } = 163;

        public void Update() => Database.Update(this);
        public void Upsert() => Database.Upsert(this);

        public PlayerInfo() { }

        public PlayerInfo(string username, string password, int adminrank, string socialclub, string vorname, string nachname)
        {
            this.username = username;
            this.password = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
            this.socialclub = socialclub;
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

        public bool CheckPassword(string input)
        {
            if (password == null)
                return false;

            return BCryptHelper.CheckPassword(input, this.password);
        }

        public static string WhichADMIN(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            string rang;
            int zahl = 0;

            if (playerInfo.adminrank == 1)
            {
                zahl = 1;
            }

            if (playerInfo.adminrank == 2)
            {
                zahl = 2;
            }

            if (playerInfo.adminrank == 3)
            {
                zahl = 3;
            }

            switch (zahl)
            {
                case 1:
                    rang = "Supporter";
                    break;

                case 2:
                    rang = "Admin";
                    break;

                case 3:
                    rang = "Serverleiter";
                    break;

                default:
                    rang = "Bürger";
                    break;
            }

            return rang;
        }

        public static string WhichFrak(Client client)
        {
            PlayerInfo playerInfo = PlayerHelper.GetPlayerStats(client);
            string frak;
            int zahl = 0;

            if (playerInfo.fraktion == 1)
            {
                zahl = 1;
            }

            if (playerInfo.fraktion == 2)
            {
                zahl = 2;
            }

            if (playerInfo.fraktion == 3)
            {
                zahl = 3;
            }

            switch (zahl)
            {
                case 1:
                    frak = "LSPD";
                    break;

                case 2:
                    frak = "SARU";
                    break;

                case 3:
                    frak = "Grove Street";
                    break;

                default:
                    frak = "Keine";
                    break;
            }

            return frak;
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
