using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FolderComparator
{
    public partial class DirCompare : Form
    {
        public DirCompare()
        {
            InitializeComponent();
        }

        private void btnPath1_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tPath1.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnPath2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tPath2.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
                return;

            this.tvPath1.Nodes.Clear();
            this.tvPath2.Nodes.Clear();

            ShowFolderList(this.tPath1.Text, this.tvPath1.Nodes);
            ShowFolderList(this.tPath2.Text, this.tvPath2.Nodes);
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
                return;

            this.tvPath1.Nodes.Clear();
            this.tvPath2.Nodes.Clear();

            CompareDir(this.tPath1.Text, this.tPath2.Text, this.tvPath1.Nodes, this.tvPath2.Nodes);

            if (this.chkHiddenSame.Checked)
            {
                DeleteSame(this.tvPath1.Nodes);
                DeleteSame(this.tvPath2.Nodes);
            }
        }

        private void btnClearPath_Click(object sender, EventArgs e)
        {
            this.tPath1.Text = string.Empty;
            this.tPath2.Text = string.Empty;
            this.chkHiddenSame.Checked = false;
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            this.tPath1.Text = ConfigurationManager.AppSettings["path1"];
            this.tPath2.Text = ConfigurationManager.AppSettings["path2"];
            this.chkHiddenSame.Checked = ConfigurationManager.AppSettings["hiddenSame"] == "1" ? true : false;
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        private bool CheckInput()
        {
            string strPath1 = this.tPath1.Text;
            string strPath2 = this.tPath2.Text;

            if (string.IsNullOrEmpty(strPath1) || string.IsNullOrEmpty(strPath2))
            {
                MessageBox.Show("请先选择路径！");
                return false;
            }

            if (!Directory.Exists(strPath1))
            {
                MessageBox.Show("路径1不存在，请重新选择！");
                return false;
            }

            if (!Directory.Exists(strPath2))
            {
                MessageBox.Show("路径2不存在，请重新选择！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 递归显示文件夹结构
        /// </summary>
        private void ShowFolderList(string path, TreeNodeCollection tvc)
        {
            var dirInfo = new DirectoryInfo(path);
            var dirs = dirInfo.GetDirectories();
            var files = dirInfo.GetFiles();

            foreach (var dir in dirs)
            {
                var node = new TreeNode() { Text = "[" + dir.Name + "]" };
                ShowFolderList(dir.FullName, node.Nodes);
                tvc.Add(node);
            }

            foreach (var file in files)
            {
                tvc.Add(new TreeNode() { Text = file.Name });
            }
        }

        /// <summary>
        /// 比较两个文件的内容
        /// </summary>
        private void CompareDir(string path1, string path2, TreeNodeCollection tvc1, TreeNodeCollection tvc2)
        {
            var dirInfo1 = new DirectoryInfo(path1);
            var dirs1 = dirInfo1.GetDirectories();
            var files1 = dirInfo1.GetFiles();

            var dirInfo2 = new DirectoryInfo(path2);
            var dirs2 = dirInfo2.GetDirectories();
            var files2 = dirInfo2.GetFiles();

            Hashtable hsDir = new Hashtable();
            foreach (var dir2 in dirs2)
            {
                hsDir.Add(dir2.Name, dir2);
            }

            foreach (var dir1 in dirs1)
            {
                if (hsDir.ContainsKey(dir1.Name))
                {
                    TreeNode node1 = new TreeNode() { Text = "[" + dir1.Name + "]" };
                    TreeNode node2 = (TreeNode)node1.Clone();
                    CompareDir(dir1.FullName, ((DirectoryInfo)hsDir[dir1.Name]).FullName, node1.Nodes, node2.Nodes);
                    tvc1.Add(node1);
                    tvc2.Add(node2);
                    hsDir.Remove(dir1.Name);
                }
                else
                {
                    tvc1.Add(new TreeNode() { Text = "(new)[" + dir1.Name + "]", ForeColor = Color.Red });
                }
            }

            foreach (var dir in hsDir.Values)
            {
                tvc2.Add(new TreeNode() { Text = "(new)[" + dir.ToString() + "]", ForeColor = Color.Red });
            }

            Hashtable hsFile = new Hashtable();
            foreach (FileInfo file2 in files2)
            {
                hsFile.Add(file2.Name, file2);
            }

            foreach (FileInfo file1 in files1)
            {
                if (hsFile.ContainsKey(file1.Name))
                {
                    FileInfo file2 = (FileInfo)hsFile[file1.Name];
                    if (file1.Length != file2.Length)
                    {
                        tvc1.Add(new TreeNode() { Text = file1.Name, ForeColor = Color.Red });
                        tvc2.Add(new TreeNode() { Text = file2.Name, ForeColor = Color.Red });
                    }
                    else
                    {
                        TreeNode node1 = new TreeNode() { Text = file1.Name };
                        TreeNode node2 = (TreeNode)node1.Clone();
                        tvc1.Add(node1);
                        tvc2.Add(node2);
                    }
                    hsFile.Remove(file1.Name);
                }
                else
                {
                    tvc1.Add(new TreeNode() { Text = "(new)" + file1.Name + "", ForeColor = Color.Red });
                }
            }

            foreach (var file in hsFile.Values)
            {
                tvc2.Add(new TreeNode() { Text = "(new)" + file.ToString() + "", ForeColor = Color.Red });
            }
        }

        /// <summary>
        /// 删除相同的节点
        /// </summary>
        private void DeleteSame(TreeNodeCollection tvc)
        {
            int count = 0;
            List<TreeNode> list = new List<TreeNode>();
            foreach (TreeNode node in tvc)
            {
                DeleteSame(node.Nodes);
                if (node.ForeColor == Color.Red || node.Nodes.Count > 0)
                    list.Add(node);
                count = count + node.Nodes.Count;
            }

            if (list.Count == 0 && count == 0)
            {
                tvc.Clear();
            }
            else
            {
                int len = tvc.Count;
                for (int i = len - 1; i >= 0; i--)
                {
                    if (!list.Contains(tvc[i]))
                        tvc.Remove(tvc[i]);
                }
            }
        }
    }
}
