namespace YetAnotherCryptography.Desktop
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            BaseApp app = new BaseApp(args);
            app.Run();
        }
    }
}