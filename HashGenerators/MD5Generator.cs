using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashGenerators
{
    public static class MD5Generator
    {
        public static string ProduceMD5Hash(string sourceStr)
        {
            string hash = null;
            using (MD5 md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                byte[] sourceBytes = Encoding.UTF8.GetBytes(sourceStr);

                // Generate hash value(Byte Array) for input data
                byte[] hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }

            return hash;
        }
    }
}
