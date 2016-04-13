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
    public class VerificationCode
    {
        private static string _dbPath = Path.Combine(Application.StartupPath, ConfigurationManager.AppSettings["VerificationCodeDB"]);

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
                    if (gray * 0.2125 + gray * 0.7154 + gray * 0.0721 >= 100)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
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

            return list;
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
