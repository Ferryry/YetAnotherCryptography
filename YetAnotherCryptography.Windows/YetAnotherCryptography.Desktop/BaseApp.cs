using System;
using YetAnotherCryptography.DLL;

namespace YetAnotherCryptography.Desktop
{
    class BaseApp
    {
        private Cryptography cryptography;
        private CommandlineInputManager manager;

        private bool EncryptData(string file, string password)
        {
            byte[] encryptedData = cryptography.Encrypt(
                File.ReadAllBytes(file),
                Hashing.GenerateHash(password.ToByte())
            );

            if (encryptedData.Length == 0)
            {
                return false;
            }

            File.WriteAllBytes(string.Format("{0}.yac", file), encryptedData);

            return true;
        }

        private bool DecryptData(string file, string password)
        {
            byte[] decryptedData = cryptography.Decrypt(
                File.ReadAllBytes(file),
                Hashing.GenerateHash(password.ToByte())
            );

            if (decryptedData.Length == 0)
            {
                return false;
            }

            File.WriteAllBytes(string.Format("{0}", Path.GetFileNameWithoutExtension(file)), decryptedData);

            return true;
        }

        public void Run()
        {
            if (manager.ActiveMode() == Keywords.Exception)
            {
                CLIPrinter.Print("Gebe -h ein, um eine Liste aller zulässigen Befehle zu bekommen");
                return;
            }

            string source = manager.GetParams("-s");
            string password = manager.GetParams("-p") ?? string.Empty;

#if DEBUG
            CLIPrinter.Print("Source: " + source);
            CLIPrinter.Print("Password: " + password);
#endif

            switch(manager.ActiveMode())
            {
                case Keywords.SourceEncrypt:
                    EncryptData(
                        source,
                        password
                    );
                    break;

                case Keywords.SourceDecrypt:
                    DecryptData(
                        source,
                        password
                    );
                    break;

                case Keywords.DirectoryEncrypt:
                    string[] decryptedSources = Directory.GetFiles(manager.GetParams("-s"));
                    for (int i = 0; i < decryptedSources.Length; i++)
                    {
                        if (!EncryptData(decryptedSources[i], password))
                        {
                            CLIPrinter.PrintError(string.Format(
                                "Die Datei {0} konnte nicht verschlüsselt werden",
                                Path.GetFileName(decryptedSources[i])
                            ));
                        }
                    }
                    break;

                case Keywords.DirectoryDecrypt:
                    string[] encryptedFiles = Directory.GetFiles(manager.GetParams("-s"));
                    for (int i = 0; i < encryptedFiles.Length; i++)
                    {
                        if(!DecryptData(encryptedFiles[i], password))
                        {
                            CLIPrinter.PrintError(string.Format(
                                "Die Datei {0} konnte nicht entschlüsselt werden",
                                Path.GetFileName(encryptedFiles[i])
                            ));
                        }
                    }
                    break;

                case Keywords.Help:
                    byte[] data = cryptography.Decrypt(File.ReadAllBytes("help.yac"), "123".ToByte());
                    CLIPrinter.Print(Utilities.ToString(data));
                    break;

                default:
                    CLIPrinter.Print("Gebe -h ein, um eine Liste aller zulässigen Befehle zu bekommen");
                    break;
            }
        }

        public BaseApp(string[] args)
        {
            cryptography = new Cryptography();
            manager = new CommandlineInputManager(args);
        }
    }
}
