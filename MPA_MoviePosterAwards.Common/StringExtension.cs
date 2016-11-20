using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 指示指定的字符串是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsBlank(this string s)
        {
            if (string.IsNullOrEmpty(s) || s.Trim().Length == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 去除每个段落首尾的空白
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimAll(this string s)
        {
            string[] strlist = s.Split('\n');
            StringBuilder str = new StringBuilder();
            foreach (var item in strlist)
            {
                if (item.Trim().Length > 0)
                {
                    str.Append(item.Trim()).Append("\n");
                }
            }
            return str.Length == 0 ? string.Empty : str.Remove(str.Length - 1, 1).ToString();
        }

        /// <summary>
        /// 除去字符/之间的空白
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimSplit(this string s)

        {
            string[] strlist = s.Split('/');
            StringBuilder str = new StringBuilder();
            foreach (var item in strlist)
            {
                str.Append(item.Trim()).Append("/");
            }
            return str.Length == 0 ? string.Empty : str.Remove(str.Length - 1, 1).ToString();
        }

        public static string DESEncryption(this string s)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputeByteArray = Encoding.Default.GetBytes(s);
            des.Key = ASCIIEncoding.ASCII.GetBytes("abcdefgh");
            des.IV = ASCIIEncoding.ASCII.GetBytes("abcdefgh");
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputeByteArray, 0, inputeByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
    }
}
