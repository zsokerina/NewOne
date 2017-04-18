using System;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ready = @"C:\Users\ПК\Downloads\Ready\";
            if (Directory.Exists(ready)==true)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(ready);

                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
                Directory.Delete(ready);
            }
            DirectoryInfo di = Directory.CreateDirectory(ready);

            ProcessStartInfo ps = new ProcessStartInfo();
            DirectoryInfo allfile = new DirectoryInfo(@"C:\Users\ПК\Downloads\AllFile\");
            foreach (var item in allfile.GetFiles())
            {
                ps.FileName = @"C:\Program Files (x86)\WinRAR\WinRAR.exe";
                ps.Arguments = @"x " + item.FullName + " " + ready;
                Process.Start(ps);
            }
        }
    }
}
