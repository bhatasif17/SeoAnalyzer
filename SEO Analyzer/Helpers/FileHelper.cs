/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using SEO_Analyzer.Helpers.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Text.Json;

namespace SEO_Analyzer.Helpers
{
    public class FileHelper : IFileHelper
    {
        public List<string> LoadData(string path)
        {
            var data = new List<string>();
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    var words = r.ReadToEnd();
                    var cdata = JsonSerializer.Deserialize<string[]>(words);
                    data.AddRange(cdata);
                }
            }
            catch (Exception ex)
            {
                //TODO logger
            }

            return data;
        }

        public HashSet<string> GetUniqueResultsFromJson(string path)
        {
            List<string> uniqueWords;
            ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
            var cachedItem = cache["unique" + path];
            if (cachedItem == null)
            {
                uniqueWords = LoadData(path);
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10);
                cache.Set("unique" + path, uniqueWords, policy);
            }
            else
            {
                uniqueWords = cachedItem as List<string>;
            }

            var result = new HashSet<string>(uniqueWords);

            return result;
        }
    }
}
