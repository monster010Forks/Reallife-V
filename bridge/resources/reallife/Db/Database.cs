using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace reallife.Db
{
    public static class Database
    {
        public static bool Upsert<T>(T data)
        {
            using (var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().Upsert(data);
            }
        }

        public static T GetData<T>(string fieldName, BsonValue data)
        {
            using (var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().FindOne(Query.EQ(fieldName, data));
            }
        }

        public static bool Update<T>(T data)
        {
            using (var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().Update(data);
            }
        }

        internal static object GetData<T>(string v1, string vorname, string v2, string nachname)
        {
            throw new NotImplementedException();
        }

        public static T GetById<T>(int id)
        {
            using (var db = new LiteDatabase(@"./Database.db"))
            {
                return db.GetCollection<T>().FindById(id);
            }
        }
    }
}
