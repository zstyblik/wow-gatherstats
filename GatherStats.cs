using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.Helpers;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GatherStats
{
    public class GatherStats : HBPlugin
    {
        public static int FjarnskagglInBagCount;
        public static int StarlightRoseInBagCount;
        public static int FoxflowerInBagCount;
        public static int DreamleaflInBagCount;
        public static int AethrilInBagCount;
        public static int FelslatelInBagCount;
        public static int LeystoneOrelInBagCount;
        public static int YserallineSeedlInBagCount;
        public static int BloodOfSargeraslInBagCount;
        public static DateTime StartingTime;

        public override void Pulse()
        {

        }

        private void BotEvents_OnBotStarted(EventArgs args)
        {
            FjarnskagglInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Fjarnskaggl\"); return countBags;", 0);
            StarlightRoseInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Starlight Rose\"); return countBags;", 0);
            FoxflowerInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Foxflower\"); return countBags;", 0);
            DreamleaflInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Dreamleaf\"); return countBags;", 0);
            AethrilInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Aethril\"); return countBags;", 0);
            YserallineSeedlInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Yseralline Seed\"); return countBags;", 0);

            FelslatelInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Felslate\"); return countBags;", 0);
            LeystoneOrelInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Leystone Ore\"); return countBags;", 0);

            BloodOfSargeraslInBagCount = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Blood of Sargeras\"); return countBags;", 0);

            StartingTime = DateTime.Now;
            Logging.Write(Colors.MediumPurple, "HB started");
        }
        private void BotEvents_OnBotStopped(EventArgs args)
        {
            int StarlightRose = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Starlight Rose\"); return countBags;", 0);
            int Foxflower = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Foxflower\"); return countBags;", 0);
            int Fjarnskaggl = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Fjarnskaggl\"); return countBags;", 0);
            int Dreamleaf = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Dreamleaf\"); return countBags;", 0);
            int Aethril = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Aethril\"); return countBags;", 0);
            int YserallineSeed = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Yseralline Seed\"); return countBags;", 0);
            int Felslate = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Felslate\"); return countBags;", 0);
            int LeystoneOre = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Leystone Ore\"); return countBags;", 0);
            int BloodOfSargeras = Lua.GetReturnVal<int>("local countBags = GetItemCount(\"Blood of Sargeras\"); return countBags;", 0);

            StarlightRoseInBagCount = StarlightRose - StarlightRoseInBagCount;
            FoxflowerInBagCount = Foxflower - FoxflowerInBagCount;
            FjarnskagglInBagCount = Fjarnskaggl - FjarnskagglInBagCount;
            DreamleaflInBagCount = Dreamleaf - DreamleaflInBagCount;
            AethrilInBagCount = Aethril - AethrilInBagCount;
            YserallineSeedlInBagCount = YserallineSeed - YserallineSeedlInBagCount;
            FelslatelInBagCount = Felslate - FelslatelInBagCount;
            LeystoneOrelInBagCount = LeystoneOre - LeystoneOrelInBagCount;
            BloodOfSargeraslInBagCount = BloodOfSargeras - BloodOfSargeraslInBagCount;

            double totalFarmingTime = DateTime.Now.Subtract(StartingTime).TotalHours;

            double StarlightRosePerHour = StarlightRoseInBagCount / totalFarmingTime;
            double FoxflowerPerHour = FoxflowerInBagCount / totalFarmingTime;
            double FjarnskagglPerHour = FjarnskagglInBagCount / totalFarmingTime;
            double DreamleafPerHour = DreamleaflInBagCount / totalFarmingTime;
            double AethrilPerHour = AethrilInBagCount / totalFarmingTime;
            double YserallineSeedPerHour = YserallineSeedlInBagCount / totalFarmingTime;
            double FelslatePerHour = FelslatelInBagCount / totalFarmingTime;
            double LeystoneOrePerHour = LeystoneOrelInBagCount / totalFarmingTime;
            double BloodOfSargerasPerHour = BloodOfSargeraslInBagCount / totalFarmingTime;

            Logging.Write(Colors.MediumPurple, "Gather Stats");
            Logging.Write(Colors.MediumPurple, "*****************");
            Logging.Write(Colors.MediumPurple, "Total farming time: " + DateTime.Now.Subtract(StartingTime).Hours.ToString() + "h " + DateTime.Now.Subtract(StartingTime).Minutes.ToString() + "min");
            Logging.Write(Colors.MediumPurple, "*****************");
            Logging.Write(Colors.MediumPurple, "Starlight Rose: " + ((int)StarlightRosePerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Foxflower: " + ((int)FoxflowerPerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Fjarnskaggl: " + ((int)FjarnskagglPerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Dreamleaf: " + ((int)DreamleafPerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Aethril: " + ((int)AethrilPerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Yseralline Seed: " + ((int)YserallineSeedPerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Felslate: " + ((int)FelslatePerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Leystone ore: " + ((int)LeystoneOrePerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "Blood of Sargeras: " + ((int)BloodOfSargerasPerHour).ToString() + "/h");
            Logging.Write(Colors.MediumPurple, "*****************");
            Logging.Write(Colors.MediumPurple, "HB Stopped");
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

        public override void OnButtonPress()
        {

        }



        public override string Author { get { return "GGstore"; } }
        public override string Name { get { return "Gather Stats"; } }
        public override Version Version { get { return new Version(1, 0); } }
        public override bool WantButton { get { return false; } }
        public static LocalPlayer Me { get { return StyxWoW.Me; } }

    }
}
