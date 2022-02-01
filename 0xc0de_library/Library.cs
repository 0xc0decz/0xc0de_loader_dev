using System;
using System.IO;
using System.Reflection;

namespace _0xc0de_library
{
    public class Library
    {
        private string GetTime()
        {
            var time = DateTime.Now;
            const string format = "HH:mm:ss";
            return time.ToString(format);
        }

        public void _msg(string line, string filename = null)
        {
            if (filename != null)
                using (TextWriter tw = File.AppendText("Loader/" + filename))
                {
                    tw.WriteLine("[ " + GetTime() + " ] " + "0xc0de_LOG : " + line);
                    //tw.Flush();
                }
            else
                using (TextWriter tw = File.AppendText("Loader/Log.txt"))
                {
                    tw.WriteLine("[ " + GetTime() + " ] " + "0xc0de_LOG : " + line);
                    //tw.Flush();
                }
        }

        public void CreateDir(string path, string foldername)
        {
            var di = Directory.CreateDirectory(path + "/" + foldername);
        }

        public int GetAllFile(string path, string name = null, string ext = null)
        {
            var fileCount = 0;
            if (name != null && ext == null)
                fileCount = Directory.GetFiles(path, name + ".dll", SearchOption.TopDirectoryOnly)
                    .Length;

            if (name == null && ext != null)
                fileCount = Directory.GetFiles(path, "*." + ext, SearchOption.TopDirectoryOnly)
                    .Length;

            if (name != null && ext != null)
                fileCount = Directory.GetFiles(path, name + "." + ext, SearchOption.TopDirectoryOnly)
                    .Length;

            if (name == null && ext == null)
                fileCount = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                    .Length;


            return fileCount;
        }
    }

    public class Libs
    {
        private readonly Library library = new Library();

        private string LibsDirectory { get; set; }

        public void Init()
        {
            Load();
        }

        private void Load()
        {
            LibsDirectory = "Loader/Libs/";
            CheckLibs();
            GetLibs();
        }

        private void GetLibs()
        {
            var di = new DirectoryInfo(LibsDirectory);
            var files = di.GetFiles("*.dll");
            foreach (var file in files)
            {
                //Bridge._library._msg(file.Name);
                var assembly = Assembly.LoadFile(LibsDirectory + file.Name);
            }
        }

        private void CheckLibs()
        {
            if (Directory.Exists(LibsDirectory)) return;
            library._msg("Libs Folder not exists - " + LibsDirectory);
            CreateLibs();
        }

        private void CreateLibs()
        {
            try
            {
                var di = Directory.CreateDirectory(LibsDirectory);
                library._msg("Libs Directory successfully created ( " + LibsDirectory + " )");
            }
            catch (Exception e)
            {
                library._msg("Error when try create Libs Folder  - " + e);
            }
        }
    }
}