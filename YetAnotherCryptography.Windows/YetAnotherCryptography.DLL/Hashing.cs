using System.Text;
using System.Security.Cryptography;
using MurmurHash.Net;
using WyHash;

namespace YetAnotherCryptography.DLL
{
    public class Hashing
    {

        #region SHA512
        public static byte[] GenerateSHA512Hash(byte[] data)
        {
            using(SHA512 sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(data);
            }
        }

        public static byte[] GenerateSHA512Hash(string data)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }
        #endregion

        #region SHA1
        public static byte[] GenerateSHA1Hash(byte[] data)
        {
            using(SHA1 sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(data);
            }
        }

        public static byte[] GenerateSHA1Hash(string data)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                return sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }
        #endregion

        #region MurMur3

        public static byte[] GenerateMurmur3Hash(byte[] data, uint seed)
        {
            return Encoding.UTF8.GetBytes(MurmurHash3.Hash32(data, seed).ToString());
        }

        public static byte[] GenerateMurmur3Hash(string data, uint seed)
        {
            return Encoding.UTF8.GetBytes(MurmurHash3.Hash32(data.ToByte(), seed).ToString());
        }

        #endregion

        #region WyHash

        public static byte[] GenerateWyHash(byte[] data)
        {
            using(WyHash64 wyHash = WyHash64.Create())
            {
                return wyHash.ComputeHash(data);
            }
        }

        public static byte[] GenerateWyHash(string data)
        {
            using (WyHash64 wyHash = WyHash64.Create())
            {
                return wyHash.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        #endregion
    }
}
