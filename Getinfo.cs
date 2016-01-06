using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MongoDB;

namespace MongoInfo
{
    public class Getinfo
    {
        private MongoOperation _mp;
        public DataGridView Caseinfo;

        public DataGridView Getcaseinfo()
        {
            //初始化DataGridView
            Caseinfo = new DataGridView();


            Caseinfo.ColumnCount = 8;
            Caseinfo.Columns[0].Name = "开始时间";
            Caseinfo.Columns[1].Name = "结束时间";
            Caseinfo.Columns[2].Name = "耕地NDVI";
            Caseinfo.Columns[3].Name = "palmer";
            Caseinfo.Columns[4].Name = "SPI";
            Caseinfo.Columns[5].Name = "农作物播种面积";
            Caseinfo.Columns[6].Name = "实际受灾面积";
            Caseinfo.Columns[7].Name = "GDP";

            var chk = new DataGridViewCheckBoxColumn();
            chk.HeaderText = "选择";
            chk.Name = "chk";

            Caseinfo.Columns.Add(chk);

            MongoConnect.MongoConnection();
            _mp = new MongoOperation();
            _mp.Connect2Mongo();
            _mp.UseCollection("旱灾案例");
            var cases = _mp.FindAll();
            if (!cases.Documents.Any()) return Caseinfo;
            foreach (var celvalue in from casedoc in cases.Documents
                let cel = 0
                select new List<string>
                {
                    casedoc["Time"].ToString(),
                    casedoc["EndTime"].ToString(),
                    casedoc["NDVI"].ToString(),
                    casedoc["Palmer"].ToString(),
                    casedoc["SPI"].ToString(),
                    casedoc["CropArea"].ToString(),
                    casedoc["AffectedCropArea"].ToString(),
                    casedoc["GDP"].ToString()
                })
            {
                // ReSharper disable once CoVariantArrayConversion
                Caseinfo.Rows.Add(celvalue.ToArray());
            }

            return Caseinfo;
        }
    }


    public class YesorNo
    {
        private List<string> _alltask;
        private readonly MongoOperation _mp;
        private List<string> _taskuser;


        public YesorNo()
        {
            MongoConnect.MongoConnection();
            MongoConnect.MongoFsConnect();
            MongoConnect.GetFs();
            _mp = new MongoOperation();
            _mp.Connect2Mongo();
        }


        //用于获取所有任务ID
        public List<string> GetAllTasks()
        {
            _alltask = new List<string>();
            _mp.UseCollection("研判任务");
            var quryresult = _mp.FindAll();
            foreach (var taskdocs in quryresult.Documents)
            {
                _alltask.Add(taskdocs["SJTaskID"].ToString());
            }
            return _alltask;
        }

        //用于获取指定任务的参与人员
        public List<string> GetAllUsers(string taskid)
        {
            _taskuser = new List<string>();
            _mp.UseCollection("研判任务");
            Document qurydoc;
            qurydoc = new Document();
            if (!string.IsNullOrEmpty(taskid))
            {
                qurydoc["SJTaskID"] = taskid;
                var quryresult = _mp.QuryMsg(qurydoc);
                foreach (var resultdoc in quryresult.Documents)
                {
                    var namedoc = resultdoc["User"] as Document;
                    if (namedoc != null)
                        foreach (var names in namedoc.Keys)
                        {
                            _taskuser.Add(names);
                        }
                }
            }

            return _taskuser;
        }

        //获取制定任务和人员的作业
        public string GetDocuments(string taskid, string name)
        {
            var filename = string.Empty;
            _mp.UseCollection("研判过程");
            var share = new Document();
            share["UserName"] = name;
            share["SJTaskID"] = taskid;
            var sharecur = _mp.QuryMsg(share).Sort("SJSubmitTime", IndexOrder.Descending).Limit(1);
            if (sharecur.Documents != null)
            {
                var sharresult = new Document();
                foreach (var sharedoc in sharecur.Documents)
                {
                    sharresult = sharedoc;
                }
                if (sharresult.Count != 0)
                {
                    filename = sharresult["SJSubmitFiles"].ToString();
                    if (!File.Exists(Application.StartupPath + @"\yesorno\" + name + "-共享" + filename + ".UDB"))
                    {
                        //获取SHP
                        _mp.fsdownload(Application.StartupPath + @"\yesorno\" + name + "-共享" + filename + ".UDB",
                            filename + ".UDB");
                        _mp.fsdownload(Application.StartupPath + @"\yesorno\" + name + "-共享" + filename + ".UDD",
                            filename + ".UDD");
                        //获取TIF
                        _mp.fsdownload(Application.StartupPath + @"\yesorno\" + "任务底图" + taskid + ".UDB",
                            "任务底图" + taskid + ".UDB");
                        _mp.fsdownload(Application.StartupPath + @"\yesorno\" + "任务底图" + taskid + ".UDD",
                            "任务底图" + taskid + ".UDD");
                        MessageBox.Show("文件已下载");
                    }
                    else
                    {
                        MessageBox.Show("文件已存在");
                    }
                }
                else
                {
                    MessageBox.Show("尚未有文件提交");
                }
            }
            return filename;
        }

        //返回评估结果更新数据库
        public void ReturnValue(string taskid, bool yesorno, string comment)
        {
            _mp.UseCollection("研判任务");
            var share = new Document();
            share["SJTaskID"] = taskid;
            var sharecur = _mp.QuryMsg(share);
            Document returndoc = null;
            foreach (var doc in sharecur.Documents)
            {
                returndoc = doc;
            }
            if (returndoc != null)
            {
                if (yesorno)
                {
                    returndoc["Level"] = "合格";
                }
                else
                {
                    returndoc["Level"] = "不合格";
                }

                returndoc["Comment"] = comment;
                _mp.Modify(returndoc);
                MessageBox.Show("已返回结果");
            }
            else
            {
                MessageBox.Show("未能提交结果");
            }
        }
    }
}