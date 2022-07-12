using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BelediyeProject.Helpers
{
    public class CryptoHelper
    {
        public static string Sifrele(string pSifre)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bsifre = md5.ComputeHash(Encoding.UTF8.GetBytes(pSifre));
            StringBuilder sb = new StringBuilder();
            foreach (var by in bsifre)
            {
                sb.Append(by.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}