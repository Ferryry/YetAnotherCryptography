using YetAnotherCryptography.DLL;

namespace YetAnotherCryptography.Desktop
{
    public enum CryptographyMode
    {
        EXCEPTION,
        HELP,
        SOURCE_ENCRYPT,
        SOURCE_DECRYPT,
        DIRECTORY_ENCRYPT,
        DIRECTORY_DECRYPT
    }

    class CommandlineInputManager
    {
        private string[] args;
        private CryptographyMode activeMode;
        private Dictionary<string, string> argsData;

        public CryptographyMode ActiveMode()
        {
            return activeMode;
        }

        public byte[]? GetPassword()
        {
            if (args[4].ToLower() == "p" && args[5].ToLower().Length > 0)
            {
                return args[5].ToByte();
            }
            return null;
        }

        public string GetValueFromArg(string arg)
        {
            return argsData.ContainsKey(arg) ? argsData[arg] : string.Empty;
        }

        private void CheckArgs()
        {
            try
            {
                if (args.Length == 0)
                {
                    activeMode = CryptographyMode.EXCEPTION;
                    return;
                }

                if (args.Length == 1 && args[0].ToLower() == "-h")
                {
                    activeMode= CryptographyMode.HELP;
                    return;
                }

                if (args.Length > 3 && args[3] == "p")
                {
                    argsData.Add("pass", args[4]);
                }

                //s = source (file), -se = source encrypt
                if (args[0].ToLower() == "s" && args[2].ToLower() == "-se")
                {
                    activeMode = CryptographyMode.SOURCE_ENCRYPT;
                    argsData.Add("src", args[1]);
                    return;
                }

                //s = source (file), -se = source decrypt
                if (args[0].ToLower() == "s" && args[2].ToLower() == "-sd")
                {
                    activeMode = CryptographyMode.SOURCE_DECRYPT;
                    argsData.Add("src", args[1]);
                    return;
                }

                //s = source (dir), -de = dir encrypt
                if (args[0].ToLower() == "s" && args[2].ToLower() == "-de")
                {
                    activeMode = CryptographyMode.DIRECTORY_ENCRYPT;
                    argsData.Add("src", args[1]);
                    return;
                }

                //s = source (dir), -se = dir decrypt
                if (args[0].ToLower() == "s" && args[2].ToLower() == "-dd")
                {
                    activeMode = CryptographyMode.DIRECTORY_DECRYPT;
                    argsData.Add("src", args[1]);
                    return;
                }

                activeMode = CryptographyMode.EXCEPTION;
            }
            catch
            {
                activeMode = CryptographyMode.EXCEPTION;
            }
        }

        private void PrepareArgs()
        {
            argsData = new Dictionary<string, string>();

            string[] fetchedArgs = args;
            args = fetchedArgs;

            CheckArgs();

#if DEBUG
            CLIPrinter.Print(">>> TOTAL ARGS: " + args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                CLIPrinter.Print(string.Format(">>>> ARG {0}: {1}", i, args[i]));
            }
            CLIPrinter.Print(">>>> ACTIVE MODE: " + activeMode.ToString());
#endif
        }

        public CommandlineInputManager(string[] args)
        {
            this.args = args;
            PrepareArgs();
        }
    }
}
