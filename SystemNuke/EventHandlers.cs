namespace SystemNuke
{
    using MEC;
    using Exiled.API.Features;
    using System.Collections.Generic;
    using Exiled.API.Enums;

    public class EventHandlers
    {
        private CoroutineHandle sncoroutine;
        private CoroutineHandle akcoroutine;
        private IEnumerator<float> SystemNukeStart()
        {
            yield return Timing.WaitForSeconds(SystemNuke.Instance.Config.SystemNukeTime);
            if (!Warhead.IsDetonated)
            {
                Warhead.Start();
                Warhead.IsLocked = true;
                Broadcast broadcast =new Broadcast($"SITE-02已被放弃，系统核弹将于{Warhead.RealDetonationTimer}秒后爆炸");
                Map.Broadcast(broadcast, true);
            }
        }
        private IEnumerator<float> AllKillStart()
        {
            yield return Timing.WaitForSeconds(SystemNuke.Instance.Config.AllKillTime);
            Broadcast broadcast=new Broadcast("为保证安全，基金会已发射核弹，将于30s秒后到达");
            Map.Broadcast(broadcast, true);
            for (int i = 30; i > 0; i--)
            {
                yield return Timing.WaitForSeconds(1f);
            }
            foreach (Player p in Player.List)
            {
                if (p == null) continue;
                if (p.IsAlive)
                {
                    p.Kill(DamageType.Warhead);
                }
            }
        }
        public void OnstartingRound()
        {
            sncoroutine = Timing.RunCoroutine(SystemNukeStart());
            akcoroutine = Timing.RunCoroutine(AllKillStart());
        }
        public void OnrestartingRound()
        {
            Timing.KillCoroutines(sncoroutine);
            Timing.KillCoroutines(akcoroutine);
        }
    }
}
