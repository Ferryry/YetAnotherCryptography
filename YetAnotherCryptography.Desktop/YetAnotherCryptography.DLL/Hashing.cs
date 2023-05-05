using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace YetAnotherCryptography.DLL
{
    public class Hashing
    {
        public static byte[] GenerateHash(byte[] data)
        {
            using(SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(data);
                BigInteger hashValue = new BigInteger(hashBytes);
                string hashString = hashValue.ToString("X");

                while (hashString.Length < 256)
                {
                    hashString = "0" + hashString;
                }

                return hashBytes;
            }
        }
    }
}
