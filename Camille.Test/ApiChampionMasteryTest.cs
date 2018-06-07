﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MingweiSamuel.Camille.Model.ChampionMastery;
using MingweiSamuel.Camille.Enums;

namespace Camille.Test
{
    [TestClass]
    public class ApiChampionMasteryTest : ApiTest
    {
        [TestMethod]
        public async Task GetChampionAsync()
        {
            CheckGetChampion(await Api.ChampionMastery.GetChampionMasteryAsync(Region.NA, 69009277, ChampionId.Zyra));
        }

        [TestMethod]
        public void GetChampion()
        {
            CheckGetChampion(Api.ChampionMastery.GetChampionMastery(Region.NA, 69009277, ChampionId.Zyra));
        }

        public static void CheckGetChampion(ChampionMastery result)
        {
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.ChampionLevel, result.ChampionLevel.ToString());
            Assert.IsTrue(result.ChampionPoints >= 389_578, result.ChampionPoints.ToString());
        }

        [TestMethod]
        public void GetChampions()
        {
            CheckGetChampions(Api.ChampionMastery.GetAllChampionMasteries(Region.NA, 69009277));
        }

        [TestMethod]
        public async Task GetChampionsAsync()
        {
            CheckGetChampions(await Api.ChampionMastery.GetAllChampionMasteriesAsync(Region.NA, 69009277));
        }

        public static void CheckGetChampions(ChampionMastery[] champData)
        {
            var topChamps = new HashSet<long>
            {
                ChampionId.Zyra, ChampionId.Soraka, ChampionId.Morgana, ChampionId.Sona, ChampionId.Janna,
                ChampionId.Ekko, ChampionId.Nami, ChampionId.Taric, ChampionId.Poppy, ChampionId.Brand
            };
            var topChampCount = topChamps.Count;
            for (var i = 0; i < topChampCount; i++)
                Assert.IsTrue(topChamps.Remove(champData[i].ChampionId), $"Unexpected top champ: {champData[i].ChampionId}.");
            Assert.AreEqual(0, topChamps.Count, $"Champions not found: {topChamps}.");
        }

        [TestMethod]
        public void GetScore()
        {
            // http://www.lolking.net/summoner/euw/20401158/0#champ-mastery
            CheckGetScore(Api.ChampionMastery.GetChampionMasteryScore(Region.EUW, 20401158));
        }

        [TestMethod]
        public async Task GetScoreAsync()
        {
            CheckGetScore(await Api.ChampionMastery.GetChampionMasteryScoreAsync(Region.EUW, 20401158));
        }

        public static void CheckGetScore(int score)
        {
            Assert.IsTrue(952 <= score && score < 1000, score.ToString());
        }
    }
}
