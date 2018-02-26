﻿using System.IO;
using AppGet.FileSystem;
using AppGet.HostSystem;
using AppGet.Serialization;
using Newtonsoft.Json;

namespace AppGet.AppData
{
    public interface IConfig
    {
        string LocalRepository { get; }

        void Save();
    }

    public class Config : IConfig
    {
        public string LocalRepository { get; }


        public Config(IFileSystem fileSystem, IPathResolver pathResolver)
        {
            var fileSystem1 = fileSystem;

            var configFile = Path.Combine(pathResolver.AppDataDirectory, "config.json");

            LocalRepository = Path.Combine(pathResolver.AppDataDirectory, "Repository\\");

            if (fileSystem1.FileExists(configFile))
            {
                var text = fileSystem1.ReadAllText(configFile);
                var settings = Json.Deserialize<dynamic>(text);
                this.LocalRepository = settings.localRepository;
            }
            else
            {
                fileSystem1.WriteAllText(configFile, Json.Serialize(this, Formatting.Indented));
            }
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}