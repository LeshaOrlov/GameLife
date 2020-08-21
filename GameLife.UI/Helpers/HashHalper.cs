using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.UI
{
    public static class HashHalper
    {
        public static string GetHash(this object obj)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(obj.ObjectToByteArray());
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
            
        }
    }
}
