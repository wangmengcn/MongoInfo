using MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;


/****** Changed by Eclipse_Sv@2015/11/21 11:05 ******/

namespace MongoInfo
{
    public class MongoConnect
    {
        private static IMongoDatabase _gTest;
        private static Mongo _gMongo;
        private static MongoGridFS _fs;
        private static MongoDB.Driver.MongoDatabase _db;

        public static void MongoConnection()
        {
            string connstr = @"Server=" + MongoXml.GetConfig()[0] + ":" + MongoXml.GetConfig()[1];
            _gMongo = new Mongo(connstr);
            _gMongo.Connect();
        }

        public static IMongoDatabase GetMongodb()
        {
            _gTest = _gMongo[MongoXml.GetConfig()[2]];
            return _gTest;
        }

        

        public static void MongoFsConnect()
        {
            var strconn = "mongodb://" + MongoXml.GetMongoIp() + ":" + MongoXml.GetMongoPort();
            var server = MongoServer.Create(strconn);

            _db = server[MongoXml.GetConfig()[2]];
        }

        public static MongoGridFS GetFs()
        {  
            _fs = _db.GridFS;
            return _fs;
        }
    }
}
