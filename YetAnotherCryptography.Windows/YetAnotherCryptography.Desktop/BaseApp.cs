using System;
using System.Security.Cryptography.Xml;
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
            if (manager.ActiveMode() == CryptographyMode.EXCEPTION)
            {
                CLIPrinter.Print("Gebe -h ein, um eine Liste aller zulässigen Befehle zu bekommen");
                return;
            }

            string source = manager.GetValueFromArg("src");
            string password = manager.GetValueFromArg("pass");

#if DEBUG
            CLIPrinter.Print("Source: " + source);
            CLIPrinter.Print("Password: " + password);
#endif

            switch(manager.ActiveMode())
            {
                case CryptographyMode.SOURCE_ENCRYPT:
                    EncryptData(
                        source,
                        password
                    );
                    break;

                case CryptographyMode.SOURCE_DECRYPT:
                    DecryptData(
                        source,
                        password
                    );
                    break;

                case CryptographyMode.DIRECTORY_ENCRYPT:
                    string[] decryptedSources = Directory.GetFiles(manager.GetValueFromArg("src"));
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

                case CryptographyMode.DIRECTORY_DECRYPT:
                    string[] encryptedFiles = Directory.GetFiles(manager.GetValueFromArg("src"));
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

                case CryptographyMode.HELP:
                    byte[] data = cryptography.Decrypt(File.ReadAllBytes("help.yac"));
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
