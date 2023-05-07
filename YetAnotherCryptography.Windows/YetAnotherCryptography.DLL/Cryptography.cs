using System;
using YetAnotherCryptography.DLL;

namespace YetAnotherCryptography
{
    public class Cryptography
    {
        private byte[]? key;

        public byte[]? GetKey() => key;

        public void SetKey(byte[] key) => this.key = key;

        public byte[] Encrypt(byte[] data) => this.Encrypt(data, "".ToByte());

        public byte[] Encrypt(byte[] data, byte[] key)
        {
            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ key[i % key.Length]);
            }

            return result;
        }

        public byte[] Decrypt(byte[] data) => this.Decrypt(data, "".ToByte());

        public byte[] Decrypt(byte[] data, byte[] key)
        {
            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ key[i % key.Length]);
            }

            return result;
        }
    }
}
