using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OfficeTool
{
    public class VerifyCode_Login
    {
        private static string _dbPath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["VerifyCodeDB_Login"]);

        public static string Spot(Bitmap bmp, int digit)
        {
            try
            {
                var result = string.Empty;

                // 遍历验证码图片的像素点，灰度化，二值化
                bmp = ProcessBmp(bmp);

                //遍历验证码图片的像素点，分割验证码
                var list = SegmentBmp(bmp);

                var database = ReadDB();

                var curErrorCount = 0; // 当前错误次数
                var minErrorCount = 0; // 最小错误次数

                for (int i = 0; i < list.Count; i++)
                {
                    var arr1 = list[i].ToCharArray();

                    minErrorCount = 10000;
                    var tmpKey = string.Empty;
                    foreach (var keyValuePair in database)
                    {
                        var arr2 = keyValuePair.Value.ToCharArray();
                        for (int j = 0; j < arr2.Length; j++)
                        {
                            if (arr2.Length != arr1.Length || arr2[j] != arr1[j])
                            {
                                curErrorCount++;
                            }
                        }

                        if (curErrorCount < minErrorCount)
                        {
                            tmpKey = keyValuePair.Key.Substring(0, 1);
                            minErrorCount = curErrorCount;
                        }

                        curErrorCount = 0;
                    }

                    result += tmpKey;
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 遍历验证码图片的像素点，灰度化，二值化
        /// </summary>
        public static Bitmap ProcessBmp(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //取位图某点颜色
                    Color color = bmp.GetPixel(i, j);

                    //灰度化
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);

                    //二值化，v为阈值，大于等于阈值的设为白色，否者设为黑色
                    if (gray * 0.2125 + gray * 0.7154 + gray * 0.0721 >= 150)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }

            ClearNoise(bmp, GetDgGrayValue(bmp), 3);

            return bmp;
        }

        /// <summary>
        /// 遍历验证码图片的像素点，分割验证码
        /// </summary>
        public static List<string> SegmentBmp(Bitmap bmp)
        {
            List<string> list = new List<string>();

            string temp = "", temp2 = "";
            for (int i = 0; i < bmp.Width; i++)
            {
                temp2 = "";
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    if (color.A == 0 || color.R == 0 || color.G == 0 || color.B == 0)
                    {
                        temp2 += "1";
                    }
                    else
                    {
                        temp2 += "0";
                    }
                }

                // 如果某一列全部是0，则图片间的空行，不纳入识别
                if (temp2.IndexOf('1') == -1)
                {
                    if (!string.IsNullOrEmpty(temp))
                    {
                        list.Add(temp.TrimEnd('|'));
                    }
                    temp = "";
                }
                else
                {
                    temp += temp2 + "|";
                }
            }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!list[i].Contains('|'))
                {
                    list.RemoveAt(i);
                }
            }

            return list;
        }

        /// <summary>
        ///  去掉杂点（适合杂点/杂线粗为1）
        /// </summary>
        /// <param name="dgGrayValue">背前景灰色界限</param>
        /// <returns></returns>
        public static void ClearNoise(Bitmap bmp, int dgGrayValue, int MaxNearPoints)
        {
            Color piexl;
            int nearDots = 0;
            int XSpan, YSpan, tmpX, tmpY;
            //逐点判断
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    piexl = bmp.GetPixel(i, j);
                    if (piexl.R < dgGrayValue)
                    {
                        nearDots = 0;
                        //判断周围8个点是否全为空
                        if (i == 0 || i == bmp.Width - 1 || j == 0 || j == bmp.Height - 1)  //边框全去掉
                        {
                            bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                        else
                        {
                            if (bmp.GetPixel(i - 1, j - 1).R < dgGrayValue) nearDots++;
                            if (bmp.GetPixel(i, j - 1).R < dgGrayValue) nearDots++;
                            if (bmp.GetPixel(i + 1, j - 1).R < dgGrayValue) nearDots++;
                            if (bmp.GetPixel(i - 1, j).R < dgGrayValue) nearDots++;
                            if (bmp.GetPixel(i + 1, j).R < dgGrayValue) nearDots++;
                            if (bmp.GetPixel(i - 1, j + 1).R < dgGrayValue) nearDots++;
                            if (bmp.GetPixel(i, j + 1).R < dgGrayValue) nearDots++;
                            if (bmp.GetPixel(i + 1, j + 1).R < dgGrayValue) nearDots++;
                        }

                        if (nearDots < MaxNearPoints)
                            bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));   //去掉单点 && 粗细小3邻边点
                    }
                    else  //背景
                        bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                }
        }

        /// <summary>
        /// 3×3中值滤波除杂，yuanbao,2007.10
        /// </summary>
        /// <param name="dgGrayValue"></param>
        public static void ClearNoise(Bitmap bmp, int dgGrayValue)
        {
            int x, y;
            byte[] p = new byte[9]; //最小处理窗口3*3
            byte s;
            //byte[] lpTemp=new BYTE[nByteWidth*nHeight];
            int i, j;

            //--!!!!!!!!!!!!!!下面开始窗口为3×3中值滤波!!!!!!!!!!!!!!!!
            for (y = 1; y < bmp.Height - 1; y++) //--第一行和最后一行无法取窗口
            {
                for (x = 1; x < bmp.Width - 1; x++)
                {
                    //取9个点的值
                    p[0] = bmp.GetPixel(x - 1, y - 1).R;
                    p[1] = bmp.GetPixel(x, y - 1).R;
                    p[2] = bmp.GetPixel(x + 1, y - 1).R;
                    p[3] = bmp.GetPixel(x - 1, y).R;
                    p[4] = bmp.GetPixel(x, y).R;
                    p[5] = bmp.GetPixel(x + 1, y).R;
                    p[6] = bmp.GetPixel(x - 1, y + 1).R;
                    p[7] = bmp.GetPixel(x, y + 1).R;
                    p[8] = bmp.GetPixel(x + 1, y + 1).R;
                    //计算中值
                    for (j = 0; j < 5; j++)
                    {
                        for (i = j + 1; i < 9; i++)
                        {
                            if (p[j] > p[i])
                            {
                                s = p[j];
                                p[j] = p[i];
                                p[i] = s;
                            }
                        }
                    }
                    //      if (bmp.GetPixel(x, y).R < dgGrayValue)
                    bmp.SetPixel(x, y, Color.FromArgb(p[4], p[4], p[4]));    //给有效值付中值
                }
            }
        }

        /// <summary>
        /// 得到灰度图像前景背景的临界值 最大类间方差法，yuanbao,2007.08
        /// </summary>
        /// <returns>前景背景的临界值</returns>
        public static int GetDgGrayValue(Bitmap bmp)
        {
            int[] pixelNum = new int[256];           //图象直方图，共256个点
            int n, n1, n2;
            int total;                              //total为总和，累计值
            double m1, m2, sum, csum, fmax, sb;     //sb为类间方差，fmax存储最大方差值
            int k, t, q;
            int threshValue = 1;                      // 阈值
            int step = 1;
            //生成直方图
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //返回各个点的颜色，以RGB表示
                    pixelNum[bmp.GetPixel(i, j).R]++;            //相应的直方图加1
                }
            }
            //直方图平滑化
            for (k = 0; k <= 255; k++)
            {
                total = 0;
                for (t = -2; t <= 2; t++)              //与附近2个灰度做平滑化，t值应取较小的值
                {
                    q = k + t;
                    if (q < 0)                     //越界处理
                        q = 0;
                    if (q > 255)
                        q = 255;
                    total = total + pixelNum[q];    //total为总和，累计值
                }
                pixelNum[k] = (int)((float)total / 5.0 + 0.5);    //平滑化，左边2个+中间1个+右边2个灰度，共5个，所以总和除以5，后面加0.5是用修正值
            }
            //求阈值
            sum = csum = 0.0;
            n = 0;
            //计算总的图象的点数和质量矩，为后面的计算做准备
            for (k = 0; k <= 255; k++)
            {
                sum += (double)k * (double)pixelNum[k];     //x*f(x)质量矩，也就是每个灰度的值乘以其点数（归一化后为概率），sum为其总和
                n += pixelNum[k];                       //n为图象总的点数，归一化后就是累积概率
            }

            fmax = -1.0;                          //类间方差sb不可能为负，所以fmax初始值为-1不影响计算的进行
            n1 = 0;
            for (k = 0; k < 256; k++)                  //对每个灰度（从0到255）计算一次分割后的类间方差sb
            {
                n1 += pixelNum[k];                //n1为在当前阈值遍前景图象的点数
                if (n1 == 0) { continue; }            //没有分出前景后景
                n2 = n - n1;                        //n2为背景图象的点数
                if (n2 == 0) { break; }               //n2为0表示全部都是后景图象，与n1=0情况类似，之后的遍历不可能使前景点数增加，所以此时可以退出循环
                csum += (double)k * pixelNum[k];    //前景的“灰度的值*其点数”的总和
                m1 = csum / n1;                     //m1为前景的平均灰度
                m2 = (sum - csum) / n2;               //m2为背景的平均灰度
                sb = (double)n1 * (double)n2 * (m1 - m2) * (m1 - m2);   //sb为类间方差
                if (sb > fmax)                  //如果算出的类间方差大于前一次算出的类间方差
                {
                    fmax = sb;                    //fmax始终为最大类间方差（otsu）
                    threshValue = k;              //取最大类间方差时对应的灰度的k就是最佳阈值
                }
            }
            return threshValue;
        }

        public static Dictionary<string, string> ReadDB()
        {
            if (!File.Exists(_dbPath)) { using (File.Create(_dbPath)) { } }

            var db = new Dictionary<string, string>();
            using (var sr = File.OpenText(_dbPath))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line.Trim()) && line.Contains("="))
                    {
                        db.Add(line.Split('=')[0], line.Split('=')[1]);
                    }
                }
            }
            return db;
        }

        public static void WriteDB(string key, string value)
        {
            if (!File.Exists(_dbPath)) { using (File.Create(_dbPath)) { } }

            var db = ReadDB();
            if (!db.Values.Contains(value))
            {
                using (var sw = File.AppendText(_dbPath))
                {
                    sw.WriteLine(key + "_" + Guid.NewGuid() + "=" + value);
                }
            }
        }
    }
}
