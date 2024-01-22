using System.Text;
using YetAnotherCryptography.DLL;

namespace YetAnotherCryptography
{
    public enum HashingAlgorithm : uint
    {
        WyHash, SHA512, SHA3, MurMur3
    }

    public class Cryptography
    {
        private byte[]? key;

        public byte[]? GetKey() => key;

        public void SetKey(byte[] key) => this.key = key;

        private byte[] HashKey(HashingAlgorithm algorithm, byte[] key = String.Empty.ToByte())
        {
            switch (algorithm)
            {
                case HashingAlgorithm.WyHash:
                    return Hashing.GenerateWyHash(key);

                case HashingAlgorithm.MurMur3:
                    return Hashing.GenerateMurmur3Hash(key, 1);

                case HashingAlgorithm.SHA512:
                    return Hashing.GenerateSHA512Hash(key);

                case HashingAlgorithm.SHA3:
                    return Hashing.GenerateSHA1Hash(key);

                default:
                    return Hashing.GenerateWyHash(key);
            }
        }

        public byte[] Encrypt(byte[] data) => this.Encrypt(data, "".ToByte());

        public byte[] Encrypt(byte[] data, byte[] key)
        {
            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                key = i % 2 == 0 ? key.Reverse().ToArray() : HashKey(HashingAlgorithm.SHA3, key);

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
                key = i % 2 == 0 ? key.Reverse().ToArray() : Hashing.GenerateHash(key);

                result[i] = (byte)(data[i] ^ key[i % key.Length]);
            }

            return result;
        }
    }
}
