namespace SystemNuke
{
    using Exiled.API.Interfaces;

    public class Config:IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public float SystemNukeTime { get; set; } = 840f;
        public float AllKillTime { get; set; } = 1080f;
    }
}
