/* ------------------------------------------------------------------
 * The MIT License (MIT)
 * Copyright (c) 2021 Asif Bhat
   ------------------------------------------------------------------ */
using HtmlAgilityPack;
using System.Collections.Generic;

namespace SEO_Analyzer.Helpers.Contracts
{
    public interface IHtmlDocumentHelper
    {
        List<string> GetExternalLinks(string url, HtmlDocument document);
        bool IsValid(HtmlDocument document);
        void SanitizeContent(HtmlDocument document);
    }
}