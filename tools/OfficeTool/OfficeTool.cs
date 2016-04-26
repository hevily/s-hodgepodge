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

namespace OfficeTool
{
    public partial class OfficeTool : Form
    {
        #region 全局变量

        private string _logFilePath = string.Empty;
        private string _iniFilePath = string.Empty;
        private string _iniFileSection = string.Empty;
        private string _checkinUrl = string.Empty;

        private int _timeInterval = 30000;
        private int _delayResult = 1000;
        private int _checkInOutTimes = 3;

        private DateTime _oldTime = DateTime.Now;
        private int _isCheckInCount = 0;
        private int _isCheckOutCount = 0;
        private bool _isCheckIn = false;
        private bool _isCheckOut = false;
        private bool _isCheckIning = false;
        private bool _isCheckOuting = false;

        private List<string> _listNotWorkDay = null;
        private string _checkInRepeat = string.Empty;
        private string _checkInSuccess = string.Empty;
        private string _checkOutEarly = string.Empty;
        private string _checkOutSuccess = string.Empty;

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
                //SetIniValue("userid", this.textBox1.Text);
                //SetIniValue("password", this.textBox2.Text);
                SetIniValue("checkintime", this.textBox3.Text);
                SetIniValue("checkouttime", this.textBox4.Text);
                SetIniValue("offsettime", this.textBox5.Text);
                WriteLog("保存成功");
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
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.AbsoluteUri == _checkinUrl)
            {
                AutoCheckInCheckOut();
            }
        }

        private void Init()
        {
            // 读取配置文件
            _logFilePath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["LogFileName"]);
            _iniFilePath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["IniFileName"]);
            _iniFileSection = ConfigurationManager.AppSettings["IniFileSection"];
            _checkinUrl = ConfigurationManager.AppSettings["CheckinUrl"];

            _listNotWorkDay = GetWorkDayList(Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["NotWorkDay"]));
            _checkInRepeat = ConfigurationManager.AppSettings["CheckInRepeat"];
            _checkInSuccess = ConfigurationManager.AppSettings["CheckInSuccess"];
            _checkOutEarly = ConfigurationManager.AppSettings["CheckOutEarly"];
            _checkOutSuccess = ConfigurationManager.AppSettings["CheckOutSuccess"];

            int.TryParse(ConfigurationManager.AppSettings["TimeInterval"], out _timeInterval);
            int.TryParse(ConfigurationManager.AppSettings["DelayResult"], out _delayResult);
            int.TryParse(ConfigurationManager.AppSettings["CheckInOutTimes"], out _checkInOutTimes);

            // 跳转到目标网址
            webBrowser1.Navigate(new Uri(_checkinUrl));

            this.textBox3.Text = GetIniValue("checkintime");
            this.textBox4.Text = GetIniValue("checkouttime");
            this.textBox5.Text = GetIniValue("offsettime");

            ReadLog();

            string errMsg = ValidateInput();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }
        }
        private string ValidateInput()
        {
            int intTmep = 0;
            string errMsg = string.Empty;

            //if (this.textBox1.Text.Length != 6 || !int.TryParse(this.textBox1.Text, out intTmep))
            //{
            //    errMsg += "员工号必须为6位数字\r\n";
            //}

            //if (string.IsNullOrEmpty(this.textBox2.Text))
            //{
            //    errMsg += "密码不能为空\r\n";
            //}

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

            return errMsg;
        }

        private void CheckIn()
        {
            try
            {
                if (webBrowser1.InvokeRequired)
                {
                    this.webBrowser1.Invoke(new MethodInvoker(delegate { CheckIn2(); }));
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
            var txt = webBrowser1.Document.GetElementById(txtId);
            var btn = webBrowser1.Document.GetElementById(btnId);
            var doc = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            if (txt != null && btn != null && doc != null)
            {
                var code = VerificationCode.Spot(GetVerificationImage(), 4);
                txt.SetAttribute("value", code);
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
                    this.webBrowser1.Invoke(new MethodInvoker(delegate { CheckOut2(); }));
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
            var txt = webBrowser1.Document.GetElementById(txtId);
            var btn = webBrowser1.Document.GetElementById(btnId);
            var doc = (IHTMLDocument2)webBrowser1.Document.DomDocument;
            if (txt != null && btn != null && doc != null)
            {
                var code = VerificationCode.Spot(GetVerificationImage(), 4);
                txt.SetAttribute("value", code);
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
                if (html.Contains(_checkInSuccess))
                {
                    WriteLog("签到成功");
                    _isCheckIn = true;
                }
                else if (html.Contains(_checkInRepeat))
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
                if (html.Contains(_checkOutEarly))
                {
                    WriteLog("您已早退");
                    _isCheckOut = false;
                }
                else if (html.Contains(_checkOutSuccess))
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
        }

        private void AutoCheckInCheckOut()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = _timeInterval;
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
            }
            _oldTime = now;

            // 自动签到
            if (!_isCheckIn && now > dtCheckIn && _isCheckInCount < _checkInOutTimes)
            {
                CheckIn();
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

        private Bitmap GetVerificationImage()
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
            if (!File.Exists(_iniFilePath))
            {
                using (File.Create(_iniFilePath)) { }
            }

            var result = WritePrivateProfileString(_iniFileSection, key, val, _iniFilePath);
        }

        /// <summary>
        /// 自定义读取INI文件中的内容方法
        /// </summary>
        /// <param name="Section">键</param>
        /// <param name="key">值</param>
        /// <returns></returns>
        private string GetIniValue(string key)
        {
            if (!File.Exists(_iniFilePath))
            {
                using (File.Create(_iniFilePath)) { }
            }

            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(_iniFileSection, key, "", temp, 1024, _iniFilePath);
            return temp.ToString();
        }

        #endregion


    }
}

