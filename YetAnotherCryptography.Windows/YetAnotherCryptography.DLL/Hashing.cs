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
                return sha512.ComputeHash(data);
            }
        }
    }
}
