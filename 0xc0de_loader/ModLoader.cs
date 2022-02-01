using System;
using System.IO;
using System.Reflection;
using _0xc0de_library.AssemblyMod;

namespace _0xc0de_loader
{
    public class ModLoader
    {
        public void Load()
        {
            Bridge._library._msg("ModLoader successfully Load");
            ModConfigInit();
            Check();
        }

        private void ModConfigInit()
        {
            Bridge._ModConfig.ModFolder = "Mods/";
            Bridge._ModConfig.ModPath = "Loader/" + Bridge._ModConfig.ModFolder;
        }

        private static void Check()
        {
            if (!Directory.Exists(Bridge._ModConfig.ModPath))
            {
                Bridge._config.lib._msg("Mods Folder not exists - " + Bridge._ModConfig.ModPath);
                FolderCreate();
            }
            else
            {
                /*  var fileCount = Directory.GetFiles(Bridge._ModConfig.ModPath, "*.dll", SearchOption.TopDirectoryOnly)
                      .Length;*/
                Bridge._library._msg("Mods in Folder : " +
                                     Bridge._library.GetAllFile(Bridge._ModConfig.ModPath, null, "dll"));
                ModChecker();
            }
        }

        private static void FolderCreate()
        {
            try
            {
                Directory.CreateDirectory(Bridge._ModConfig.ModPath);
                Bridge._library._msg("Mods Directory successfully created ( " + Bridge._ModConfig.ModFolder + " )");
            }
            // ReSharper disable once CatchAllClause
            catch (Exception e)
            {
                Bridge._library._msg("Error when try create Mods Folder  - " + e);
            }
        }

        private static void ModChecker()
        {
            _ = Directory.GetFiles(Bridge._ModConfig.ModPath, "*.dll", SearchOption.TopDirectoryOnly)
                .Length;


            var di = new DirectoryInfo(Bridge._ModConfig.ModPath);
            var files = di.GetFiles("*.dll");
            var str2 = "";
            var str3 = "";
            var str4 = "";
            foreach (var file in files)
            {
                //Bridge._library._msg(file.Name);
                var assembly = Assembly.LoadFile(Bridge._ModConfig.ModPath + file.Name);

                foreach (var attr in Attribute.GetCustomAttributes(assembly))
                    switch (attr)
                    {
                        //  Bridge._library._msg("ModID : " + ((ModIDAttribute)attr).Title);
                        case ModIDAttribute attribute:
                            str2 = attribute.Title;
                            break;
                        //Bridge._library._msg("ModID : " + ((ModNameAttribute)attr).Title);
                        case ModNameAttribute attribute:
                            str3 = attribute.Title;
                            break;
                        //Bridge._library._msg("ModID : " + ((ModVersionAttribute)attr).Version);
                        case ModVersionAttribute attribute:
                            str4 = attribute.Version;
                            break;
                    }
                Bridge._library._msg("[ MODID : " + str2 + " ][ MODNAME : " + str3 + " ][ VERSION : " + str4 +
                                     " ]");
                ///todo
                ///make custom class to inject
                foreach (var t in assembly.GetTypes())
                    if (t.IsClass && t.Name == "Main") 
                    {
                        Activator.CreateInstance(t);
                     Bridge._library._msg(t.Name);
                    }
            }
        }
    }


    public class ModConfig
    {
        public string ModFolder;
        public string ModPath;
    }
}