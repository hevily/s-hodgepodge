using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IISJournalAnalyzer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string _Condition_RequestType = null;
        private string _Condition_RequestUrl = null;
        private string _Condition_UrlParams = null;
        private string _Condition_ServerIP = null;
        private string _Condition_ClientIP = null;
        private string _Condition_UserAgent = null;

        private void btnSelectLogFile1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "IIS日志|*.log";
            of.ShowDialog();
            if (!System.IO.File.Exists(of.FileName))
                return;

            this.txtLogFile1.Text = of.FileName;
        }

        private void btnSelectLogFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "IIS日志|*.log";
            of.ShowDialog();
            if (!System.IO.File.Exists(of.FileName))
                return;

            this.txtLogFile2.Text = of.FileName;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(this.txtLogFile1.Text))
            {
                MessageBox.Show("日志1必须选择！");
                return;
            }

            if (this.txtRequestType.Text.Length == 0
                && this.txtRequestUrl.Text.Length == 0
                && this.txtUrlParams.Text.Length == 0
                && this.txtServerIP.Text.Length == 0
                && this.txtClientIP.Text.Length == 0
                && this.txtUserAgent.Text.Length == 0)
            {
                MessageBox.Show("过滤条件不能全部为空！");
                return;
            }

            this.txtDestRows.Text = "";
            _Condition_ClientIP = this.txtClientIP.Text;
            _Condition_RequestType = this.txtRequestType.Text;
            _Condition_RequestUrl = this.txtRequestUrl.Text;
            _Condition_ServerIP = this.txtServerIP.Text;
            _Condition_UrlParams = this.txtUrlParams.Text;
            _Condition_UserAgent = this.txtUserAgent.Text;

            List<LogItem> logItems = new List<LogItem>(1000);
            LogItem tmpItem = new LogItem();
            string tmpLine = null;
            System.IO.StreamReader sr = new System.IO.StreamReader(this.txtLogFile1.Text);
            while ((tmpLine = sr.ReadLine()) != null)
            {
                if (logItems.Count >= 1000)
                {
                    MessageBox.Show("命中的日志条目超过1000，请修改查询条件以获取更精确的结果！");
                    break;
                }
                if (tmpItem.LoadData(tmpLine))
                {
                    if (CheckLogItemAboutCondition(tmpItem))
                    {
                        logItems.Add(tmpItem);
                        tmpItem = new LogItem();
                    }
                }
            }
            sr.Close();

            if (System.IO.File.Exists(this.txtLogFile2.Text))
            {
                sr = new System.IO.StreamReader(this.txtLogFile2.Text);
                while ((tmpLine = sr.ReadLine()) != null)
                {
                    if (logItems.Count >= 1000)
                    {
                        MessageBox.Show("命中的日志条目超过1000，请修改查询条件以获取更精确的结果！");
                        break;
                    }
                    if (tmpItem.LoadData(tmpLine))
                    {
                        if (CheckLogItemAboutCondition(tmpItem))
                        {
                            logItems.Add(tmpItem);
                            tmpItem = new LogItem();
                        }
                    }
                }
                sr.Close();
            }

            if (logItems.Count > 0)
            {
                var items = logItems.OrderBy(n => n.RequestTime).ToArray();
                StringBuilder itemBuffer = new StringBuilder();
                for (int i = 0; i < items.Length; i++)
                {
                    itemBuffer.Append(items[i].TheLogLine + "\r\n");
                }
                this.txtDestRows.Text = itemBuffer.ToString();
            }
        }

        /// <summary>
        /// 判断日志条目是否命中条件
        /// </summary>
        /// <param name="tmpItem"></param>
        /// <returns></returns>
        private bool CheckLogItemAboutCondition(LogItem tmpItem)
        {
            if (!string.IsNullOrEmpty(_Condition_ClientIP) && (tmpItem.ClientIP == null || !tmpItem.ClientIP.Contains(_Condition_ClientIP)))
                return false;
            if (!string.IsNullOrEmpty(_Condition_RequestType) && (tmpItem.RequestMethod == null || !tmpItem.RequestMethod.Contains(_Condition_RequestType)))
                return false;
            if (!string.IsNullOrEmpty(_Condition_RequestUrl) && (tmpItem.RequestUrl == null || !tmpItem.RequestUrl.Contains(_Condition_RequestUrl)))
                return false;
            if (!string.IsNullOrEmpty(_Condition_ServerIP) && (tmpItem.ServerIP == null || !tmpItem.ServerIP.Contains(_Condition_ServerIP)))
                return false;
            if (!string.IsNullOrEmpty(_Condition_UrlParams) && (tmpItem.UrlParams == null || !tmpItem.UrlParams.Contains(_Condition_UrlParams)))
                return false;
            if (!string.IsNullOrEmpty(_Condition_UserAgent) && (tmpItem.UserAgent == null || !tmpItem.UserAgent.Contains(_Condition_UserAgent)))
                return false;
            return true;
        }
    }
}
