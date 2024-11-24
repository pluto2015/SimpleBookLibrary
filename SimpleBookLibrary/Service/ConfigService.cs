using SimpleBookLibrary.Model.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleBookLibrary.Service
{
    /// <summary>
    /// 配置
    /// </summary>
    public class ConfigService
    {
        /// <summary>
        /// 数据库地址
        /// </summary>
        public string DataBasePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"DataBase/");

        private string ConfigFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs/");

        public AppConfig AppConfig { get; set; }
        public ConfigService()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            var jsonStr = File.ReadAllText(Path.Combine(ConfigFilePath,"AppConfig.json"));
            AppConfig = JsonConvert.DeserializeObject<AppConfig>(jsonStr);
        }

        public void SaveConfig()
        {
            var jsonStr = JsonConvert.SerializeObject(AppConfig);
            File.WriteAllText(Path.Combine(ConfigFilePath, "AppConfig.json"), jsonStr);
        }
    }
}
