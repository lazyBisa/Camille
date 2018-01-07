﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MingweiSamuel.Camille.Enums;

namespace MingweiSamuel.Camille
{
    public class RiotApi
    {
        private readonly RiotApiConfig _config;

        public RiotApi(RiotApiConfig config)
        {
            _config = config;
        }

        #region requests
        internal T Get<T>(string methodId, string url, Region region, KeyValuePair<string, string>[] queryParams)
        {
            return GetAsync<T>(methodId, url, region, queryParams).Result;
        }

        internal async Task<T> GetAsync<T>(string methodId, string url, Region region, KeyValuePair<string, string>[] queryParams)
        {
            return default(T);
        }

        internal T GetNonRateLimited<T>(string methodId, string url, Region region, KeyValuePair<string, string>[] queryParams)
        {
            return GetNonRateLimitedAsync<T>(methodId, url, region, queryParams).Result;
        }

        internal async Task<T> GetNonRateLimitedAsync<T>(string methodId, string url, Region region, KeyValuePair<string, string>[] queryParams)
        {
            return default(T);
        }
        #endregion
    }
}