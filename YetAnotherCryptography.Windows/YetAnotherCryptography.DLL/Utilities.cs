using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherCryptography.DLL
{
    public static class Utilities
    {
        public static byte[] ToByte(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static string ToString(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static byte[] ToByte(this Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static byte[] RandomPasswordGenerator()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 32; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString().ToByte();
        }
    }
}
