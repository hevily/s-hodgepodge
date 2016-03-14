using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FileRegularReplace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string[] _exts = { ".txt", ".html", ".htm" };

        private void btnSeletePath_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("请先选择路径！"); return;
            }

            var filePathList = GetFilePathList(txtPath.Text, txtRegex.Text);
            if (filePathList.Count <= 0)
            {
                listMatch.DataSource = null;
                MessageBox.Show("无匹配结果，请修改正则！");
            }
            else
            {
                listMatch.DataSource = filePathList;
            }

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text))
            {
                MessageBox.Show("请先选择路径！"); return;
            }
            if (string.IsNullOrEmpty(txtRegex.Text))
            {
                MessageBox.Show("请先填写正则表达式！"); return;
            }

            if (listMatch.Items.Count > 0)
            {
                foreach (string filePath in listMatch.Items)
                {
                    var strSource = File.ReadAllText(filePath);
                    var regex = new Regex(txtRegex.Text);
                    var strTarget = regex.Replace(strSource, txtReplace.Text);
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    File.AppendAllText(filePath, strTarget);
                }
            }

            MessageBox.Show("替换成功！");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPath.Text = "";
            txtRegex.Text = "";
            txtReplace.Text = "";
            txtSource.Text = "";
            txtTarget.Text = "";
            listMatch.Items.Clear();
        }

        private void listMatch_Click(object sender, EventArgs e)
        {
            if (listMatch.SelectedIndex != -1)
            {
                txtSource.Text = File.ReadAllText(listMatch.SelectedItem.ToString());
                var regex = new Regex(txtRegex.Text);
                txtTarget.Text = regex.Replace(txtSource.Text, txtReplace.Text);
            }
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private List<string> GetFilePathList(string path, string pattern, List<string> filePathList = null)
        {
            if (filePathList == null)
                filePathList = new List<string>();

            var dirInfo = new DirectoryInfo(path);

            var subFileInfos = dirInfo.GetFiles();
            if (subFileInfos.Length > 0)
            {
                foreach (FileInfo info in subFileInfos)
                {
                    if (!_exts.Contains(Path.GetExtension(info.FullName).ToLower()))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(pattern))
                    {
                        var regex = new Regex(pattern);
                        var match = regex.Match(File.ReadAllText(info.FullName));
                        if (match.Success)
                        {
                            filePathList.Add(info.FullName);
                        }
                    }
                    else
                    {
                        filePathList.Add(info.FullName);
                    }
                }
            }

            var subDirInfos = dirInfo.GetDirectories();
            if (subDirInfos.Length > 0)
            {
                foreach (DirectoryInfo info in subDirInfos)
                {
                    GetFilePathList(info.FullName, pattern, filePathList);
                }
            }

            return filePathList;
        }


    }
}
