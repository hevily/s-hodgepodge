using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OfficeTool
{
    public partial class GenNotWorkDay : Form
    {
        public GenNotWorkDay()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var strYear = this.textBox2.Text;
            var intYear = 0;
            if (strYear.Length != 4 || !int.TryParse(strYear, out intYear))
            {
                MessageBox.Show("年份格式不正确！");
                return;
            }

            var result = string.Empty;
            DateTime dt = new DateTime(intYear, 1, 1);

            while (true)
            {
                if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    result += dt.ToString("yyyyMMdd") + "=周末\r\n";
                }

                dt = dt.AddDays(1);

                if (dt.Year != intYear)
                {
                    break;
                }
            }

            this.textBox1.Text = result;
        }
    }
}
