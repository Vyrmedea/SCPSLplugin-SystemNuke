namespace SystemNuke
{
    using Exiled.API.Features;
    using ServerHandlers = Exiled.Events.Handlers.Server;
    public class SystemNuke:Plugin<Config>
    {
        private static readonly SystemNuke InstanceVale = new SystemNuke();
        private SystemNuke() { }
        public static SystemNuke Instance=>InstanceVale;
        public override void OnEnabled()
        {
            EventHandlers eventHandlers = new EventHandlers();
            ServerHandlers.RoundStarted += eventHandlers.OnstartingRound;
            ServerHandlers.RestartingRound += eventHandlers.OnrestartingRound;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            EventHandlers eventHandlers = new EventHandlers();
            ServerHandlers.RoundStarted -= eventHandlers.OnstartingRound;
            ServerHandlers.RestartingRound-=eventHandlers.OnrestartingRound;
            base.OnDisabled();
        }
    }
}