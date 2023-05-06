using System;

namespace YetAnotherCryptography.Desktop
{
    public class IOHelper
    {
        public static void WriteFile(byte[] data, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
        }

        public static byte[] ReadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                int readBytes = fs.Read(buffer, 0, buffer.Length);

                if (readBytes != buffer.Length)
                {
                    throw new IOException("Fehler beim lesen");
                }

                fs.Close();

                return buffer;
            }
        }
    }
}
