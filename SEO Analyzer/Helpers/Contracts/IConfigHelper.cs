/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
namespace SEO_Analyzer.Helpers.Contracts
{
    public interface IConfigHelper
    {
        string GetValue(string paramName);
        int GetValueId(string paramName);
    }
}