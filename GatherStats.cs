using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.Helpers;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GatherStats
{
    public class GatherStats : HBPlugin
    {
        public Stopwatch GatherStatsTimer = new Stopwatch();
        public int GatherStatsInterval = 300;  // seconds
        public WoWPoint LastLoc;

        public static float DistanceTravelled;
        // gold
        public static ulong GoldInBagCount;
        // flowers and ore
        public static int AethrilInBagCount;
        public static int BloodOfSargeraslInBagCount;
        public static int DreamleaflInBagCount;
        public static int FelslatelInBagCount;
        public static int FjarnskagglInBagCount;
        public static int FoxflowerInBagCount;
        public static int LeystoneOrelInBagCount;
        public static int StarlightRoseInBagCount;
        public static int YserallineSeedlInBagCount;

        public override void Pulse()
        {
            if (!GatherStatsTimer.IsRunning) {
                GatherStatsTimer.Reset();
                GatherStatsTimer.Start();
            }
            if (GatherStatsTimer.Elapsed.TotalSeconds > GatherStatsInterval) {
                GatherBagStats();
                PrintStats();
                GatherStatsTimer.Reset();
            }
        }

        private void GatherBagStats()
        {
            WoWPlayer Me = StyxWoW.Me;
            DistanceTravelled = LastLoc.Distance(Me.Location);
            LastLoc = Me.Location;

            GoldInBagCount = Me.Gold;
            AethrilInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Aethril\"); return countBags;", 0);
            BloodOfSargeraslInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Blood of Sargeras\"); return countBags;", 0);
            DreamleaflInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Dreamleaf\"); return countBags;", 0);
            FelslatelInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Felslate\"); return countBags;", 0);
            FjarnskagglInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Fjarnskaggl\"); return countBags;", 0);
            FoxflowerInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Foxflower\"); return countBags;", 0);
            LeystoneOrelInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Leystone Ore\"); return countBags;", 0);
            StarlightRoseInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Starlight Rose\"); return countBags;", 0);
            YserallineSeedlInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Yseralline Seed\"); return countBags;", 0);
        }

        private void BotEvents_OnBotStarted(EventArgs args)
        {
            Logging.Write(Colors.MediumPurple, "[@GatherStats] init");
            // init location
            WoWPlayer Me = StyxWoW.Me;
            LastLoc = Me.Location;
            GatherStatsTimer.Start();
            GatherBagStats();
            PrintStats();
        }

        private void BotEvents_OnBotStopped(EventArgs args)
        {
            GatherBagStats();
            PrintStats();
            Logging.Write(Colors.MediumPurple, "[@GatherStats] stop");
            GatherStatsTimer.Stop();
        }

        private void PrintStats() {
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Distance Travelled: " + ((float)DistanceTravelled).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Gold: " + ((ulong)GoldInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Aethril: " + ((int)AethrilInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Blood of Sargeras: " + ((int)BloodOfSargeraslInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Dreamleaf: " + ((int)DreamleaflInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Felslate: " + ((int)FelslatelInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Fjarnskaggl: " + ((int)FjarnskagglInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Foxflower: " + ((int)FoxflowerInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Leystone Ore: " + ((int)LeystoneOrelInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Starlight Rose: " + ((int)StarlightRoseInBagCount).ToString());
            Logging.Write(Colors.MediumPurple, "[@GatherStats] Yseralline Seed: " + ((int)YserallineSeedlInBagCount).ToString());
        }

        public override void OnEnable()
        {
            BotEvents.OnBotStarted += BotEvents_OnBotStarted;
            BotEvents.OnBotStopped += BotEvents_OnBotStopped;
        }

        public override void OnDisable()
        {
            BotEvents.OnBotStarted -= BotEvents_OnBotStarted;
            BotEvents.OnBotStopped -= BotEvents_OnBotStopped;

        }

        public override void OnButtonPress() { }
        public override string Author { get { return "GGstore"; } }
        public override string Name { get { return "Gather Stats"; } }
        public override Version Version { get { return new Version(2, 0); } }
        public override bool WantButton { get { return false; } }
        public static LocalPlayer Me { get { return StyxWoW.Me; } }
    }
}
