using reallife.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Player
{
    public class PlayerClothes
    {
        public int _id { get; set; }
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
    }
}
