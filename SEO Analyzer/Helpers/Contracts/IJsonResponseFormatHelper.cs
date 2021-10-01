/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using SEO_Analyzer.Models;

namespace SEO_Analyzer.Helpers.Contracts
{
    public interface IJsonResponseFormatHelper
    {
        string GetJsonParsedModel(WordCounterResultViewModel inputModel);
    }
}