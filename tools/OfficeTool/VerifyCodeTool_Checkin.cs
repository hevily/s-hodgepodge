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
    public partial class VerifyCodeTool_Checkin : Form
    {

        Dictionary<string, string> _result;

        public VerifyCodeTool_Checkin()
        {
            InitializeComponent();
            webBrowser1.Navigate(new Uri(ConfigurationManager.AppSettings["CheckinUrl"]));
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var id = ConfigurationManager.AppSettings["ImgId"];

            var img = webBrowser1.Document.GetElementById(id);
            img.InvokeMember("click");

            HTMLDocument html = (HTMLDocument)this.webBrowser1.Document.DomDocument;
            IHTMLControlElement img2 = (IHTMLControlElement)webBrowser1.Document.Images[id].DomElement;
            IHTMLControlRange range = (IHTMLControlRange)((HTMLBody)html.body).createControlRange();

            range.add(img2);
            range.execCommand("Copy", false, null);

            if (Clipboard.ContainsImage())
            {
                pictureBox1.Image = Clipboard.GetImage();
            }
            else
            {
                MessageBox.Show("获取验证码失败");
            }

            Clipboard.Clear();

            pictureBox2.Image = VerifyCode_Checkin.ProcessBmp(new Bitmap(pictureBox1.Image));
            textBox1.Text = VerifyCode_Checkin.Spot(new Bitmap(pictureBox1.Image), 4);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var list = VerifyCode_Checkin.SegmentBmp(new Bitmap(pictureBox2.Image));
            if (!string.IsNullOrEmpty(textBox1.Text) && list != null)
            {
                var arr = textBox1.Text.ToCharArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    VerifyCode_Checkin.WriteDB(arr[i].ToString(), list[i]);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            var result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                var name = fbd.SelectedPath + "/" + Guid.NewGuid() + ".jpg";
                pictureBox1.Image.Save(name);
                MessageBox.Show("保存成功");
            }
        }
    }
}

