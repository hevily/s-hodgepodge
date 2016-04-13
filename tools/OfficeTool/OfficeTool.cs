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
        private string _logFilePath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["LogFileName"]);
        private string _iniFilePath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["IniFileName"]);
        private string _iniFileSection = ConfigurationManager.AppSettings["IniFileSection"];
        private DateTime _oldTime = DateTime.Now;
        private bool _isCheckIn = false;
        private bool _isCheckOut = false;
        private delegate bool CheckInDelegate();
        private delegate bool CheckOutDelegate();

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
            ReadLog();
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            CheckOut();
            ReadLog();
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
            ReadLog();
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

        private void Init()
        {
            webBrowser1.Navigate(new Uri(ConfigurationManager.AppSettings["CheckinUrl"]));

            Thread.Sleep(500);

            //this.textBox1.Text = GetIniValue("userid");
            //this.textBox2.Text = GetIniValue("password");
            this.textBox3.Text = GetIniValue("checkintime");
            this.textBox4.Text = GetIniValue("checkouttime");
            this.textBox5.Text = GetIniValue("offsettime");
            ReadLog();

            Thread.Sleep(500);

            string errMsg = ValidateInput();
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(errMsg);
                return;
            }

            Thread.Sleep(500);

            AutoCheckInCheckOut();

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

        private bool CheckIn()
        {
            try
            {
                if (webBrowser1.InvokeRequired)
                {
                    CheckInDelegate checkInDelegate = CheckIn2;
                    return (bool)this.webBrowser1.Invoke(checkInDelegate);
                }
                else
                {
                    return CheckIn2();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                return false;
            }
        }
        private bool CheckIn2()
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
                doc.parentWindow.execScript("function alert(str){return true;} ", "javaScript");
                WriteLog("签到成功");
                return true;
            }
            WriteLog("签到失败");
            return false;

        }
        private bool CheckOut()
        {
            try
            {
                if (webBrowser1.InvokeRequired)
                {
                    CheckOutDelegate checkOutDelegate = CheckOut2;
                    return (bool)this.webBrowser1.Invoke(checkOutDelegate);
                }
                else
                {
                    return CheckOut2();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
                return false;
            }
        }
        private bool CheckOut2()
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
                doc.parentWindow.execScript("function alert(str){return true;} ", "javaScript");
                WriteLog("签退成功");
                return true;
            }
            WriteLog("签退失败");
            return false;
        }

        private void AutoCheckInCheckOut()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = int.Parse(ConfigurationManager.AppSettings["TimeInterval"]);
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

            DateTime dtCheckIn = new DateTime(now.Year, now.Month, now.Day, hourCheckIn, minuteCheckIn, 30);
            DateTime dtCheckOut = new DateTime(now.Year, now.Month, now.Day, hourCheckOut, minuteCheckOut, 30);

            // 刷新签到、签退状态
            if (_oldTime.Date != now.Date)
            {
                _isCheckIn = false;
                _isCheckOut = false;
            }
            _oldTime = now;

            // 自动签到
            if (!_isCheckIn && now > dtCheckIn)
            {
                _isCheckIn = CheckIn();
            }

            // 自动签退
            if (!_isCheckOut && now > dtCheckOut)
            {
                _isCheckOut = CheckOut();
            }

            ReadLog();

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

