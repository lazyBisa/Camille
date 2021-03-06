﻿using System;
using System.Collections.Concurrent;

namespace MingweiSamuel.Camille.Enums
{
    public struct Region
    {
        #region instance creation
        /// <summary>
        /// Dictionary containing known instnaces.
        /// This must be listed above the `Register(...)` calls.
        /// </summary>
        private static readonly ConcurrentDictionary<string, Region> Regions =
            new ConcurrentDictionary<string, Region>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the Region associated with the given region key or platform ID. Throws a KeyNotFoundException if not found.
        /// </summary>
        /// <param name="name">Region key or platform ID.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">If region not found.</exception>
        /// <returns>Matching region.</returns>
        public static Region Get(string name)
        {
            return Regions[name];
        }

        /// <summary>
        /// Create region instance and register in Regions dictionary. Private.
        /// </summary>
        private static Region Register(string key, string platform)
        {
#pragma warning disable 618
            var region = new Region(key, platform);
#pragma warning restore 618
            Regions[key] = region;
            if (platform != null)
                Regions[platform] = region;
            return region;
        }
        #endregion

        #region normal regions
        /// <summary>Brazil.</summary>
        public static readonly Region BR = Register("BR", "BR1");
        /// <summary>North-east Europe.</summary>
        public static readonly Region EUNE = Register("EUNE", "EUN1");
        /// <summary>West Europe.</summary>
        public static readonly Region EUW = Register("EUW", "EUW1");
        /// <summary>North America.</summary>
        public static readonly Region NA = Register("NA", "NA1");
        /// <summary>Korea.</summary>
        public static readonly Region KR = Register("KR", "KR");
        /// <summary>North Latin America.</summary>
        public static readonly Region LAN = Register("LAN", "LA1");
        /// <summary>South Latin America.</summary>
        public static readonly Region LAS = Register("LAS", "LA2");
        /// <summary>Oceania.</summary>
        public static readonly Region OCE = Register("OCE", "OC1");
        /// <summary>Russia.</summary>
        public static readonly Region RU = Register("RU", "RU");
        /// <summary>Turkey.</summary>
        public static readonly Region TR = Register("TR", "TR1");
        /// <summary>Japan.</summary>
        public static readonly Region JP = Register("JP", "JP1");
        #endregion

        #region other regions
        /// <summary>Public beta environment. Only functional in certain endpoints.</summary>
        public static readonly Region PBE = Register("PBE", "PBE1");
        /// <summary>Garena publisher - South east asia regions. Not functional in endpoints.</summary>
        public static readonly Region Garena = Register("GARENA", null);
        /// <summary>Tencent publisher - China. Not functional in endpoints.</summary>
        public static readonly Region Tencent = Register("TENCENT", null);
        /// <summary>Global.</summary>
        public static readonly Region Global = Register("GLOBAL", "global");
        #endregion

        #region regional proxies
        /// <summary>Regional proxy for Americas.</summary>
        public static readonly Region Americas = Register("AMERICAS", "AMERICAS");
        /// <summary>Regional proxy for Europe.</summary>
        public static readonly Region Europe = Register("EUROPE", "EUROPE");
        /// <summary>Regional proxy for Asia.</summary>
        public static readonly Region Asia = Register("ASIA", "ASIA");
        #endregion

        #region tournament realms
        /// <summary>Tournament Realm 1.</summary>
        public static readonly Region TRLH1 = Register("TRLH", "TRLH1");
        /// <summary>Tournament Realm 2.</summary>
        public static readonly Region TRLH2 = Register("TRLH", "TRLH2");
        /// <summary>Tournament Realm 3.</summary>
        public static readonly Region TRLH3 = Register("TRLH", "TRLH3");
        /// <summary>Tournament Realm 4.</summary>
        public static readonly Region TRLH4 = Register("TRLH", "TRLH4");
        #endregion

        /// <summary>Region key in all capital letters.</summary>
        public readonly string Key;
        /// <summary>Platform ID in capital letters and digits.</summary>
        public readonly string Platform;

        [Obsolete("Use Region.* static regions or Region.Parse(string) instead.")]
        public Region(string key, string platform)
        {
            Key = key;
            Platform = platform;
        }
    }
}
