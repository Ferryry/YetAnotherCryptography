﻿using System;

namespace YetAnotherCryptography
{
    public class Cryptography
    {
        private byte[]? key;

        public byte[]? GetKey() => key;

        public void SetKey(byte[] key) => this.key = key;

        public byte[] Encrypt(byte[] data) => this.Encrypt(data, new byte[0]);

        public byte[] Encrypt(byte[] data, byte[] key)
        {
            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = (byte)(data[i] ^ key[i % key.Length]);
            }

            return result;
        }

        public byte[] Decrypt(byte[] data) => this.Decrypt(data, new byte[0]);

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