using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.Data.OleDb;
using System.Threading;
using System.Collections.Generic;

namespace Aver3.Win
{
    public partial class Spider : Form
    {
        public Spider()
        {
            InitializeComponent();
        }
        #region console Spider
        const string connStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source=../../Data.mdb";
        static OleDbConnection conn = new OleDbConnection(connStr);//连接数据库
        static int recordPosition = 0;                             //记录点
        static Regex RegAnchor = new Regex("(?<=href *= *'?\"?)[^'\";> ]*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        static Regex RegText = new Regex("(?<=< *body[^>]*>).*", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
        static Regex RegTitle = new Regex("(?<=<title>).*(?=</title>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        static Regex RegTopUrl = new Regex("http:                  //[^/]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        static MatchCollection myMC = null;                        //匹配的模式
        static Hashtable newUrlList = new Hashtable();             //URL的列表
        static int updateFlag = 0;

        //获取HTML
        public class GetHtml
        {
            string url = null;      
            string content = null;  //内容
            public GetHtml(string incomeUrl)
            {
                url = incomeUrl;
            }
            private bool CheckUrl(string UrltoCheck)
            {

                if (Regex.IsMatch(UrltoCheck, "^#*$", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                    return false;
                else if (UrltoCheck == url || (UrltoCheck + "/") == url || UrltoCheck == (url + "/"))
                    return false;
                else if (UrltoCheck.EndsWith(".css"))
                    return false;
                else if (UrltoCheck.EndsWith(".wmv"))
                    return false;
                else if (UrltoCheck.EndsWith(".asf"))
                    return false;
                else if (UrltoCheck.EndsWith(".mp3"))
                    return false;
                else if (UrltoCheck.EndsWith(".avi"))
                    return false;
                else if (UrltoCheck.EndsWith(".mpg"))
                    return false;
                else if (UrltoCheck.EndsWith(".mpeg"))
                    return false;
                else if (UrltoCheck.EndsWith(".rmvb"))
                    return false;
                else if (UrltoCheck.EndsWith(".rm"))
                    return false;
                else if (UrltoCheck.EndsWith(".doc"))
                    return false;
                else if (UrltoCheck.EndsWith(".rar"))
                    return false;
                else if (UrltoCheck.EndsWith(".zip"))
                    return false;
                else if (UrltoCheck.EndsWith(".tar"))
                    return false;
                else if (UrltoCheck.EndsWith(".xls"))
                    return false;
                else if (UrltoCheck.EndsWith(".pdf"))
                    return false;
                else if (UrltoCheck.EndsWith(".jpg"))
                    return false;
                else if (UrltoCheck.EndsWith(".ico"))
                    return false;
                else if (UrltoCheck.EndsWith(".gif"))
                    return false;
                else if (UrltoCheck.EndsWith(".bmp"))
                    return false;
                else if (UrltoCheck.EndsWith(".png"))
                    return false;
                else if (UrltoCheck.StartsWith("ftp://"))
                    return false;
                else if (UrltoCheck.StartsWith("telnet://"))
                    return false;
                else if (UrltoCheck.StartsWith("mms://"))
                    return false;
                else if (UrltoCheck.StartsWith("rstp://"))
                    return false;
                else if (UrltoCheck.StartsWith("mailto"))
                    return false;
                else if (UrltoCheck.StartsWith("javascript"))
                    return false;
                else
                    return true;
            }
            private bool checkUrlThisSite(string NewUrltoCheck)
            {
                if (NewUrltoCheck.IndexOf("error") >= 0)
                    return false;
                else if (NewUrltoCheck.IndexOf("bbs") > 0)
                    return false;

                else if (Regex.IsMatch(NewUrltoCheck, @"\d\d\d\d.org", RegexOptions.IgnoreCase | RegexOptions.Compiled))
                    return true;
                else if (NewUrltoCheck.StartsWith("http://10.") || NewUrltoCheck.StartsWith("http://210.32.") || NewUrltoCheck.StartsWith("http://222.205."))
                    return true;
                else
                    return false;

            }
            private string GenUrl(string incomeUrl)
            {
                if (incomeUrl.StartsWith("http://"))
                    return incomeUrl;
                else
                {
                    incomeUrl = Regex.Replace(incomeUrl, @"(?<=[^.])./", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    while (incomeUrl.StartsWith("./"))
                        incomeUrl = incomeUrl.Substring(2, incomeUrl.Length - 2);

                    if (incomeUrl.StartsWith("/"))
                    {
                        string topUrl = RegTopUrl.Match(this.url).Value;
                        string trueUrl = topUrl + incomeUrl;
                        return trueUrl;
                    }
                    else if (incomeUrl.StartsWith("../"))
                    {
                        int lastBackPosition = incomeUrl.LastIndexOf("../") + 3;
                        int backTimes = lastBackPosition / 3;
                        int qPosition = this.url.IndexOf("?");
                        if (qPosition < 0)
                            qPosition = this.url.Length;
                        string subUrl = this.url.Substring(0, qPosition);
                        for (int i = 0; i <= backTimes; i++)
                        {
                            subUrl = subUrl.Substring(0, subUrl.LastIndexOf("/"));
                        }
                        subUrl = subUrl + "/";
                        string trueUrl = subUrl + Regex.Replace(incomeUrl, @"../", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        return trueUrl;
                    }
                    else
                    {
                        int qPisition = this.url.IndexOf("?");
                        if (qPisition < 0)
                            qPisition = this.url.Length;
                        string baseUrl = this.url.Substring(0, qPisition);
                        baseUrl = Regex.Replace(baseUrl, "(?<=.*/)[^/]*$", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        string trueUrl = baseUrl + incomeUrl;
                        return trueUrl;

                    }
                }
            }

            public void GetCode(object obj)
            {
                try
                {
                    updateFlag = 0;
                    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                    StreamReader myStreamReader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                    content = myStreamReader.ReadToEnd().ToLower();
                    myStreamReader.Close();
                    myResponse.Close();
                    string title = RegTitle.Match(content).Value;
                    string trueText = RegText.Match(content).Value;
                    trueText = Regex.Replace(trueText, "[\n\r\t]*", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);//删除\r\n
                    trueText = Regex.Replace(trueText.Trim(), " *", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    trueText = Regex.Replace(trueText, "<script[^>]*>.*?</script>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);//删除script
                    trueText = Regex.Replace(trueText, "<[^>]*>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    trueText = Regex.Replace(trueText, "(&nbsp;)*", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    trueText = Regex.Replace(trueText, "['\"]", "&quot;", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    SaveData mySD = new SaveData();
                    //	 mySD.SaveContent(url,title,trueText);
                    myMC = RegAnchor.Matches(content);
                    foreach (Match i in myMC)
                    {
                        if (i.Value == "")
                            continue;
                        string newUrl = i.Value;
                        bool pass1 = CheckUrl(newUrl);
                        if (pass1)
                        {
                            newUrl = GenUrl(newUrl);
                            bool pass2 = checkUrlThisSite(newUrl);
                            if (pass2)
                            {
                                if (!newUrlList.Contains(newUrl))
                                {
                                    mySD.SaveUrl(newUrl);
                                    newUrlList.Add(newUrl, null);
                                }
                            }
                        }
                    }
                    Console.WriteLine(++recordPosition);
                }
                catch
                {
                    Console.WriteLine(++recordPosition);
                    updateFlag = 1;
                }

            }
        }

        public static void GetUrl()
        {
            try
            {
                lock (conn)
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                }
                //  recordPosition = 0;
                /*
                string sqlAddList2 = "select url from url";
                OleDbCommand addListComm2 = new OleDbCommand(sqlAddList2, conn);
                OleDbDataReader addListRD2 = addListComm2.ExecuteReader();
                while (addListRD2.Read())
                {
                if (newUrlList.Contains(addListRD2["url"].ToString()))
                {
                string sqlDelUrl = "delete from url where url='" + addListRD2["url"].ToString() + "'";
                OleDbCommand delComm2 = new OleDbCommand(sqlDelUrl, conn);
                delComm2.ExecuteNonQuery();
                }
                else
                newUrlList.Add(addListRD2["url"].ToString(), null);
                }
                addListRD2.Close();
                                */
                //	string sqlDel = "delete from newurl where url like '%;%'";
                //	OleDbCommand delComm = new OleDbCommand(sqlDel, conn);
                //	delComm.ExecuteNonQuery();
                while (recordPosition < 1000)
                {
                    string sqlAddList = "select url from newurl";
                    OleDbCommand addListComm = new OleDbCommand(sqlAddList, conn);
                    OleDbDataReader addListRD = addListComm.ExecuteReader();
                    while (addListRD.Read())
                        if (!newUrlList.Contains(addListRD["url"].ToString()))
                            newUrlList.Add(addListRD["url"].ToString(), null);
                    addListRD.Close();
                    string sql = "select top 100 * from NewUrl where flag=0 order by id";
                    OleDbCommand comm = new OleDbCommand(sql, conn);
                    DataSet myDS = new DataSet();
                    OleDbDataAdapter myDA = new OleDbDataAdapter(comm);
                    myDA.Fill(myDS, "newurl");
                    DataTable myDT = new DataTable();
                    myDT = myDS.Tables[0];
                    //	recordPosition = myDT.Rows.Count;
                    OleDbCommandBuilder myCommB = new OleDbCommandBuilder(myDA);
                    foreach (DataRow i in myDT.Rows)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(new GetHtml(i["url"].ToString()).GetCode));
                        //  Thread HtmlCode = new Thread(new ThreadStart(new GetHtml(i["url"].ToString()).GetCode));
                        //	HtmlCode.Start();
                        //	HtmlCode.Join();
                        if (updateFlag == 0)
                            i["flag"] = 1;
                        else
                        {
                            i["flag"] = 2;
                            Console.WriteLine("********** " + i["url"] + " is Skiped**********");
                        }
                    }
                    myDA.Update(myDS, "newurl");
                    Console.WriteLine("All Finish!!!!!");
                    Thread.Sleep(5 * 1000);
                    //GetUrl();
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
            finally
            {
                lock (conn)
                {
                    if (conn != null)
                        conn.Close();
                }
            }
        }
        public class SaveData
        {
            string titletoSave = null;
            string urlToSave = null;
            string contenttoSave = null;

            public void SaveContent(string incomeUrl, string incomeTitle, string incomeContent)
            {
                try
                {
                    urlToSave = incomeUrl;
                    contenttoSave = incomeContent;
                    titletoSave = incomeTitle;
                    lock (conn)
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        string sql = "insert into Url (title,content,url) values('" + titletoSave + "','" + contenttoSave + "','" + urlToSave + "')";
                        OleDbCommand commStore = new OleDbCommand(sql, conn);
                        commStore.ExecuteNonQuery();
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.ToString());
                }
            }
            public void SaveUrl(string newUrl)
            {
                try
                {
                    lock (conn)
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        string sql = "insert into newurl (url) values('" + newUrl + "')";
                        OleDbCommand commSaveNewUrl = new OleDbCommand(sql, conn);
                        commSaveNewUrl.ExecuteNonQuery();
                    }
                }
                catch
                {
                    //	Console.WriteLine(ee.ToString());
                    Console.WriteLine("***** " + newUrl + " has been inserted!!");
                }
            }
        }
        #endregion

        #region GetHtml
        //调用的时候需要知道页面的编码方式
        string getHtml(string UrlPath, Encoding encodeType)
        {
            Uri url = new Uri(UrlPath);
            WebClient wc = new WebClient();
            wc.Encoding = encodeType;
            Stream s = wc.OpenRead(url);
            StreamReader sr = new StreamReader(s, encodeType);
            return sr.ReadToEnd();
        }
        void GetHtmlByWebrequest(string UrlPath)
        {

            Encoding encoding = Encoding.Default;
            WebRequest request = WebRequest.Create(UrlPath);
            using (Stream writer = request.GetRequestStream())
            {
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes("cookie");
                request.ContentLength = data.Length;
                writer.Write(data, 0, data.Length);
            }
            request.Timeout = 20 * 1000;//请求超时。
            request.Method = "GET"; //获取数据的方法。GET .POST
            HttpWebResponse Response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sReader = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                String Htmlstring = sReader.ReadToEnd();
            }
            //用正则表达式来剔除不需要的Html。

            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusDescription.ToUpper() == "OK")
            {
                switch (response.CharacterSet.ToLower())
                {
                    case "gbk":
                        encoding = Encoding.GetEncoding("GBK");//貌似用GB2312就可以
                        break;
                    case "gb2312":
                        encoding = Encoding.GetEncoding("GB2312");
                        break;
                    case "utf-8":
                        encoding = Encoding.UTF8;
                        break;
                    case "big5":
                        encoding = Encoding.GetEncoding("Big5");
                        break;
                    case "iso-8859-1":
                        encoding = Encoding.UTF8;//ISO-8859-1的编码用UTF-8处理，致少优酷的是这种方法没有乱码
                        break;
                    default:
                        encoding = Encoding.UTF8;//如果分析不出来就用的UTF-8
                        break;
                }
                comboBox1.Text = "Lenght:" + response.ContentLength.ToString() + "<br>CharacterSet:" + response.CharacterSet + "<br>Headers:" + response.Headers + "<br>";
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream, encoding);
                string responseFromServer = reader.ReadToEnd();
                richTextBox1.Text += responseFromServer;
                FindLink(responseFromServer);
                richTextBox1.Text += responseFromServer;

                reader.Close();
                dataStream.Close();
                response.Close();
            }
            else
            {
                textBox2.Text = "Error";
            }
        }
        //这个方法只实现简单的找到链接，并没有过滤掉#或javascript:void(0)这样的内容
        private void FindLink(string html)
        {
            textBox3.Text = "";
            List<string> hrefList = new List<string>();//链接
            List<string> nameList = new List<string>();//链接名称

            string pattern = @"<a\s*href=(""|')(?<href>[\s\S.]*?)(""|').*?>\s*(?<name>[\s\S.]*?)</a>";
            MatchCollection mc = Regex.Matches(html, pattern);
            foreach (Match m in mc)
            {
                if (m.Success)
                {
                    //加入集合数组
                    hrefList.Add(m.Groups["href"].Value);
                    nameList.Add(m.Groups["name"].Value);
                    textBox2.Text += m.Groups["href"].Value + "|" + m.Groups["name"].Value + "\n";
                }
            }
        }
        //过滤掉没有用的HTML代码，保留文字内容，基本还是正规表达式
        public string ClearHtml(string text)//过滤html,js,css代码
        {
            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            text = Regex.Replace(text, "<head[^>]*>(?:.|[\r\n])*?</head>", "");
            text = Regex.Replace(text, "<script[^>]*>(?:.|[\r\n])*?</script>", "");
            text = Regex.Replace(text, "<style[^>]*>(?:.|[\r\n])*?</style>", "");

            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", ""); //<br> 
            text = Regex.Replace(text, "\\&[a-zA-Z]{1,10};", "");
            text = Regex.Replace(text, "<[^>]*>", "");

            text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", ""); //&nbsp;
            text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty); //其它任何标记
            text = Regex.Replace(text, "[\\s]{2,}", " "); //两个或多个空格替换为一个

            text = text.Replace("'", "''");
            text = text.Replace("\r\n", "");
            text = text.Replace("  ", "");
            text = text.Replace("\t", "");
            return text.Trim();
        }
        #endregion

        #region Spider
        public class Attribute : ICloneable
        {
            string name;//属性名
            string value;//属性值
            char delim;

            public Attribute(string name0, string value0, char delim0)
            {
                name = name0;
                value = value0;
                delim = delim0;
            }
            public Attribute() : this("", "", (char)0) { }
            public Attribute(string name, string value) : this(name, value, (char)0) { }

            public object Clone()
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //string html = getHtml("http://www.baidu.com", Encoding.GetEncoding("GB2312"));
        }
    }
}
