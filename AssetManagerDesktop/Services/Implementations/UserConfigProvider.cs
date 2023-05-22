using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AssetManagerDesktop.Services.Implementations
{
    [Serializable]
    public class UserConfig
    {
        [JsonIgnore]
        public string ApiToken { get; set; } = string.Empty;

        public string ServerName { get; set; } = String.Empty;
        public string UserName { get; set; } = string.Empty;
    }

    public class UserConfigProvider
    {
        public event EventHandler UserConfigUpdated;

        public UserConfig Config { get; private set; }

        public UserConfigProvider()
        {
            if (File.Exists("./config"))
                Config = JsonSerializer.Deserialize<UserConfig>(File.OpenRead("./config")) ?? new UserConfig();
            else
                Config = new UserConfig();
        }

        public void Deconstruct()
        {
            using FileStream f = File.Open("./config", FileMode.Create);
            JsonSerializer.Serialize(f, Config);
        }

        public void Updated([CallerMemberName] string? caller = null)
        {
            UserConfigUpdated?.Invoke(caller, EventArgs.Empty);
        }
    }
}