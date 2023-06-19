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
                Exiled.API.Features.Broadcast broadcast =new Exiled.API.Features.Broadcast($"SITE-02已被放弃，系统核弹将于{Warhead.RealDetonationTimer}秒后爆炸");
                foreach ( Player p in Player.List)
                {
                    if (p == null) continue;
                    p.Broadcast( broadcast );
                }
            }
        }
        private IEnumerator<float> AllKillStart()
        {
            yield return Timing.WaitForSeconds(SystemNuke.Instance.Config.AllKillTime);
            for (int i = 30; i > 0; i--)
            {
                yield return Timing.WaitForSeconds(1f);
                Exiled.API.Features.Broadcast broadcast=new Exiled.API.Features.Broadcast($"为保证安全，基金会已发射核弹，将于{i}秒后到达", 1);
                foreach (Player p in Player.List)
                {
                    if (p == null) continue;
                    p.Broadcast(broadcast);
                }
            }
            foreach (Player p in Player.List)
            {
                if (p.IsAlive)
                {
                    if (p == null) continue;
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
