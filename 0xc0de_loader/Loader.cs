using System;
using System.IO;
using _0xc0de_library;
using _0xc0de_library.INI;

namespace _0xc0de_loader
{
    public class Loader
    {
        public static void Main(string[] args)
        {
            Bridge._ModConfig = new ModConfig();
            Bridge._config = new Config();
            Bridge._modLoader = new ModLoader();
            Bridge._library = new Library();
            Bridge._loader = new Loader();
            Bridge._modInfo = new ModInfo();
            Bridge._Libs = new Libs();

            Bridge._Libs.Init();
            Bridge._config.Load();
        }
    }

    public class Config
    {
        private readonly Data _cd = new Data();
        public Library lib = new Library();

        public void Load()
        {
            CreateData();
            Check();

            Bridge._modLoader.Load();
        }

        private void Check()
        {
            if (!File.Exists(_cd.PathIniFile) || !Directory.Exists(_cd.FolderName))
            {
                lib._msg("Loader Config file or Loader Folder not exist");
                CreateConfig();
            }
            else
            {
                Bridge._library._msg("Loader Config file loaded successfully ( " + _cd.PathIniFile + " )");
            }
        }

        private void CreateConfig()
        {
            try
            {
                var di = Directory.CreateDirectory(_cd.FolderName); //Create INI FILE
                lib._msg("Loader Directory successfully created ( " + _cd.FolderName + " )");
                try
                {
                    var configStream = File.Create(_cd.PathIniFile); //Create INI FILE
                    configStream.Close();
                    lib._msg("Loader Config file successfully created ( " + _cd.PathIniFile + " )");
                }
                catch (Exception e)
                {
                    lib._msg("Error when try create Loader Config file  - " + e);
                }
            }
            catch (Exception e)
            {
                lib._msg("Error when try create Loader Folder  - " + e);
            }

            try
            {
                var inif = new IniFile(_cd.PathIniFile);
                inif.Section(_cd.Section[0]).Set(_cd.Key0[0], _cd.Key1[0]);

                inif.Save(_cd.PathIniFile);
            }
            catch (Exception e)
            {
                lib._msg("Error when try insert DATA - " + e);
            }
        }

        private void CreateData()
        {
            _cd.FolderName = "Loader";
            _cd.FileName = "0xc0de_config.ini";
            _cd.PathIniFile = _cd.FolderName + "/" + _cd.FileName;

            _cd.Section[0] = "config_loader";
            _cd.Key0[0] = "Debug";
            _cd.Key1[0] = "false";
        }

        public class Data
        {
            public string FileName;
            public string FolderName;
            public string[] Key0 = new string[256];
            public string[] Key1 = new string[256];
            public string PathIniFile;
            public string[] Section = new string[256];
        }
    }
}