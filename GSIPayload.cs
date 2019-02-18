namespace OsuAuroraMod {

    internal class RootGSINode {
        public ProviderNode provider = new ProviderNode();
        public GameNode game = new GameNode();
    }

    class GameNode {
        public string status = "";
        public double hp = 0;
        public double accuracy = 0;
        public int combo = 0;

        public int count50 = 0;
        public int count100 = 0;
        public int count200 = 0;
        public int count300 = 0;
        public int countKatu = 0;
        public int countGeki = 0;
        public int countMiss = 0;
    }

    class ProviderNode {
        public string name = "osu";
        public int appid = -1;
    }
}
