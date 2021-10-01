/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using System.Collections.Generic;

namespace SEO_Analyzer.Helpers.Contracts
{
    public interface IFileHelper
    {
        HashSet<string> GetUniqueResultsFromJson(string path);
        List<string> LoadData(string path);
    }
}