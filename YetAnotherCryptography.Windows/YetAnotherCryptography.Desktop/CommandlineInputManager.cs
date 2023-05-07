using YetAnotherCryptography.DLL;

namespace YetAnotherCryptography.Desktop
{
    public enum Keywords : int
    {
        Source = 0,
        Password = 1,
        SourceEncrypt = 2,
        SourceDecrypt = 3,
        DirectoryEncrypt = 4,
        DirectoryDecrypt = 5,
        Exception = 6,
        Help = 7,
    }

    class CommandlineInputManager
    {
        private string[] givenParameters;
        private Dictionary<string, Keywords> argumentsKeywordList;
        private Dictionary<string, string> filteredParameters;
        private Keywords activeMode;

        public Keywords ActiveMode()
        {
            return activeMode;
        }

        public string? GetParams(string key)
        {
            return filteredParameters.ContainsKey(key) ? filteredParameters[key] : null;
        }

        private void CheckArgs()
        {
            filteredParameters = new Dictionary<string, string>();

            try
            {
                if (givenParameters.Length == 0)
                {
                    activeMode = Keywords.Exception;
                    return;
                }

                for (int i = 0; i < givenParameters.Length; i++)
                {

                    if (givenParameters[i].ToLower() == "-h")
                    {
                        activeMode = Keywords.Help;
                        return;
                    }

                    foreach (var item in argumentsKeywordList)
                    {
                        if (givenParameters[i].ToLower() == item.Key)
                        {
                            filteredParameters.Add(item.Key, givenParameters[i + 1]);
                        }
                    }
                }

                foreach (var item in filteredParameters)
                {
                    if (item.Key == "-se")
                    {
                        activeMode = Keywords.SourceEncrypt;
                        return;
                    }

                    if (item.Key == "-sd")
                    {
                        activeMode = Keywords.SourceDecrypt;
                        return;
                    }
                    if (item.Key == "-de")
                    {
                        activeMode = Keywords.DirectoryEncrypt;
                        return;
                    }
                    if (item.Key == "-dd")
                    {
                        activeMode = Keywords.DirectoryDecrypt;
                        return;
                    }
                }

                activeMode = Keywords.Exception;
            }
            catch (Exception exc)
            {
                CLIPrinter.PrintError(exc.Message);
                activeMode = Keywords.Exception;
            }
        }

        private void PrepareArgs()
        {
            argumentsKeywordList = new Dictionary<string, Keywords>
            {
                { "-s", Keywords.Source },
                { "-p", Keywords.Password },
                { "-h", Keywords.Help },
                { "-se", Keywords.SourceEncrypt },
                { "-sd", Keywords.SourceDecrypt },
                { "-de", Keywords.DirectoryEncrypt },
                { "-dd", Keywords.DirectoryDecrypt },
            };

            CheckArgs();

#if DEBUG
            CLIPrinter.Print(">>> TOTAL ARGS: " + givenParameters.Length);
            for (int i = 0; i < givenParameters.Length; i++)
            {
                CLIPrinter.Print(string.Format(">>>> ARG {0}: {1}", i, givenParameters[i]));
            }
            CLIPrinter.Print(">>>> ACTIVE MODE: " + activeMode.ToString());
#endif
        }

        public CommandlineInputManager(string[] args)
        {
            givenParameters = args;
            PrepareArgs();
        }
    }
}
