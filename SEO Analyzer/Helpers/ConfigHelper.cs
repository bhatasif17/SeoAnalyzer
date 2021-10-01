/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using Microsoft.Extensions.Configuration;
using SEO_Analyzer.Helpers.Contracts;

namespace SEO_Analyzer.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        private IConfiguration configuration;
        public ConfigHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetValue(string paramName)
        {
            return this.configuration[paramName];
        }

        public int GetValueId(string paramName)
        {
            return int.Parse(GetValue(paramName));
        }
    }
}
