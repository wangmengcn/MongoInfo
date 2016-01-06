using System;
using System.IO;
using System.Windows.Forms;
using MongoDB;

/****** Changed by Eclipse_Sv@2015/11/21 11:05 ******/

namespace MongoInfo
{
    public class MongoOperation
    {
#pragma warning disable CS0618 // 类型或成员已过时
        private IMongoCollection _col;
#pragma warning restore CS0618 // 类型或成员已过时
        private string _gDatabase;
        private Mongo _gMongo;
        private string _gServerip;
        private string _gServerPort;
        private IMongoDatabase _gTest;


        public void Connect2Mongo(string ip, string port, string database)
        {
            try
            {
                var connstr = @"Server=" + ip + ":" + port; //外网地址
                //var connstr = @"Server=localhost:27017";
                _gMongo = new Mongo(connstr);
                _gMongo.Connect();

                _gTest = _gMongo[database];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常信息");
            }
        }


        public void Connect2Mongo()
        {
            try
            {
                _gTest = MongoConnect.GetMongodb();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常信息");
            }
        }


        public void UseCollection(string tablename)
        {
            if (tablename == null) throw new ArgumentNullException(nameof(tablename));
            if (!string.IsNullOrEmpty(tablename))
            {
                switch (tablename)
                {
                    case "角色表":
                        _col = _gTest.GetCollection("RoleTable");
                        break;
                    case "用户表":
                        _col = _gTest.GetCollection("UserTable");
                        break;
                    case "单位部门表":
                        _col = _gTest.GetCollection("DepTable");
                        break;
                    case "研判过程":
                        _col = _gTest.GetCollection("SJTable");
                        break;
                    case "研判任务":
                        _col = _gTest.GetCollection("SJTaskTable");
                        break;
                    case "洪涝案例":
                        _col = _gTest.GetCollection("FloodRecTable");
                        break;
                    case "地震案例":
                        _col = _gTest.GetCollection("EQRecTable");
                        break;
                    case "旱灾案例":
                        _col = _gTest.GetCollection("DroutRecTable");
                        break;
                    case "雪灾案例":
                        _col = _gTest.GetCollection("SnowTable");
                        break;
                    case "标准表":
                        _col = _gTest.GetCollection("StandarsTable");
                        break;
                    case "背景知识":
                        _col = _gTest.GetCollection("BackgroundTable");
                        break;
                    case "损毁等级":
                        _col = _gTest.GetCollection("DamageGradeTable");
                        break;
                    case "解译标志":
                        _col = _gTest.GetCollection("TargetsTable");
                        break;
                    case "模型库":
                        _col = _gTest.GetCollection("ModelTable");
                        break;
                    case "灾害元数据":
                        _col = _gTest.GetCollection("DisasterMetadata");
                        break;
                    case "比较研判":
                        _col = _gTest.GetCollection("Comparison");
                        break;
                }
            }
        }

        public void InsertMsg(Document doc)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            try
            {
                _col.Save(doc);
            }
            catch
            {
                MessageBox.Show("未能上传数据");
            }
        }

        public void DelMsg(Document doc)
        {
            _col?.Remove(doc);
        }

        public void Modify(Document doc)
        {
            _col?.Save(doc);
        }

       
        public ICursor QuryMsg(Document doc)
        {
            ICursor result = null;
            result = _col.Find(doc);
            return result;
        }

        public ICursor FindAll()
        {
            ICursor result = null;
            result = _col.FindAll();
            return result;
        }

        public void Fsupload(string localfilename, string filename)
        {
            var fs = MongoConnect.GetFs();
            Stream fsStream = new FileStream(localfilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fs.Upload(fsStream, filename);
        }

        public void fsdownload(string filename)
        {
            var fs = MongoConnect.GetFs();
            var apppath = Application.StartupPath;
            var localpath = apppath + "\\word\\Background\\" + filename;
            fs.Download(localpath, filename);
        }


        public void fsdownload(string localpath, string remotefile)
        {
            var fs = MongoConnect.GetFs();
            try
            {
                fs.Download(localpath, remotefile);
            }
            catch (Exception e)
            {
            }
        }

        public bool Taskprintscr(string localpath, string remotefile)
        {
            var fs = MongoConnect.GetFs();
            try
            {
                fs.Download(localpath, remotefile);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}