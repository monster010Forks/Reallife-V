using BCrypt;
using reallife.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace reallife.Player
{
    public class Players
    {
        public int _id { get; set; }
        public string socialclub { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int ban { get; set; }
        public int warn { get; set; }

        public void Update() => Database.Update(this);
        public void Upsert() => Database.Upsert(this);

        public Players() { }
        public Players(string username, string password, string socialclub)
        {
            this.username = username;
            this.password = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
            this.socialclub = socialclub;
        }

        public bool CheckPassword(string input)
        {
            if (password == null)
                return false;

            return BCryptHelper.CheckPassword(input, this.password);
        }
    }
}
