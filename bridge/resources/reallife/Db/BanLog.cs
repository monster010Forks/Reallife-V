using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Db
{
    class BanLog
    {
        public int _id { get; set; }
        public string banned { get; set; }
        public string bannedby { get; set; }
        public string grund { get; set; }

        public void Update() => Database.Update(this);
        public void Upsert() => Database.Upsert(this);
    }
}
