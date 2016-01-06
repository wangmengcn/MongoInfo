using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace MongoInfo
{
    internal class MongoXml
    {
        private readonly XmlDocument _xmldoc = new XmlDocument();

        public void Loaddoc(string xmlpath)
        {
            try
            {
                _xmldoc.Load(xmlpath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        public static List<string> GetConfig()
        {
            var filepath = "./conf/config.xml";
            var conf = new List<string>();
            var confdoc = new XmlDocument();
            try
            {
                confdoc.Load(filepath);
                var nodelist = confdoc.SelectNodes("/MongoConf/ServerName");
                var element = nodelist?[0] as XmlElement;
                if (element != null)
                {
                    conf.Add(element.GetAttribute("ip"));
                    conf.Add(element.GetAttribute("port"));
                    conf.Add(element.GetAttribute("db"));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return conf;
        }


        public static string GetMongoIp()
        {
            var filepath = "./conf/config.xml";
            var ip = string.Empty;
            var confdoc = new XmlDocument();
            try
            {
                confdoc.Load(filepath);
                var nodelist = confdoc.SelectNodes("/MongoConf/ServerName");
                var element = nodelist?[0] as XmlElement;
                if (element != null)
                {
                    ip = element.GetAttribute("ip");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return ip;
        }

        public static string GetMongoPort()
        {
            var filepath = "./conf/config.xml";
            var ip = string.Empty;
            var confdoc = new XmlDocument();
            try
            {
                confdoc.Load(filepath);
                var nodelist = confdoc.SelectNodes("/MongoConf/ServerName");
                var element = nodelist?[0] as XmlElement;
                if (element != null)
                {
                    ip = element.GetAttribute("port");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return ip;
        }

        public static string GetMongoInterval()
        {
            var filepath = "./conf/config.xml";
            var ip = string.Empty;
            var confdoc = new XmlDocument();
            try
            {
                confdoc.Load(filepath);
                var nodelist = confdoc.SelectNodes("/MongoConf/ServerName");
                var element = nodelist?[0] as XmlElement;
                if (element != null)
                {
                    ip = element.GetAttribute("interval");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return ip;
        }


        public static string Get503Process()
        {
            var filepath = "./conf/503.xml";
            var ip = string.Empty;
            var confdoc = new XmlDocument();
            try
            {
                confdoc.Load(filepath);
                var nodelist = confdoc.SelectNodes("/Config/Path");
                var element = nodelist?[0] as XmlElement;
                if (element != null)
                {
                    ip = element.GetAttribute("Process");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return ip;
        }


        public XmlNodeList Getnodes()
        {
            var nodelist = _xmldoc.SelectNodes("/MongoConf/ServerName");
            return nodelist;
        }

        public void AddXmlNode(string name, string ip, string port)
        {
            var root = _xmldoc.SelectSingleNode("MongoConf");
            if (root == null)
            {
            }
            if (Chechnode(name))
            {
                var newnode = _xmldoc.CreateElement("ServerName");
                newnode.SetAttribute("name", name);
                newnode.SetAttribute("ip", ip);
                newnode.SetAttribute("port", port);

                root?.AppendChild(newnode);
                _xmldoc.Save("./config/config.xml");
                MessageBox.Show("添加成功");
            }
            else
                MessageBox.Show("节点已存在，请设置其他的名称！");
        }

        private string XmlNode(string name)
        {
            string nodestr = string.Empty;

            return nodestr;
        }


        public string GetjobId()
        {
            var jobid = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/jobID");
            var element = childNodes?[0] as XmlElement;
            if (element != null) jobid = element.InnerText;
            return jobid;
        }

        //获取Input.xml中的用户账户
        public string GetAccount()
        {
            var account = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/account");
            var element = childNodes?[0] as XmlElement;
            if (element != null) account = element.InnerText;
            return account;
        }

        //获取Input.xml中的用户名
        public string Getusername()
        {
            var username = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/user");
            var element = childNodes?[0] as XmlElement;
            if (element != null) username = element.InnerText;
            return username;
        }

        //获取Input.xml中的密码
        public string Getpassword()
        {
            var password = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/password");
            var element = childNodes?[0] as XmlElement;
            if (element != null) password = element.InnerText;
            return password;
        }

        //获取Input.xml中的用户所在部门
        public string Getdepartment()
        {
            var department = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/department");
            var element = childNodes?[0] as XmlElement;
            if (element != null) department = element.InnerText;
            return department;
        }

        //获取Input.xml中的用户性别
        public string Getsex()
        {
            var sex = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/sex");
            var element = childNodes?[0] as XmlElement;
            if (element != null) sex = element.InnerText;
            return sex;
        }

        //获取Input.xml中的用户职务
        public string Gettitle()
        {
            var title = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/title");
            var element = childNodes?[0] as XmlElement;
            if (element != null) title = element.InnerText;
            return title;
        }

        //获取Input.xml中的用户电话
        public string Getphone()
        {
            var phone = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/phone");
            var element = childNodes?[0] as XmlElement;
            if (element != null) phone = element.InnerText;
            return phone;
        }

        //获取Input.xml中的用户电子邮件
        public string Getemail()
        {
            var email = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/email");
            var element = childNodes?[0] as XmlElement;
            if (element != null) email = element.InnerText;
            return email;
        }

        //获取Input.xml中的用户权限
        public string Getrightlevel()
        {
            var rightlevel = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/rightlevel");
            var element = childNodes?[0] as XmlElement;
            if (element != null) rightlevel = element.InnerText;
            return rightlevel;
        }


        //返回Input.xml文件中的放缩略图的相对路径，503要求在其中新建一个文件夹
        public string GetOutputFile()
        {
            var jobid = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/outFilePath");
            var element = childNodes?[0] as XmlElement;
            if (element != null) jobid = element.InnerText;
            return jobid;
        }

        //返回Input.xml文件中放Output.xml文件的相对路径
        public string GetOutFilePath()
        {
            var jobid = string.Empty;
            var childNodes = _xmldoc.SelectNodes("/root/content/public/processOutPath");
            var element = childNodes?[0] as XmlElement;
            if (element != null) jobid = element.InnerText;
            return jobid;
        }


        //获取503XML文件中传输数据列表
        public List<string> GetInputFileName()
        {
            var childNodes = _xmldoc.SelectNodes("/root/content/public/inputFileList/inputFile");
            var fileslist = new List<string>();
            if (childNodes != null)
                fileslist.AddRange(from XmlNode node in childNodes select node.InnerText);
            return fileslist;
        }


        public void DelNode(string name)
        {
            var childNodes = _xmldoc.SelectNodes("/MongoConf/ServerName");

            var rootnode = _xmldoc.SelectSingleNode("MongoConf");
            for (var i = 0; i < childNodes.Count; i++)
            {
                var childElement = (XmlElement) childNodes[i];
                if (childElement.GetAttribute("name") == name)
                {
                    //childElement.RemoveAll();
                    rootnode?.RemoveChild(childElement);

                    break;
                }
            }

            _xmldoc.Save("./config/config.xml");
        }

        public void ModifyNode(string name, string ip, string port)
        {
            var childNodes = _xmldoc.SelectNodes("/MongoConf/ServerName");


            for (var i = 0; i < childNodes.Count; i++)
            {
                var childElement = (XmlElement) childNodes[i];
                if (childElement.GetAttribute("name") == name)
                {
                    //childElement.RemoveAll();
                    childElement.SetAttribute("ip", ip);
                    childElement.SetAttribute("port", port);
                    break;
                }
            }

            _xmldoc.Save("./config/config.xml");
        }


        private bool Chechnode(string servername)
        {
            var nodelist = Getnodes();
            for (var i = 0; i < nodelist.Count; i++)
            {
                var xmlNode = nodelist.Item(i);
                if (xmlNode?.Attributes != null)
                {
                    var name = xmlNode.Attributes["name"].Value;
                    if (servername == name)
                        return false;
                }
            }

            return true;
        }
    }
}