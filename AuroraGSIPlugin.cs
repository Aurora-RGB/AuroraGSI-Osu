using Newtonsoft.Json;
using OsuRTDataProvider;
using Sync.Plugins;
using System;
using System.Linq;
using System.Net.Http;

namespace OsuAuroraMod {
    
    [SyncRequirePlugin(typeof(OsuRTDataProviderPlugin))]
    public class AuroraGSIPlugin : Plugin {

        // Meta-data
        public const string PLUGIN_NAME = "AuroraGSI";
        public const string PLUGIN_AUTHOR = "Wibble199";
        public const string VERSION = "1.0.0";

        // HTTP/GSI variables
        private HttpClient client = new HttpClient();
        private RootGSINode gsiNode = new RootGSINode();

        // Required constructor
        public AuroraGSIPlugin() : base(PLUGIN_NAME, PLUGIN_AUTHOR) { }

        public override void OnEnable() {
            try {
                // Get a reference to the OsuRTDataProviderPlugin and it's ListenerManager
                var dp = (OsuRTDataProviderPlugin)getHoster().EnumPluings().First(plugin => plugin.Name == "OsuRTDataProvider");
                var lm = dp.ListenerManager;

                // Add events and update the payload
                lm.OnStatusChanged += (_, status) => { gsiNode.game.status = status.ToString(); SendUpdate(); };
                lm.OnAccuracyChanged += acc => { gsiNode.game.accuracy = acc; SendUpdate(); };
                lm.OnHealthPointChanged += hp => { gsiNode.game.hp = hp; SendUpdate(); };
                lm.OnComboChanged += combo => { gsiNode.game.combo = combo; SendUpdate(); };
                lm.OnCount50Changed += count => { gsiNode.game.count50 = count; SendUpdate(); };
                lm.OnCount100Changed += count => { gsiNode.game.count100 = count; SendUpdate(); };
                lm.OnCount300Changed += count => { gsiNode.game.count300 = count; SendUpdate(); };
                lm.OnCountKatuChanged += count => { gsiNode.game.countKatu = count; SendUpdate(); };
                lm.OnCountGekiChanged += count => { gsiNode.game.countGeki = count; SendUpdate(); };
                lm.OnCountMissChanged += count => { gsiNode.game.countMiss = count; SendUpdate(); };

            } catch { }
        }

        // Serialize and send the updates to Aurora
        private void SendUpdate() {
            string json = JsonConvert.SerializeObject(gsiNode);
            client.PostAsync("http://localhost:9088/", new StringContent(json));
        }
    }
}
