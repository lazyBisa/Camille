using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MingweiSamuel.Camille.Enums
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Queues
    {
        public class Queue
        {
            public readonly string   Name;
            public readonly int      Id;
            public readonly Maps.Map Map;

            internal Queue(int id, Maps.Map map, string name)
            {
                this.Name = name;
                this.Map  = map;
                this.Id   = id;
            }

            public static implicit operator string(Queue queue) => queue.Name;
            public static implicit operator int(Queue queue) => queue.Id;
            public static implicit operator Queue(int id) => GetQueue(id);
            public static implicit operator Queue(string name) => GetQueue(name);
        }

        public static Queue GetQueue(int id)
        {
            return Queues.AllQueues.Single(q => q.Id == id);
        }
        public static Queue GetQueue(string name)
        {
            return Queues.StandardQueues.SingleOrDefault(q => q.Name == name) ??
                   Queues.CustomGameModes.SingleOrDefault(q => q.Name == name) ?? Queues.DeprecatedQueues.SingleOrDefault(q => q.Name == name);
        }

        public static readonly Queue CUSTOM           = new Queue(0, Maps.SR_CURRENT, "CUSTOM");
        public static readonly Queue NORMAL_5x5_DRAFT = new Queue(400, Maps.SR_CURRENT, "NORMAL_5x5_DRAFT");
        public static readonly Queue RANKED_5x5_SOLO  = new Queue(420, Maps.SR_CURRENT, "RANKED_SOLO_5x5");
        public static readonly Queue NORMAL_5x5_BLIND = new Queue(430, Maps.SR_CURRENT, "NORMAL_5x5_BLIND");
        public static readonly Queue RANKED_5x5_FLEX  = new Queue(440, Maps.SR_CURRENT, "RANKED_FLEX_SR");
        public static readonly Queue ARAM_5x5         = new Queue(450, Maps.HOWLING_ABYSS, "ARAM_5x5");
        public static readonly Queue NORMAL_3x3       = new Queue(460, Maps.TT_CURRENT, "NORMAL_3x3");
        public static readonly Queue RANKED_3x3_FLEX  = new Queue(470, Maps.TT_CURRENT, "RANKED_FLEX_TT");
        public static readonly Queue CLASH            = new Queue(700, Maps.SR_CURRENT, "CLASH");

        public static readonly IEnumerable<Queue> StandardQueues = new[]
            {CUSTOM, NORMAL_5x5_DRAFT, RANKED_5x5_SOLO, NORMAL_5x5_BLIND, RANKED_5x5_FLEX, ARAM_5x5, NORMAL_3x3, RANKED_3x3_FLEX, CLASH};
        public static readonly IEnumerable<Queue> CustomGameModes = new[]
        {
            Others.FIRSTBLOOD_1x1, Others.FIRSTBLOOD_2x2, Others.SR_6x6, Others.URF_5x5, Others.ONEFORALL_MIRRORMODE_5x5, Others.BOT_URF_5x5, Others.TT_6x6,
            Others.BILGEWATER_ARAM_5x5, Others.NEMESIS, Others.BM_BRAWLERS, Others.NOT_DOMINION, Others.ASSASSINATE_5x5, Others.DARKSTAR_3x3, Others.BOT_3x3_INTRO,
            Others.BOT_3x3_BEGINNER, Others.BOT_3x3_INTERMEDIATE, Others.BOT_5x5_INTRO, Others.BOT_5x5_BEGINNER, Others.BOT_5x5_INTERMEDIATE, Others.ARURF_5X5, Others.ASCENSION,
            Others.KING_PORO_5x5, Others.SIEGE, Others.NIGHTMARE_BOT_VOTING, Others.NIGHTMARE_BOT_STANDARD, Others.GUARDIAN_NORMAL, Others.GUARDIAN_ONSLAUGHT, Others.OVERCHARGE,
            Others.ARURF_SNOW, Others.ONEFORALL_5x5
        };
        public static readonly IEnumerable<Queue> DeprecatedQueues = new[]
        {
            Others.Deprecated.NORMAL_5x5_BLIND, Others.Deprecated.RANKED_5x5_SOLO, Others.Deprecated.RANKED_5x5_PREMADE, Others.Deprecated.BOT_5x5, Others.Deprecated.NORMAL_3x3,
            Others.Deprecated.RANKED_3x3_FLEX, Others.Deprecated.NORMAL_5x5_DRAFT, Others.Deprecated.ODIN_5x5_BLIND, Others.Deprecated.ODIN_5x5_DRAFT,
            Others.Deprecated.BOT_ODIN_5x5, Others.Deprecated.BOT_5x5_INTRO, Others.Deprecated.BOT_5x5_BEGINNER, Others.Deprecated.BOT_5x5_INTERMEDIATE,
            Others.Deprecated.RANKED_TEAM_3x3, Others.Deprecated.RANKED_TEAM_5x5, Others.Deprecated.BOT_TT_3x3, Others.Deprecated.TEAM_BUILDER_DRAFT_UNRANKED_5x5,
            Others.Deprecated.ARAM_5x5, Others.Deprecated.ONEFORALL_5x5, Others.Deprecated.NIGHTMARE_BOT_5x5_RANK1, Others.Deprecated.NIGHTMARE_BOT_5x5_RANK2,
            Others.Deprecated.NIGHTMARE_BOT_5x5_RANK5, Others.Deprecated.ASCENSION_5x5, Others.Deprecated.KING_PORO_5x5, Others.Deprecated.SIEGE, Others.Deprecated.ARURF_5X5,
            Others.Deprecated.RANKED_PREMADE_5x5
        };
        public static readonly IEnumerable<Queue> AllQueues = Queues.StandardQueues.Concat(Queues.CustomGameModes).Concat(Queues.DeprecatedQueues);

        public static class Others
        {
            public static readonly Queue FIRSTBLOOD_1x1           = new Queue(72, Maps.HOWLING_ABYSS, "FIRSTBLOOD_1x1");
            public static readonly Queue FIRSTBLOOD_2x2           = new Queue(73, Maps.HOWLING_ABYSS, "FIRSTBLOOD_2x2");
            public static readonly Queue SR_6x6                   = new Queue(75, Maps.SR_CURRENT, "SR_6x6");
            public static readonly Queue URF_5x5                  = new Queue(76, Maps.SR_CURRENT, "URF_5x5");
            public static readonly Queue ONEFORALL_MIRRORMODE_5x5 = new Queue(78, Maps.HOWLING_ABYSS, "ONEFORALL_MIRRORMODE_5x5");
            public static readonly Queue BOT_URF_5x5              = new Queue(83, Maps.SR_CURRENT, "BOT_URF_5x5");
            public static readonly Queue TT_6x6                   = new Queue(98, Maps.TT_CURRENT, "TT_6x6");
            public static readonly Queue BILGEWATER_ARAM_5x5      = new Queue(100, Maps.BUTCHERS_BRIDGE, "BILGEWATER_ARAM_5x5");
            public static readonly Queue NEMESIS                  = new Queue(310, Maps.SR_CURRENT, "");
            public static readonly Queue BM_BRAWLERS              = new Queue(313, Maps.SR_CURRENT, "");
            public static readonly Queue NOT_DOMINION             = new Queue(317, Maps.CRYSTAL_SCAR, "");
            public static readonly Queue ASSASSINATE_5x5          = new Queue(600, Maps.SR_CURRENT, "ASSASSINATE_5x5");
            public static readonly Queue DARKSTAR_3x3             = new Queue(610, Maps.COSMIC_RUINS, "DARKSTAR_3x3");
            public static readonly Queue BOT_3x3_INTRO            = new Queue(800, Maps.SR_CURRENT, "BOT_3x3_INTRO");
            public static readonly Queue BOT_3x3_BEGINNER         = new Queue(810, Maps.SR_CURRENT, "BOT_3x3_BEGINNER");
            public static readonly Queue BOT_3x3_INTERMEDIATE     = new Queue(820, Maps.SR_CURRENT, "BOT_3x3_INTERMEDIATE");
            public static readonly Queue BOT_5x5_INTRO            = new Queue(830, Maps.SR_CURRENT, "BOT_5x5_INTRO");
            public static readonly Queue BOT_5x5_BEGINNER         = new Queue(840, Maps.SR_CURRENT, "BOT_5x5_BEGINNER");
            public static readonly Queue BOT_5x5_INTERMEDIATE     = new Queue(850, Maps.SR_CURRENT, "BOT_5x5_INTERMEDIATE");
            public static readonly Queue ARURF_5X5                = new Queue(900, Maps.SR_CURRENT, "ARURF_5X5");
            public static readonly Queue ASCENSION                = new Queue(910, Maps.CRYSTAL_SCAR, "ASCENSION_5x5");
            public static readonly Queue KING_PORO_5x5            = new Queue(920, Maps.HOWLING_ABYSS, "KING_PORO_5x5");
            public static readonly Queue SIEGE                    = new Queue(940, Maps.SR_CURRENT, "SIEGE");
            public static readonly Queue NIGHTMARE_BOT_VOTING     = new Queue(950, Maps.SR_CURRENT, "");
            public static readonly Queue NIGHTMARE_BOT_STANDARD   = new Queue(960, Maps.SR_CURRENT, "");
            public static readonly Queue GUARDIAN_NORMAL          = new Queue(980, Maps.VALORAN_CITY_PARK, "");
            public static readonly Queue GUARDIAN_ONSLAUGHT       = new Queue(990, Maps.VALORAN_CITY_PARK, "");
            public static readonly Queue OVERCHARGE               = new Queue(1000, Maps.SUBSTRUCTURE_43, "");
            public static readonly Queue ARURF_SNOW               = new Queue(1010, Maps.SUBSTRUCTURE_43, "");
            public static readonly Queue ONEFORALL_5x5            = new Queue(1020, Maps.SR_CURRENT, "ONEFORALL_5x5");


            //TODO: make a propper deprecation system. Most queue are continued in a different queue since a certain patch
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public static class Deprecated
            {
                public static readonly Queue NORMAL_5x5_BLIND                = new Queue(2, Maps.SR_CURRENT, "NORMAL_5x5_BLIND");
                public static readonly Queue RANKED_5x5_SOLO                 = new Queue(4, Maps.SR_CURRENT, "RANKED_SOLO_5x5");
                public static readonly Queue RANKED_5x5_PREMADE              = new Queue(6, Maps.SR_CURRENT, "");
                public static readonly Queue BOT_5x5                         = new Queue(7, Maps.SR_CURRENT, "BOT_5x5");
                public static readonly Queue NORMAL_3x3                      = new Queue(8, Maps.TT_CURRENT, "NORMAL_3x3");
                public static readonly Queue RANKED_3x3_FLEX                 = new Queue(9, Maps.TT_CURRENT, "RANKED_FLEX_TT");
                public static readonly Queue NORMAL_5x5_DRAFT                = new Queue(14, Maps.SR_CURRENT, "NORMAL_5x5_DRAFT");
                public static readonly Queue ODIN_5x5_BLIND                  = new Queue(16, Maps.CRYSTAL_SCAR, "ODIN_5x5_BLIND");
                public static readonly Queue ODIN_5x5_DRAFT                  = new Queue(17, Maps.CRYSTAL_SCAR, "ODIN_5x5_DRAFT");
                public static readonly Queue BOT_ODIN_5x5                    = new Queue(25, Maps.CRYSTAL_SCAR, "BOT_ODIN_5x5");
                public static readonly Queue BOT_5x5_INTRO                   = new Queue(31, Maps.SR_CURRENT, "BOT_5x5_INTRO");
                public static readonly Queue BOT_5x5_BEGINNER                = new Queue(32, Maps.SR_CURRENT, "BOT_5x5_BEGINNER");
                public static readonly Queue BOT_5x5_INTERMEDIATE            = new Queue(33, Maps.SR_CURRENT, "BOT_5x5_INTERMEDIATE");
                public static readonly Queue RANKED_TEAM_3x3                 = new Queue(41, Maps.TT_CURRENT, "RANKED_TEAM_3x3");
                public static readonly Queue RANKED_TEAM_5x5                 = new Queue(42, Maps.SR_CURRENT, "RANKED_TEAM_5x5");
                public static readonly Queue BOT_TT_3x3                      = new Queue(52, Maps.TT_CURRENT, "BOT_TT_3x3");
                public static readonly Queue TEAM_BUILDER_DRAFT_UNRANKED_5x5 = new Queue(61, Maps.SR_CURRENT, "TEAM_BUILDER_DRAFT_UNRANKED_5x5");
                public static readonly Queue ARAM_5x5                        = new Queue(65, Maps.HOWLING_ABYSS, "ARAM_5x5");
                public static readonly Queue ONEFORALL_5x5                   = new Queue(70, Maps.SR_CURRENT, "ONEFORALL_5x5");
                public static readonly Queue NIGHTMARE_BOT_5x5_RANK1         = new Queue(91, Maps.SR_CURRENT, "NIGHTMARE_BOT_5x5_RANK1");
                public static readonly Queue NIGHTMARE_BOT_5x5_RANK2         = new Queue(92, Maps.SR_CURRENT, "NIGHTMARE_BOT_5x5_RANK2");
                public static readonly Queue NIGHTMARE_BOT_5x5_RANK5         = new Queue(93, Maps.SR_CURRENT, "NIGHTMARE_BOT_5x5_RANK5");
                public static readonly Queue ASCENSION_5x5                   = new Queue(96, Maps.CRYSTAL_SCAR, "ASCENSION_5x5");
                public static readonly Queue KING_PORO_5x5                   = new Queue(300, Maps.HOWLING_ABYSS, "KING_PORO_5x5");
                public static readonly Queue SIEGE                           = new Queue(315, Maps.SR_CURRENT, "SIEGE");
                public static readonly Queue ARURF_5X5                       = new Queue(318, Maps.SR_CURRENT, "ARURF_5X5");
                public static readonly Queue RANKED_PREMADE_5x5              = new Queue(410, Maps.SR_CURRENT, "RANKED_PREMADE_5x5");

                //public static readonly Queue BILGEWATER_5x5           = new Queue(-1, Maps.BUTCHERS_BRIDGE, "BILGEWATER_5x5");
                //public static readonly Queue GROUP_FINDER_5x5         = new Queue(-1, Maps.SR_CURRENT, "GROUP_FINDER_5x5");
                //public static readonly Queue TEAM_BUILDER_DRAFT_RANKED_5x5   = new Queue(-1, Maps.SR_CURRENT, "TEAM_BUILDER_DRAFT_RANKED_5x5");
                //public static readonly Queue TEAM_BUILDER_RANKED_SOLO        = new Queue(-1, Maps.SR_CURRENT, "TEAM_BUILDER_RANKED_SOLO");
            }
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class Maps
    {
        public struct Map
        {
            public readonly int    Id;
            public readonly string Name;

            internal Map(int id, string name)
            {
                this.Id   = id;
                this.Name = name;
            }
        }

        public static Map GetMap(int mapId)
        {
            return Maps.MapList.Single(m => m.Id == mapId);
        }

        public static readonly Map SR_ORIGINAL_SUMMER = new Map(1, "Summoner's Rift - Original Summer Variant");
        public static readonly Map SR_ORIGINAL_AUTUMN = new Map(2, "Summoner's Rift - Original Autumn Variant");
        public static readonly Map SR_CURRENT         = new Map(11, "Summoner's Rift - Current Version");
        public static readonly Map TT_ORIGINAL        = new Map(4, "Twisted Treeline - Original Version");
        public static readonly Map TT_CURRENT         = new Map(10, "Twisted Treeline - Current Version");
        public static readonly Map HOWLING_ABYSS      = new Map(12, "Howling Abyss - ARAM Map");
        public static readonly Map PROVING_GROUNDS    = new Map(3, "The Proving Grounds - Tutorial Map");
        public static readonly Map BUTCHERS_BRIDGE    = new Map(14, "Butcher's Bridge - ARAM Map");
        public static readonly Map CRYSTAL_SCAR       = new Map(8, "The Crystal Scar - Dominion Map");
        public static readonly Map COSMIC_RUINS       = new Map(16, "Cosmic Ruins - Dark Star: Singularity Map");
        public static readonly Map VALORAN_CITY_PARK  = new Map(18, "Valoran City Park - Star Guardian Invasion Map");
        public static readonly Map SUBSTRUCTURE_43    = new Map(19, "Substructure 43 - PROJECT: Hunters Map");

        public static readonly IEnumerable<Map> MapList = new[]
        {
            SR_ORIGINAL_SUMMER,
            SR_ORIGINAL_AUTUMN,
            PROVING_GROUNDS,
            TT_ORIGINAL,
            CRYSTAL_SCAR,
            TT_CURRENT,
            SR_CURRENT,
            HOWLING_ABYSS,
            BUTCHERS_BRIDGE,
            COSMIC_RUINS,
            VALORAN_CITY_PARK,
            SUBSTRUCTURE_43
        };
    }
}
