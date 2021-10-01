/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using HtmlAgilityPack;
using SEO_Analyzer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEO_Analyzer.Services.Contracts
{
    public interface IWordCountService
    {
        IList<string> GetExternalLinks(string url, HtmlDocument document);
        IDictionary<string, int> GetMetaKeywordsFrequencies(HtmlDocument document, IDictionary<string, int> wordsCountDictionary);
        Task<ExternalSourceInfoModel> GetRemoteData(string url);
        IDictionary<string, int> GetWordsFrequenciesFromExternalSource(HtmlDocument document);
        IDictionary<string, int> GetWordsFrequenciesFromText(string text);
    }
}